using AllanNovalta.PollQuestion.Web.Infrastructures.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllanNovalta.PollQuestion.Web.Infrastructures.Data.Models
{
    public class PollChoice : BaseModel
    {
        public string Text { get; set; }

        public string Value { get; set; }


    }
}