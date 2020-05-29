using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class FeedbackCategory
    {
        public FeedbackCategory()
        {
            Feedback = new HashSet<Feedback>();
        }

        public int Id { get; set; }
        public string Category { get; set; }

        public virtual ICollection<Feedback> Feedback { get; set; }
    }
}
