using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class ClientRepo
    {
        WasteAppDbContext _db;
        public ClientRepo(WasteAppDbContext db)
        {
            this._db = db;
        }
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


        /////Requests List
        public List<Request> RequestsList(int id)
        {

            return _db.Request.Where(x => x.ClientId == id).Select(x => new Request()
            {
                Id = x.Id,
                ApartmentNumber = x.ApartmentNumber,
                ClientId = x.ClientId,
                ScheduleId = x.ScheduleId,
                BuildingNumber = x.BuildingNumber,
                Points = x.Points,
                OrgaincWeight = x.OrgaincWeight,
                NonOrganicWeight = x.NonOrganicWeight,
                AddressId = x.AddressId,
                CollectorId = x.CollectorId,
                Rate = x.Rate,
                IsSeparated = x.IsSeparated,
                Schedule = new Schedule()
                {
                    Time = x.Schedule.Time
                }
            }).ToList();
        }
        ///
        /// new request
        /// 
        public bool StartNewRequest(int id)
        {
            bool flag = (_db.Request.Where(x => x.ClientId == id && x.Schedule.Time > DateTime.Now
            && (x.OrgaincWeight <= 0 || x.NonOrganicWeight <= 0)).ToList()).Count > 0;

            return !flag;
        }
        public Client GetClient(int id)
        {
            Client client = _db.Client.Select(x => new Client
            {
                Id = x.Id,
                ClientName = x.ClientName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Mobile = x.Mobile,
                AddressId = x.AddressId,
                BuildingNumber = x.BuildingNumber,
                ApartmentNumber = x.ApartmentNumber,
                TotalPoints = x.TotalPoints,
                CategoryId = x.CategoryId,
                Address = new Address()
                {
                    Id = x.Address.Id,
                    RegionId = x.Address.RegionId,
                    StreetName = x.Address.StreetName
                }
            })
                 .Where(x => x.Id == id).FirstOrDefault(); ;
            return client;
        }

        public List<Region> getRegions()
        {

            return _db.Region.Select(x => new Region()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public List<Address> getAddresses(int id)
        {
            List<Address> addresses = _db.Address.Select(x => new Address()
            {
                Id = x.Id,
                RegionId = x.RegionId,
                StreetName = x.StreetName
            }).Where(a => a.RegionId == id).ToList();
            return (addresses);
        }

        public List<Schedule> getSchedules(int id)
        {

            List<Schedule> schedules = _db.Schedule.Select(x => new Schedule()
            {
                Id = x.Id,
                Time = x.Time,
                DriverId = x.DriverId,
                RegionId = x.RegionId
            }).Where(a => a.RegionId == id).ToList();
            return schedules;
        }
        public void AddNewRequest(Request request)
        {
            _db.Request.Add(request);
            _db.SaveChanges();
        }
        public Request GetCurrentRequest(int id)
        {
            Request request = _db.Request.Select(x => new Request()
            {
                Id = x.Id,
                ApartmentNumber = x.ApartmentNumber,
                ClientId = x.ClientId,
                ScheduleId = x.ScheduleId,
                BuildingNumber = x.BuildingNumber,
                Points = x.Points,
                OrgaincWeight = x.OrgaincWeight,
                NonOrganicWeight = x.NonOrganicWeight,
                AddressId = x.AddressId,
                CollectorId = x.CollectorId,
                Rate = x.Rate,
                IsSeparated = x.IsSeparated,
                Schedule = new Schedule()
                {
                    Time = x.Schedule.Time
                }
            }).Where(x => x.ClientId == id).ToList().Last();
            return request;
        }

        public Request DeleteRequest(int id)
        {
            Request request = _db.Request.Find(id);
            _db.Request.Remove(request);
            _db.SaveChanges();
            return request;
        }
    }
}
