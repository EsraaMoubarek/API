using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class UserRepo
    {
        WasteAppDbContext _db;
        public UserRepo(WasteAppDbContext db)
        {
            this._db = db;
        }
        public List<User> GetUsers()
        {
            return _db.User.ToList();
        }
        public User addUser(User u)
        {
            _db.User.Add(u);
            _db.SaveChanges();
            return u;
        }



        /////////
        public void RegisterApartment(Client client)
        {
            client.CategoryId = 1;
            _db.Client.Add(client);
            _db.SaveChanges();
        }
        public void RegisterRestrnt(Client client)
        {
            client.CategoryId = 2;
            _db.Client.Add(client);
            _db.SaveChanges();
        }
        public Client FindClient(string phonenumber, string Password)
        {
            Client client = _db.Client.SingleOrDefault(u => u.Mobile == phonenumber && u.Password == Password);
            return client;
        }
        public User FindUser(string phonenumber, string Password)
        {
            User user = _db.User.SingleOrDefault(u => u.PhoneNumber == phonenumber && u.Password == Password);
            return user;
        }
        public List<ClientCategory> AllCategory()
        {
            List<ClientCategory> category = _db.ClientCategory.Select(a => new ClientCategory { Id = a.Id, Name = a.Name }).ToList();
            return category;
        }
        #region Collector
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
                ClientStreetName = a.Address.StreetNameArabic,
                ClientRegionName = a.Address.Region.NameArabic,
                Date = a.Schedule.Time,
                ScheduleID = a.Schedule.Id,//to add weight
                NonOrganicWeight = a.NonOrganicWeight,
                ClientPhoneNumber = a.Client.Mobile //addedNew
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

        public int AddWeight(int ClientID, int OrgaincWeight, int NonOrganicWeight, int ScheduleID, bool? IsSeparated)
        {
            Request requestofclient = _db.Request.SingleOrDefault(c => c.ClientId == ClientID && c.ScheduleId == ScheduleID);
            requestofclient.OrgaincWeight = OrgaincWeight;
            requestofclient.NonOrganicWeight = NonOrganicWeight;
            requestofclient.IsSeparated = IsSeparated;
            _db.SaveChanges();
            return requestofclient.Id;

        }
        public object CollectorProfile(int ColectorID)
        {
            return _db.User.Where(c => c.Id == ColectorID).Select(c => new { c.UserName, c.PhoneNumber, c.Email });
        }
        #endregion
    }
}
