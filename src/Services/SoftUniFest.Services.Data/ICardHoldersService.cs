namespace SoftUniFest.Services.Data
{
    using System.Collections.Generic;

    using SoftUniFest.Web.ViewModels.Discounts;

    public interface ICardHoldersService
    {
        ICollection<WaitingDiscountsViewModel> GetAll();
    }
}
