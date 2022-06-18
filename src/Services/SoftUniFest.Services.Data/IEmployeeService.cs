namespace SoftUniFest.Services.Data
{
    using System.Threading.Tasks;

    using SoftUniFest.Web.ViewModels.Discounts;
    using SoftUniFest.Web.ViewModels.PosTerminals;
    using SoftUniFest.Web.ViewModels.Traders;

    public interface IEmployeeService
    {
        Task<EmployeeDiscountListViewModel> GetAll();

        EmployeeDiscountListViewModel GetAllWaiting();

        TraderListViewModel GetAllTraders();

        TerminalListViewModel GetAllTerminals();

        Task RejectDiscount(string discountId, string userId);

        Task ApproveDiscount(string discountId, string userId);
    }
}
