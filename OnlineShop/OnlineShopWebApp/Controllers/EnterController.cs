﻿using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System.Threading;

namespace OnlineShopWebApp.Controllers
{
    public class EnterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Enter(Authorization authorization)
        {
            return Redirect("~/Home/Index/");
        }
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Registration registration)
        {
            return Redirect("~/Home/Index/");
        }

    }
}