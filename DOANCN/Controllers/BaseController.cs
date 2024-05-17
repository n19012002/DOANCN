using Microsoft.AspNetCore.Mvc;

namespace DOANCN.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
