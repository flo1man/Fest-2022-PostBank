namespace SoftUniFest.Web
{
    using System;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Hangfire;
    using Hangfire.Console;
    using Hangfire.Dashboard;
    using Hangfire.SqlServer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using SoftUniFest.Common;
    using SoftUniFest.Data;
    using SoftUniFest.Data.Common;
    using SoftUniFest.Data.Common.Repositories;
    using SoftUniFest.Data.Models;
    using SoftUniFest.Data.Repositories;
    using SoftUniFest.Data.Seeding;
    using SoftUniFest.Services;
    using SoftUniFest.Services.CronJobs;
    using SoftUniFest.Services.Data;
    using SoftUniFest.Services.Mapping;
    using SoftUniFest.Services.Messaging;
    using SoftUniFest.Web.ViewModels;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ApplicationDbContext2>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("SecondConnection")));

            services.AddHangfire(
                config => config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings().UseSqlServerStorage(
                        this.configuration.GetConnectionString("DefaultConnection"),
                        new SqlServerStorageOptions
                        {
                            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                            QueuePollInterval = TimeSpan.Zero,
                            UseRecommendedIsolationLevel = true,
                            UsePageLocksOnDequeue = true,
                            DisableGlobalLocks = true,
                        }).UseConsole());

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }).AddRazorRuntimeCompilation();
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender>(
                x => new SendGridEmailSender(this.configuration["SendGrid:ApiKey"]));
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ITraderService, TraderService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<ICardHoldersService, CardHoldersService>();
            services.AddTransient<IApproveEmployeeService, ApproveEmployeeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager recurringJobManager)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
                SeedHangfireJobs(recurringJobManager);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireServer(new BackgroundJobServerOptions { WorkerCount = 2 });
            app.UseHangfireDashboard(
                    "/hangfire");

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllerRoute(
                        "areaRoute",
                        "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapControllerRoute(
                        "default",
                        "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapRazorPages();
                });
        }

        private static void SeedHangfireJobs(IRecurringJobManager recurringJobManager)
        {
            recurringJobManager.AddOrUpdate<ApplicationSync>("UsersSync", x => x.Work(), Cron.Minutely);
            Thread.Sleep(10);
            recurringJobManager.AddOrUpdate<CardHolderSync>("CardHolderSync", x => x.Work(), Cron.Minutely);
            //recurringJobManager.AddOrUpdate<TraderSync>("TraderSync", x => x.Work(), Cron.Minutely);
            recurringJobManager.AddOrUpdate<EmployeeSync>("EmployeeSync", x => x.Work(), Cron.Minutely);
            recurringJobManager.AddOrUpdate<PosTerminalSync>("PosTerminalSync", x => x.Work(), Cron.Minutely);
            recurringJobManager.AddOrUpdate<NotificationSend>("NotificationSend", x => x.Work(), "30 7 * * wed"); // “At 07:30 on Wednesday.”
        }

        private class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                var httpContext = context.GetHttpContext();
                return httpContext.User.IsInRole(GlobalConstants.AdministratorRoleName);
            }
        }
    }
}