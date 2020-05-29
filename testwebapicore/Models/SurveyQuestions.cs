using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class SurveyQuestions
    {
        public SurveyQuestions()
        {
            ServeyUsers = new HashSet<ServeyUsers>();
        }

        public int? SurveyId { get; set; }
        public int Id { get; set; }
        public string Question { get; set; }
        public string ChoiceA { get; set; }
        public string ChoiceB { get; set; }
        public string ChoiceC { get; set; }
        public string ChoiceD { get; set; }

        public virtual Survey Survey { get; set; }
        public virtual ICollection<ServeyUsers> ServeyUsers { get; set; }
    }
}
