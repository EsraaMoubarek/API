using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class Address
    {
        public Address()
        {
            Client = new HashSet<Client>();
            Request = new HashSet<Request>();
        }

        public int Id { get; set; }
        public int RegionId { get; set; }
        public string StreetName { get; set; }
        public string StreetNameArabic { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<Client> Client { get; set; }
        public virtual ICollection<Request> Request { get; set; }
    }
}
