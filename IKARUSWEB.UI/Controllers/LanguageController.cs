using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace IKARUSWEB.UI.Controllers
{
    public class LanguageController : Controller
    {
        [HttpGet]
        public IActionResult _LanguageMenu()
        {

            return View();
        }

        [HttpPost]
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Ok();
        }
    }
}
