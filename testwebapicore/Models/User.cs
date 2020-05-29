using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class User
    {
        public User()
        {
            Request = new HashSet<Request>();
            Schedule = new HashSet<Schedule>();
            ScheduleCollector = new HashSet<ScheduleCollector>();
        }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        public bool? EmailConfirmed { get; set; }
        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Request> Request { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
        public virtual ICollection<ScheduleCollector> ScheduleCollector { get; set; }
    }
}
