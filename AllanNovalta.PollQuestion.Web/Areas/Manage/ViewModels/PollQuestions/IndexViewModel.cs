using AllanNovalta.PollQuestion.Web.Infrastructures.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllanNovalta.PollQuestion.Web.Areas.Manage.ViewModels.PollQuestions
{
    public class IndexViewModel
    {
        public Page<AllanNovalta.PollQuestion.Web.Infrastructures.Data.Models.PollQuestion> PollQuestions { get; set; }
    }
}
