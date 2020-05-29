using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class Survey
    {
        public Survey()
        {
            ServeyUsers = new HashSet<ServeyUsers>();
            SurveyQuestions = new HashSet<SurveyQuestions>();
        }

        public int Id { get; set; }
        public DateTime? GenerationDate { get; set; }

        public virtual ICollection<ServeyUsers> ServeyUsers { get; set; }
        public virtual ICollection<SurveyQuestions> SurveyQuestions { get; set; }
    }
}
