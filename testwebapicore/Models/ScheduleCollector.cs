using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class ScheduleCollector
    {
        public int ScheduleId { get; set; }
        public int CollectorId { get; set; }

        public virtual User Collector { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}
