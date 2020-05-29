using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class ServeyUsers
    {
        public int SurveyId { get; set; }
        public int ClientId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }

        public virtual Client Client { get; set; }
        public virtual SurveyQuestions Question { get; set; }
    }
}
