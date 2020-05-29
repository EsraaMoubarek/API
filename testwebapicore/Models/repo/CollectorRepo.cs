using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class CollectorRepo
    {
        WasteAppDbContext _db;
        public CollectorRepo(WasteAppDbContext db)
        {
            _db = db;
        }

        public List<ClientListModel> GetClientList(int CollectorID)
        {
            List<ClientListModel> clientLists = new List<ClientListModel>();
            var Clients = _db.Request.Where(c => c.CollectorId == CollectorID).
                Select(a =>
            new ClientListModel
            {
                ClientID = a.Client.Id, //to add weight
                ClientFirstName = a.Client.FirstName,
                ClientLastName = a.Client.LastName,
                ClientApartmentNumber = a.ApartmentNumber,
                ClientBuildingNumber = a.BuildingNumber,
                ClientStreetName = a.Address.StreetName,
                ClientRegionName = a.Address.Region.Name,
                Date = a.Schedule.Time,
                ScheduleID = a.Schedule.Id,//to add weight
            });

            foreach (var client in Clients)
            {

                if (client.Date.ToString("yyyy/MM/dd") == DateTime.Now.ToString("yyyy/MM/dd"))
                {
                    clientLists.Add(client);
                }
            }
            return clientLists;
        }

        public Request AddWeight(int ClientID, int OrgaincWeight, int NonOrganicWeight, int ScheduleID, bool? IsSeparated)
        {
            Request requestofclient = _db.Request.Single(c => c.ClientId == ClientID && c.ScheduleId == ScheduleID);
            requestofclient.OrgaincWeight = OrgaincWeight;
            requestofclient.NonOrganicWeight = NonOrganicWeight;
            requestofclient.IsSeparated = IsSeparated;
            _db.SaveChanges();
            return requestofclient;

        }
        public object CollectorProfile(int ColectorID)
        {
            return _db.User.Where(c => c.Id == ColectorID).Select(c => new { c.UserName, c.PhoneNumber, c.Email });
        }
    }
}
