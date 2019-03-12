using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AllanNovalta.PollQuestion.Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PollAnswersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}