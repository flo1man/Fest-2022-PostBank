namespace SoftUniFest.Services.CronJobs
{
    using SoftUniFest.Data;
    using SoftUniFest.Data.Common;
    using SoftUniFest.Data.Common.Repositories;
    using SoftUniFest.Data.Models;
    using SoftUniFest.Data.Models.App;
    using System.Linq;
    using System.Threading.Tasks;

    public class CardHolderSync
    {
        private readonly IDbQueryRunner queryRunner;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly ApplicationDbContext2 dbContext;

        public CardHolderSync(IDbQueryRunner queryRunner,
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
                $"TRUNCATE TABLE [SoftUniFest2].[dbo].[CardHolders]");

            var cardHolders = this.userRepository.All()
                .Where(x => x.Roles.Any(x => x.RoleId == "e33b9253-b9c8-4cfe-9e18-b4ca11e33835"))
                .ToList();


            foreach (var cardHolder in cardHolders)
            {
                await dbContext.CardHolders.AddAsync(new CardHolder
                {
                    Id = cardHolder.Id,
                    Username = cardHolder.UserName,
                    CardNumber = cardHolder.CreditCardNumber,
                    Email = cardHolder.Email,
                    DateOfRegister = cardHolder.CreatedOn,
                    Password = cardHolder.PasswordHash,
                    PhoneNumber = cardHolder.PhoneNumber,
                });
            }

            await this.dbContext.SaveChangesAsync();
        }
    }
}
