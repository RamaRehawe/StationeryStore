﻿using Microsoft.AspNetCore.Mvc;

namespace StationeryStore.Views.Profile
{
    public class ProfileController : Controller
    {
        [HttpGet]
        [Route("front_profile")]
        public IActionResult Index()
        {
            return View("Profile");
        }
    }
}
