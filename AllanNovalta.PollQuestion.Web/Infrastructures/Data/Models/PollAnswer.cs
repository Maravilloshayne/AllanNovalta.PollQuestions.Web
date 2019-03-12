using AllanNovalta.PollQuestion.Web.Infrastructures.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllanNovalta.PollQuestion.Web.Infrastructures.Data.Models
{
    public class PollAnswer : BaseModel
    {
        public Guid? PollChoiceId { get; set; }

        public Guid? PollQuestionId { get; set; }

        public Guid? UserId { get; set; }



    }
}