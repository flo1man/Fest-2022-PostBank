namespace SoftUniFest.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SoftUniFest.Common;
    using SoftUniFest.Data.Models;
    using SoftUniFest.Services.Data;
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstants.EmployeeRoleName)]
    public class EmployeesController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmployeeService employeeService;

        public EmployeesController(
            UserManager<ApplicationUser> userManager,
            IEmployeeService employeeService)
        {
            this.userManager = userManager;
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
        public async Task<IActionResult> Reject(string discountId)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.employeeService.RejectDiscount(discountId, userId);

            return this.RedirectToAction(nameof(this.Discounts));
        }

        [HttpGet]
        public async Task<IActionResult> Approve(string discountId)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.employeeService.ApproveDiscount(discountId, userId);

            return this.RedirectToAction(nameof(this.Discounts));
        }
    }
}
