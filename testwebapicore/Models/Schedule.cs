using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            Request = new HashSet<Request>();
            ScheduleCollector = new HashSet<ScheduleCollector>();
        }

        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int? DriverId { get; set; }
        public int? RegionId { get; set; }

        public virtual User Driver { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<Request> Request { get; set; }
        public virtual ICollection<ScheduleCollector> ScheduleCollector { get; set; }
    }
}
