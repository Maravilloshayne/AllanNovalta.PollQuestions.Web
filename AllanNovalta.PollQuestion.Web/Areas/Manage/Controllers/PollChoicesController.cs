using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllanNovalta.PollQuestion.Web.Infrastructures.Data.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AllanNovalta.PollQuestion.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PollChoicesController : Controller
    {
        private readonly DefaultDbContext _context;
        private IHostingEnvironment _env;


        public PollChoicesController(DefaultDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}