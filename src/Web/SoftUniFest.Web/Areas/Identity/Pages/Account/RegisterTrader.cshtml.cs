namespace SoftUniFest.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;
    using SoftUniFest.Common;
    using SoftUniFest.Data;
    using SoftUniFest.Data.Common.Repositories;
    using SoftUniFest.Data.Models;
    using SoftUniFest.Services.Messaging;
    using static SoftUniFest.Common.DataConstants;

    public class RegisterTraderModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext dbContext;
        private readonly IDeletableEntityRepository<POSTerminal> posTerminalRepository;

        public RegisterTraderModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext dbContext,
            IDeletableEntityRepository<POSTerminal> posTerminalRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            this.dbContext = dbContext;
            this.posTerminalRepository = posTerminalRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [RegularExpression(@"^(\+359|0)\s?[89](\d{2}\s\d{3}\d{3}|[789]\d{7})$", ErrorMessage = "Invalid Phone Number. Format must start with +359 or 0")]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var userName = "trader_" + Guid.NewGuid().ToString().Substring(0, 8);
                var password = Guid.NewGuid().ToString() + "@S";
                var user = new ApplicationUser { UserName = userName, Email = Input.Email, PhoneNumber = Input.PhoneNumber};
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    var userId = user.Id;
                    var roleId = this.dbContext.Roles.FirstOrDefault(x => x.Name == GlobalConstants.TraderRoleName)?.Id;
                    await this.dbContext.UserRoles.AddAsync(new IdentityUserRole<string>
                    {
                        UserId = userId,
                        RoleId = roleId,
                    });
                    await dbContext.SaveChangesAsync();
                    await this.posTerminalRepository.AddAsync(new POSTerminal
                    {
                        TraderId = userId,
                    });
                    await this.posTerminalRepository.SaveChangesAsync();
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { area = "Identity", code },
                        protocol: Request.Scheme);

                    _logger.LogInformation("User created a new account with password.");
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Your temp username : {userName}");
                    sb.AppendLine($"Your temp password : {password}");
                    sb.Append($"<p>You need to reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    // TODO:
                    await _emailSender.SendEmailAsync("softunifest2022@abv.bg", "SoftUni Fest", Input.Email, "New trader", sb.ToString());

                    return this.RedirectToPage("./SuccessRegister");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
