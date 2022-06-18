namespace SoftUniFest.Services.CronJobs
{
    using SoftUniFest.Data;
    using SoftUniFest.Data.Common;
    using SoftUniFest.Data.Common.Repositories;
    using SoftUniFest.Data.Models;
    using SoftUniFest.Data.Models.App;
    using System.Linq;
    using System.Threading.Tasks;

    public class TraderSync
    {
        private readonly IDbQueryRunner queryRunner;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly ApplicationDbContext2 dbContext;

        public TraderSync(IDbQueryRunner queryRunner,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            ApplicationDbContext2 dbContext)
        {
            this.queryRunner = queryRunner;
            this.userRepository = userRepository;
            this.dbContext = dbContext;
        }

        public async Task Work()
        {
            await queryRunner.RunQueryAsync(
                $"DELETE FROM [SoftUniFest2].[dbo].[Traders]");

            var traders = this.userRepository.All()
                .Where(x => x.Roles.Any(x => x.RoleId == "b75c8fc1-fb06-4774-bba7-416cb0433477"))
                .ToList();

            foreach (var trader in traders)
            {
                await dbContext.Traders.AddAsync(new AppTrader
                {
                    Id = trader.Id,
                    Username = trader.UserName,
                    Email = trader.Email,
                    Password = trader.PasswordHash,
                    PhoneNumber = trader.PhoneNumber,
                    DateOfRegister = trader.CreatedOn
                });
            }

            await this.dbContext.SaveChangesAsync();
        }
    }
}
