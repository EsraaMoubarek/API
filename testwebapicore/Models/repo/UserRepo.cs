using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using testwebapicore.HubConfig;

namespace testwebapicore.Models.repo
{
    public class UserRepo
    {
        WasteAppDbContext _db;
        private IHubContext<ChartHub> _hub;

        public UserRepo(WasteAppDbContext db, IHubContext<ChartHub> hub)
        {
            this._db = db;
            _hub = hub;
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
            var Clients = _db.Request.Where(c => c.CollectorId == CollectorID && (c.OrgaincWeight==0 || c.NonOrganicWeight==0)).
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
                OrganicWeight = a.OrgaincWeight,
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

        public List<ClientListModel> GetDoneClientList(int CollectorID)
        {
            List<ClientListModel> clientLists = new List<ClientListModel>();
            var Clients = _db.Request.Where(c => c.CollectorId == CollectorID && (c.OrgaincWeight > 0 || c.NonOrganicWeight > 0)).
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
                OrganicWeight = a.OrgaincWeight,
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
        public User getById(int id)
        {
            return _db.User.Where(a => a.Id == id).Select(b => new User { UserName = b.UserName }).First();
        }
        public void AddPoints(int ClientID, int NonOrganicWeight, int reqID)
        {
            // return  _db.user  .Where(c => c.Id == ColectorID).Select(c => new { c.UserName, c.PhoneNumber, c.Email });

            Request req = _db.Request.Single(c => c.Id == reqID);
            decimal? avg = GetSelectWasteToCalcAvgTotPrice();
            int pointsCollected = (int)Math.Floor(NonOrganicWeight * (decimal)avg);
            req.Points = pointsCollected;
            //to add total points
            req.Client.TotalPoints = req.Client.TotalPoints + pointsCollected;

            _db.SaveChanges();
            //add these points to his total points
            Client client=  _db.Client.SingleOrDefault(a => a.Id == ClientID);
            client.TotalPoints += pointsCollected;
            //notify el user
            string name = req.Client.ClientName;
            string msg = "You Collect"; 
            string ConnectionID = _db.ClientConnection.Single(c => c.ClientId == ClientID).ConnectionId;
             //All Clients
              // _hub.Clients.All.SendAsync("MessageReceived", name, msg, pointsCollected.ToString());
             //Specific Client
            _hub.Clients.Client(ConnectionID).SendAsync("MessageReceived", name, msg, pointsCollected.ToString());

        }

        public decimal? SelectWasteToCalcAvgTotPrice()
        {
            decimal? totalPrice = 0;
            List<Waste> Wastes = _db.Waste.Select(a => new Waste { Name = a.Name, Price = a.Price, Percent = a.Percent }).ToList();
            foreach (var item in Wastes)
            {
                totalPrice += (item.Percent * item.Price) / 100;
            }
            decimal? AvgTotalPrice = totalPrice / 1000;
            return AvgTotalPrice;
        }
        public decimal? GetSelectWasteToCalcAvgTotPrice()
        {
            return SelectWasteToCalcAvgTotPrice();
        }

    }
}
