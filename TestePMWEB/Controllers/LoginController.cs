using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestePMWEB.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        [Route("loginusuario")]
        public IActionResult Index()
        {
            return View();
        }
    }
}