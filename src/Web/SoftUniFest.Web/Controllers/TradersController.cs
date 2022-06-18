namespace SoftUniFest.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SoftUniFest.Common;
    using SoftUniFest.Data.Models;
    using SoftUniFest.Services.Data;
    using SoftUniFest.Web.ViewModels.Discounts;
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstants.TraderRoleName)]
    public class TradersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITraderService traderService;

        public TradersController(UserManager<ApplicationUser> userManager, ITraderService traderService)
        {
            this.userManager = userManager;
            this.traderService = traderService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DiscountInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.traderService.CreateDiscount(inputModel, userId);
            return this.RedirectToAction(nameof(this.All));
        }

        [HttpGet]
        public IActionResult All(DiscountListViewModel viewModel)
        {
            var userId = this.userManager.GetUserId(this.User);
            var discounts = this.traderService.GetAll(userId);
            viewModel.Discounts = discounts;

            return this.View(viewModel);
        }
    }
}
