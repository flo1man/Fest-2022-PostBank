namespace SoftUniFest.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SoftUniFest.Data;
    using SoftUniFest.Data.Models.App;
    using SoftUniFest.Services.Mapping;
    using SoftUniFest.Web.ViewModels.Discounts;

    public class TraderService : ITraderService
    {
        private readonly ApplicationDbContext2 dbContext;

        public TraderService(ApplicationDbContext2 dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateDiscount(DiscountInputModel inputModel, string userId)
        {
            var discount = new Discount
            {
                Percentage = inputModel.Percentage,
                StartDate = inputModel.StartDate,
                EndDate = inputModel.EndDate,
                TraderId = userId,
                Status = StatusType.Waiting,
            };

            await this.dbContext.Discounts.AddAsync(discount);
            await this.dbContext.SaveChangesAsync();
        }

        public ICollection<DiscountViewModel> GetAll(string userId)
        {
            var discounts = this.dbContext.Discounts
                .Where(x => x.TraderId == userId)
                .To<DiscountViewModel>()
                .ToList();

            return discounts;
        }
    }
}
