using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace seedMS.Web.AspNetCore.Areas.Core
{
    [Authorize]
    [Area("Core")]
    [Route("core")]
    public class CoreController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        [Route("")]
        virtual public IActionResult Index()
        {
            // Main view of the account controller/Area
            return View();
        }
    }
}
