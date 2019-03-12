using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllanNovalta.PollQuestion.Web.Areas.Manage.ViewModels.PollQuestions
{
    public class AttachImageViewModel
    {
        public Guid? PollQuestionId { get; set; }
        public IFormFile Image { get; set; }
    }
}
