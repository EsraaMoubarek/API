using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models
{
    public class ClientListModel
    {
        public int ClientID { get; set; }
        public int ScheduleID { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public int? ClientApartmentNumber { get; set; }
        public int? ClientBuildingNumber { get; set; }
        public string ClientStreetName { get; set; }
        public string ClientRegionName { get; set; }
        public DateTime Date { get; set; }
    }
}
