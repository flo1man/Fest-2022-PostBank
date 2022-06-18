namespace SoftUniFest.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SoftUniFest.Data;
    using SoftUniFest.Data.Models.App;
    using SoftUniFest.Services.Mapping;
    using SoftUniFest.Web.ViewModels.Discounts;
    using SoftUniFest.Web.ViewModels.PosTerminals;
    using SoftUniFest.Web.ViewModels.Traders;

    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext2 dbContext;

        public EmployeeService(ApplicationDbContext2 dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<EmployeeDiscountListViewModel> GetAll()
        {
            var discounts = this.dbContext.Discounts.Include(x => x.Trader).ToList();

            //var expiredDiscounts = discounts
            //    .Where(x => x.Status != StatusType.Expired &&
            //    DateTime.Compare(DateTime.Parse(x.EndDate.ToShortDateString(), CultureInfo.InvariantCulture), DateTime.Parse(DateTime.Now.ToShortDateString(), CultureInfo.InvariantCulture)) <= 0)
            //    .ToList();

            //if (expiredDiscounts.Any())
            //{
            //    foreach (var discount in expiredDiscounts)
            //    {
            //        discount.Status = StatusType.Expired;
            //    }

            //    await this.dbContext.SaveChangesAsync();
            //}

            var viewModel = new EmployeeDiscountListViewModel { Discounts = discounts };

            return viewModel;
        }

        public EmployeeDiscountListViewModel GetAllWaiting()
        {
            var discounts = this.dbContext.Discounts
                .Include(x => x.Trader)
                .Where(x => x.Status == StatusType.Waiting)
                .ToList();

            var viewModel = new EmployeeDiscountListViewModel { Discounts = discounts };

            return viewModel;
        }

        public TerminalListViewModel GetAllTerminals()
        {
            var terminals = this.dbContext.PosTerminals.To<TerminalViewModel>().ToList();

            var viewModel = new TerminalListViewModel { Terminals = terminals };

            return viewModel;
        }

        public TraderListViewModel GetAllTraders()
        {
            var traders = this.dbContext.Traders.To<TraderViewModel>().ToList();

            var viewModel = new TraderListViewModel { Traders = traders };

            return viewModel;
        }

        public void RejectDiscount(string discountId)
        {
            var discount = this.dbContext.Discounts.FirstOrDefault(x => x.Id == discountId);

            discount.Status = StatusType.Rejected;
            discount.ApproveCount = 0;
            this.dbContext.SaveChanges();
        }

        public void ApproveDiscount(string discountId)
        {
            var discount = this.dbContext.Discounts.FirstOrDefault(x => x.Id == discountId);

            discount.Status = StatusType.Active;
            discount.ApproveCount++;
            this.dbContext.SaveChanges();
        }
    }
}
