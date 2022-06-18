namespace SoftUniFest.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class NotificationContoller : BaseController
    {
        [HttpPost]
        public IActionResult Notify(string id)
        {
            return this.Redirect("/Home");
        }
    }
}
