﻿using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers
{
    public class RequestController : Controller
    {
        public IActionResult SubmitRequestScreen()
        {
            return View();
        }
    }
}
