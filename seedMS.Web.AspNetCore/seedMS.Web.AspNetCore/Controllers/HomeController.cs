
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using seedMS.Core.DomainModels.Identity;
using System.Threading.Tasks;

namespace seedMS.Web.AspNetCore.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        //private readonly UserManager<ApplicationUser> userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            //this.userManager = userManager;
        }
        //[Authorize(Roles = "User")]
        public IActionResult Index()
        {
            //string userName =  userManager.GetUserName(User);
            //return View("Index",userName);
            return View("Index", "persona");
        }       
    }
}
