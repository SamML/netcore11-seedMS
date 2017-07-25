using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using seedMS.Core.DomainModels.Repositories;
using seedMS.Web.AspNetCore.Areas.Core.ViewModels;
using seedMS.Web.AspNetCore.Controllers;
using System.Threading.Tasks;

namespace seedMS.Web.AspNetCore.Areas.Core.Controllers
{
    [Area("Core")]
    [Route("core/[controller]")]
    public class AccountController : CoreController
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet]
        [Route("")]
        public override IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("[action]/{id?}")]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[action]/{id?}")]
        public async Task<IActionResult> SignOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [Route("[action]/{id?}")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Route("[action]/{id?}")]
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}