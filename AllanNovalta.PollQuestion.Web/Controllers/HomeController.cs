﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AllanNovalta.PollQuestion.Web.Models;
using Microsoft.AspNetCore.Hosting;
using AllanNovalta.PollQuestion.Web.Infrastructures.Data.Helpers;

namespace AllanNovalta.PollQuestion.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DefaultDbContext _context;
        private IHostingEnvironment _env;


        public HomeController(DefaultDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
