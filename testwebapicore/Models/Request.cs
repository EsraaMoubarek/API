using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class Request
    {
        public Request()
        {
            ScheduleCollector = new HashSet<ScheduleCollector>();
        }

        public int Id { get; set; }
        public int? ApartmentNumber { get; set; }
        public int? ClientId { get; set; }
        public int ScheduleId { get; set; }
        public int? BuildingNumber { get; set; }
        public int? Points { get; set; }
        public int? OrgaincWeight { get; set; }
        public int? NonOrganicWeight { get; set; }
        public int? AddressId { get; set; }
        public int? CollectorId { get; set; }
        public int? Rate { get; set; }
        public bool? IsSeparated { get; set; }

        public virtual Address Address { get; set; }
        public virtual Client Client { get; set; }
        public virtual User Collector { get; set; }
        public virtual Schedule Schedule { get; set; }
        public virtual ICollection<ScheduleCollector> ScheduleCollector { get; set; }
    }
}
