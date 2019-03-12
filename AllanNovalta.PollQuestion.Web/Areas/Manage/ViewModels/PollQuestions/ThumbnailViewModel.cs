using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllanNovalta.PollQuestion.Web.Areas.Manage.ViewModels.PollQuestions
{
    public class ThumbnailViewModel
    {
        public Guid? PollQuestionId { get; set; }
        public IFormFile Thumbnail { get; set; }
    }
}