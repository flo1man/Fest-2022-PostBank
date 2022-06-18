namespace SoftUniFest.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoftUniFest.Common;
    using SoftUniFest.Services.Data;
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstants.EmployeeRoleName)]
    public class EmployeesController : BaseController
    {
        private readonly IEmployeeService employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var viewModel = await this.employeeService.GetAll();

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult AllTraders()
        {
            var viewModel = this.employeeService.GetAllTraders();

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult AllTerminals()
        {
            var viewModel = this.employeeService.GetAllTerminals();

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Discounts()
        {
            var viewModel = this.employeeService.GetAllWaiting();

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Reject(string discountId)
        {
            this.employeeService.RejectDiscount(discountId);

            return this.RedirectToAction(nameof(this.All));
        }

        [HttpGet]
        public IActionResult Approve(string discountId)
        {
            this.employeeService.ApproveDiscount(discountId);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
