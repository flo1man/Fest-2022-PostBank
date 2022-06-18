namespace SoftUniFest.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoftUniFest.Web.ViewModels.Discounts;

    public interface ITraderService
    {
        Task CreateDiscount(DiscountInputModel inputModel, string userId);

        ICollection<DiscountViewModel> GetAll(string userId);
    }
}
