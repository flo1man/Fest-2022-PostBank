namespace SoftUniFest.Services.CronJobs
{
    using Microsoft.EntityFrameworkCore;
    using SoftUniFest.Data;
    using SoftUniFest.Data.Common;
    using SoftUniFest.Data.Common.Repositories;
    using SoftUniFest.Data.Models;
    using SoftUniFest.Data.Models.App;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationSync
    {
        private readonly IDbQueryRunner queryRunner;
        private readonly IDeletableEntityRepository<Employee> employeeRepository;
        private readonly ApplicationDbContext dbContext;

        public ApplicationSync(IDbQueryRunner queryRunner,
            IDeletableEntityRepository<Employee> employeeRepository,
            ApplicationDbContext dbContext)
        {
            this.queryRunner = queryRunner;
            this.employeeRepository = employeeRepository;
            this.dbContext = dbContext;
        }

        public async Task Work()
        {
            await queryRunner.RunQueryAsync(
                $"TRUNCATE TABLE [SoftUniFest].[dbo].[Employees]");

            var employees = this.dbContext.Users.Include(x => x.Roles).ToList()
                .Where(x => x.Roles.Any(x => x.RoleId == "8150ac3c-3491-46a2-af91-7a3d889df3ba"))
                .ToList();

            foreach (var emp in employees)
            {
                await dbContext.Employees.AddAsync(new Employee
                {
                    Id = emp.Id,
                    Username = emp.UserName,
                    Email = emp.Email,
                    Password = emp.PasswordHash,
                    CreatedOn = emp.CreatedOn,
                    DeletedOn = emp.DeletedOn,
                    IsDeleted = emp.IsDeleted,
                    ModifiedOn = emp.ModifiedOn,
                });
            }
            
            await dbContext.SaveChangesAsync();
        }
    }
}
