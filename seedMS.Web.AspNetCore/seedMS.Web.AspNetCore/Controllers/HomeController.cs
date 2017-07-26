using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using seedMS.Core.DomainModels.Repositories;

namespace seedMS.Web.AspNetCore.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
    

        public HomeController()
        {
           
        }

        //[Authorize(Roles = "User")]
        public IActionResult Index()
        {
            
            return View("Index", "persona");
        }
    }
}