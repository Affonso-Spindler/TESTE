using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestePMWEB.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        [Route("registerusuario")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
