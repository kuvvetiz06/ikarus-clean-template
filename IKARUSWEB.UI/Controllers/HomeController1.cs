using Microsoft.AspNetCore.Mvc;

namespace IKARUSWEB.UI.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
