namespace SoftUniFest.Services.CronJobs
{
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

    public class EmployeeSync
    {
        private readonly IDbQueryRunner queryRunner;
        private readonly IDeletableEntityRepository<Employee> employeeRepository;
        private readonly ApplicationDbContext2 dbContext;

        public EmployeeSync(IDbQueryRunner queryRunner,
            IDeletableEntityRepository<Employee> employeeRepository,
            ApplicationDbContext2 dbContext)
        {
            this.queryRunner = queryRunner;
            this.employeeRepository = employeeRepository;
            this.dbContext = dbContext;
        }

        public async Task Work()
        {

            await queryRunner.RunQueryAsync(
                $"TRUNCATE TABLE [SoftUniFest2].[dbo].[Employees]");

            var employees = this.employeeRepository.All().ToList();

            foreach (var employee in employees)
            {
                await dbContext.Employees.AddAsync(new AppEmployee
                {
                    Id = employee.Id,
                    Email = employee.Email,
                    Password = employee.Password,
                    Username = employee.Username,
                });
            }

            await this.dbContext.SaveChangesAsync();
        }
    }
}
