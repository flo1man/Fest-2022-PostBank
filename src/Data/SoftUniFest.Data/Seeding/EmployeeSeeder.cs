namespace SoftUniFest.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using SoftUniFest.Common;
    using SoftUniFest.Data.Models;

    internal class EmployeeSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleId = dbContext.Roles.FirstOrDefault(x => x.Name == GlobalConstants.EmployeeRoleName)?.Id;

            if (!dbContext.UserRoles.Where(x => x.RoleId == roleId).Any())
            {
                var firstEmployee = new ApplicationUser
                {
                    AccessFailedCount = 0,
                    Email = "employee_fest2022@abv.bg",
                    NormalizedEmail = "EMPLOYEE_FEST2022@abv.bg",
                    TwoFactorEnabled = false,
                    EmailConfirmed = false,
                    IsDeleted = false,
                    CreatedOn = DateTime.UtcNow,
                    LockoutEnabled = false,
                    PhoneNumberConfirmed = true,
                    UserName = "emloyee1",
                    NormalizedUserName = "EMPLOYEE1",
                    PasswordHash = "AQAAAAEAACcQAAAAELAa2FQajvi080AHwMWzt3rmn2pQxndbBhlChUfuQtre4p96NR7AVUjJVBKOlX7beA==", // Password = Admin123@
                    PhoneNumber = "+359 894562626",
                    CreditCardNumber = "5479161367882942",
                    ExpiredOn = "11/22",
                };

                var secondEmployee = new ApplicationUser
                {
                    AccessFailedCount = 0,
                    Email = "employee2_fest2022@abv.bg",
                    NormalizedEmail = "EMPLOYEE2_FEST2022@abv.bg",
                    TwoFactorEnabled = false,
                    EmailConfirmed = false,
                    IsDeleted = false,
                    CreatedOn = DateTime.UtcNow,
                    LockoutEnabled = false,
                    PhoneNumberConfirmed = true,
                    UserName = "emloyee2",
                    NormalizedUserName = "EMPLOYEE2",
                    PasswordHash = "AQAAAAEAACcQAAAAELAa2FQajvi080AHwMWzt3rmn2pQxndbBhlChUfuQtre4p96NR7AVUjJVBKOlX7beA==", // Password = Admin123@
                    PhoneNumber = "+359 894562626",
                    CreditCardNumber = "5479161367882942",
                    ExpiredOn = "11/22",
                };

                dbContext.Add(firstEmployee);
                dbContext.Add(secondEmployee);
                await dbContext.SaveChangesAsync();

                dbContext.UserRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = roleId,
                    UserId = firstEmployee.Id,
                });

                dbContext.UserRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = roleId,
                    UserId = secondEmployee.Id,
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
