using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class Client
    {
        public Client()
        {
            Feedback = new HashSet<Feedback>();
            PromotionCodes = new HashSet<PromotionCodes>();
            Request = new HashSet<Request>();
            ServeyUsers = new HashSet<ServeyUsers>();
        }

        public int Id { get; set; }
        public string ClientName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int AddressId { get; set; }
        public string Password { get; set; }
        public int BuildingNumber { get; set; }
        public int ApartmentNumber { get; set; }
        public int? TotalPoints { get; set; }
        public int? CategoryId { get; set; }

        public virtual Address Address { get; set; }
        public virtual ClientCategory Category { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<PromotionCodes> PromotionCodes { get; set; }
        public virtual ICollection<Request> Request { get; set; }
        public virtual ICollection<ServeyUsers> ServeyUsers { get; set; }
    }
}
