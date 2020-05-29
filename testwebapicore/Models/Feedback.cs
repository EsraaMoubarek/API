using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string FeedbackContent { get; set; }
        public int? ClientId { get; set; }
        public DateTime? Date { get; set; }

        public virtual FeedbackCategory Category { get; set; }
        public virtual Client Client { get; set; }
    }
}
