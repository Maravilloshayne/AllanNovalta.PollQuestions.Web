using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllanNovalta.PollQuestion.Web.Areas.Manage.ViewModels.PollQuestions
{
    public class UpdateContentViewModel
    {
        public Guid? PollQuestionId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}