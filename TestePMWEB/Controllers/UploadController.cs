﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestePMWEB.Controllers
{
    [AllowAnonymous]
    public class UploadController : Controller
    {        
        [Route("Upload")]
        public IActionResult Index()
        {
            return View();
        }
    }
}