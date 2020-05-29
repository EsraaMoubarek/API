using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class Address
    {
        public Address()
        {
            Client = new HashSet<Client>();
        }

        public int Id { get; set; }
        public string Governorate { get; set; }
        public int? RegionId { get; set; }
        public string StreetName { get; set; }
        public int? StreetNumber { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<Client> Client { get; set; }
    }
}
