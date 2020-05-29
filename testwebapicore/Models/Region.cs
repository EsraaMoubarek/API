using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class Region
    {
        public Region()
        {
            Address = new HashSet<Address>();
            Schedule = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
    }
}
