using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllanNovalta.PollQuestion.Web.Areas.Manage.ViewModels.PollQuestions
{
    public class BannerViewModel
    {
        public Guid? PollQuestionId { get; set; }
        public IFormFile Banner { get; set; }
    }
}
