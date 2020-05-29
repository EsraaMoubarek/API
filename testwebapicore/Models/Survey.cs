using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class Survey
    {
        public Survey()
        {
            SurveyQuestions = new HashSet<SurveyQuestions>();
        }

        public int Id { get; set; }
        public DateTime? GenerationDate { get; set; }

        public virtual ICollection<SurveyQuestions> SurveyQuestions { get; set; }
    }
}
