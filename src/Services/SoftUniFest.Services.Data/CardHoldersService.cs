namespace SoftUniFest.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SoftUniFest.Data;
    using SoftUniFest.Data.Models.App;
    using SoftUniFest.Services.Mapping;
    using SoftUniFest.Web.ViewModels.Discounts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CardHoldersService : ICardHoldersService
    {
        private readonly ApplicationDbContext2 dbContext;

        public CardHoldersService(ApplicationDbContext2 dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICollection<WaitingDiscountsViewModel> GetAll()
        {
            return this.dbContext.Discounts
                .Where(x => x.ApproveCount == 2)
                .To<WaitingDiscountsViewModel>()
                .ToList();
        }
    }
}
