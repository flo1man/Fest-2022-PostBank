namespace SoftUniFest.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoftUniFest.Common;
    using SoftUniFest.Services.Data;

    [Authorize(Roles = GlobalConstants.CardHolderRoleName)]
    public class CardHoldersController : BaseController
    {
        private readonly ICardHoldersService cardHoldersService;

        public CardHoldersController(ICardHoldersService cardHoldersService)
        {
            this.cardHoldersService = cardHoldersService;
        }

        [HttpGet]
        public IActionResult All()
        {
            var viewModel = this.cardHoldersService.GetAll();

            return this.View(viewModel);
        }
    }
}
