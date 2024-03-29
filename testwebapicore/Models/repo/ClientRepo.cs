﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            client.ClientName = "DUMMY";
            _db.Client.Add(client);
            _db.SaveChanges();
        }
        public void RegisterRestrnt(Client client)
        {
            client.CategoryId = 2;

            client.ClientName = "DUMMY";
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

            return _db.Request.Where(x => x.ClientId == id && x.IsSeparated !=null 
            && (x.OrgaincWeight > 0 || x.NonOrganicWeight > 0)).Select(x => new Request()
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
                },
                Address = new Address() {
                    StreetName = x.Address.StreetName
                    , Region = new Region() { 
                        Id = x.Address.Region.Id,
                        Name = x.Address.Region.Name
                    }
                }
               , Collector = new User()
               {
                   Id = x.Collector.Id,
                   UserName = x.Collector.UserName
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
                 .Where(x => x.Id == id).FirstOrDefault();
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

        public List<Schedule> getSchedules(int id,int clientId)
        {
            List<Request> clientRequests = _db.Request
                .Where(x => x.ClientId == clientId &&
                 x.Schedule.RegionId == id &&
                x.Schedule.Time >= DateTime.Now
                ).ToList();

            List<Schedule> schedules = _db.Schedule.Select(x => new Schedule()
            {
                Id = x.Id,
                Time = x.Time,
                DriverId = x.DriverId,
                RegionId = x.RegionId
            }).Where(a => a.RegionId == id && a.Time >= DateTime.Now).ToList();

            foreach (var request in clientRequests)
            {
                Schedule schedule = _db.Schedule.Find(request.ScheduleId);
                schedules.RemoveAll(element => element.Id == schedule.Id);

            }
            return schedules;
        }
        public void AddNewRequest(Request request)
        {
            var schedule = _db.Schedule.Find(request.ScheduleId);

            if (schedule.Time == DateTime.Now.Date) {
                
                var requests = _db.Request.Where(x => x.ScheduleId == request.ScheduleId
                    && x.AddressId == request.AddressId
                ).ToList();

                if (requests != null)
                {
                    request.CollectorId = requests.Select(x => x.CollectorId).First();
                }
                else 
                {
                    var scheduleCollectors = _db.ScheduleCollector
                    .Where(x => x.ScheduleId == request.ScheduleId).ToList();
                    request.CollectorId = scheduleCollectors.Select(x => x.CollectorId).First();
                }
            }
            else{
                request.CollectorId = null;
            }
            _db.Request.Add(request);
            _db.SaveChanges();
      
        }
        public Request GetCurrentRequest(int id)
        {
            try
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
                        ,Region = new Region() {
                            Id = x.Schedule.Region.Id,
                            Name = x.Schedule.Region.Name,
                            NameArabic = x.Schedule.Region.NameArabic
                        }
                    }
                    ,Address = new Address() {
                        Id = x.Address.Id,
                        StreetName = x.Address.StreetName,
                        StreetNameArabic= x.Address.StreetNameArabic
                    }
                }).Where(x => x.ClientId == id && (x.OrgaincWeight <=0 ||x.OrgaincWeight ==null )
                && (x.NonOrganicWeight<=0 || x.NonOrganicWeight == null))
                .ToList().First();

                return request;
            }
            catch {
                return new Request() { };
            }
        }

        public Request DeleteRequest(int id)
        {
            Request request = _db.Request.Find(id);
            _db.Request.Remove(request);
            _db.SaveChanges();
            return request;
        }
        ///******** Promotion Part ********/// 
        public List<Promotions> getPromotions()
        {
            List<Promotions> promotions = new List<Promotions>();
            Promotions validPromotion = new Promotions();

            var availblePromotions = _db.PromotionCodes.Where(x => x.ClientId == null)
                .Select(x=>x.PromtionId).ToList();

            var t = availblePromotions.Distinct();

            foreach (var promotion in t) {
                validPromotion = _db.Promotions.Select(x => new Promotions()
                {
                    Id = x.Id,
                    Name = x.Name,
                    RequiredPoints = x.RequiredPoints,
                    DateFrom = x.DateFrom,
                    DateTo = x.DateTo,
                    Details = x.Details,
                    Company = new ComapnyPromotion() { 
                        Id = x.Company.Id,
                        Name = x.Company.Name
                    }
                })
                 .Where(x => x.Id == promotion).FirstOrDefault();
                if (validPromotion.DateTo >= DateTime.Now)
                {
                    promotions.Add(validPromotion);
                }
            }
           // var promotions = _db.Promotions.Where(x => x.DateTo >= DateTime.Now ).ToList(); 
            return promotions;
        }

        public bool AddClientPromotion(PromotionCodes clientPromotion) {
            try
            {
                var client = _db.Client
                       .Find(clientPromotion.ClientId);

                var promoRequiredPoints = _db.Promotions
                    .Where(x => x.Id == clientPromotion.PromtionId)
                    .Select(x => x.RequiredPoints);

                // can cause null reference exception ** modification needed
                if (client.TotalPoints >= promoRequiredPoints.FirstOrDefault())
                {

                    var promotionCode = _db.PromotionCodes
                        .Where(x => x.PromtionId == clientPromotion.PromtionId
                        && x.ClientId == null).FirstOrDefault();

                    promotionCode.ClientId = clientPromotion.ClientId;
                    promotionCode.TakeDate = DateTime.Now;
                  //  _db.ClientPromotions.Add(clientPromotion);

                    ///
                    client.TotalPoints = client.TotalPoints - promoRequiredPoints.FirstOrDefault();

                    _db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (NullReferenceException) {
                return false;
            }
        }

        public List<PromotionCodes> GetMyPromotions(int clientId) {

            List<PromotionCodes> clientPromotions = _db.PromotionCodes
                .Where(x => x.ClientId == clientId)
                .Select(x => new PromotionCodes()
                {
                    ClientId = x.ClientId,
                    PromtionId = x.PromtionId,
                    Code = x.Code,
                    TakeDate = x.TakeDate,
                    Promtion = new Promotions() {
                        Name = x.Promtion.Name,
                        RequiredPoints = x.Promtion.RequiredPoints,
                        DateFrom = x.Promtion.DateFrom,
                        DateTo = x.Promtion.DateTo,
                        Details = x.Promtion.Details,
                        Company = new ComapnyPromotion() {
                            Name = x.Promtion.Company.Name
                        }
                    }
                })
                .ToList();

           return clientPromotions;

        }
        public int getClientPoints(int id) {
            var points = _db.Client.Where(x => x.Id == id).Select(x => x.TotalPoints).FirstOrDefault();
            if (points != null)
            {
                return (int)points;
            }
            else{
                return 0;
            }
            
        }


        ///******** Profile ********/// 

        public Client getClientData(int id) {

            return _db.Client.Where(x=>x.Id == id)
                .Select(x=>new Client()
                {
                    Id = x.Id,
                    ClientName = x.ClientName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Mobile = x.Mobile,
                    Email = x.Email,
                    BuildingNumber = x.BuildingNumber,
                    ApartmentNumber = x.ApartmentNumber,
                    AddressId= x.AddressId,
                    CategoryId = x.CategoryId,
                    Category = new ClientCategory(){
                        Id = x.Category.Id,
                        Name = x.Category.Name
                    },
                    Address = new Address() { 
                        Id = x.Address.Id,
                        StreetName = x.Address.StreetName,
                        RegionId=x.Address.RegionId,
                        Region = new Region() {
                            Id = x.Address.Region.Id,
                            Name = x.Address.Region.Name
                        }
                    }
                }).FirstOrDefault();
        }
        public List<ClientCategory> GetClientCategories() {

            return _db.ClientCategory.Select(x=>new ClientCategory() { 
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public void UpdateClient(Client client) {
            var clientUpdated = _db.Client.SingleOrDefault(x => x.Id == client.Id);
            clientUpdated.FirstName = client.FirstName;
            clientUpdated.LastName = client.LastName;
            //clientUpdated.Mobile = client.Mobile;
            clientUpdated.BuildingNumber = client.BuildingNumber;
            clientUpdated.ApartmentNumber = client.ApartmentNumber;
            clientUpdated.AddressId = client.AddressId;
            clientUpdated.CategoryId = client.CategoryId;

            _db.SaveChanges();
        }
        public List<object> ClientsInRegion()
        {
            List<Region> rgns = _db.Region.Select(a => new Region { Id = a.Id, Name = a.Name }).ToList();
            int tot = _db.Client.Count();
            List<object> bigArray = new List<object>();
            // object temp=[]
            foreach (var item in rgns)
            {
                ArrayList arrayList = new ArrayList();
                float per0 = _db.Client.Count(t => t.Address.Region.Id == item.Id) / (float)tot;
                arrayList.Add(item.Name);
                arrayList.Add(per0);
                bigArray.Add(arrayList);
            }
            return bigArray;
        }
        public List<object> TypeOfClientsInRegion()
        {
            List<Region> rgns = _db.Region.Select(a => new Region { Id = a.Id, Name = a.Name }).ToList();
            // int tot = _db.Client.Count();
            List<object> bigArray = new List<object>();
            // object temp=[]
            foreach (var item in rgns)
            {
                ArrayList arrayList = new ArrayList();
                float perAPrtmnt = _db.Client.Count(t => t.Address.Region.Id == item.Id && t.CategoryId == 1);// / (float)tot;
                float perRestrnt = _db.Client.Count(t => t.Address.Region.Id == item.Id && t.CategoryId == 2);// / (float)tot;
                arrayList.Add(item.Name);
                arrayList.Add(perAPrtmnt);
                arrayList.Add(perRestrnt);

                bigArray.Add(arrayList);
            }
            return bigArray;
        }

        public ClientConnection AddClientConnection(int ClientID, string ConnectionID)
        {
            ClientConnection con;
            con = _db.ClientConnection.FirstOrDefault(a => a.ClientId == ClientID);
            if (con != null)
            {
                con.ConnectionId = ConnectionID;
               // return existingcon;

            }
            else
            {
                con = new ClientConnection();

                con.ClientId = ClientID;
                con.ConnectionId = ConnectionID;
                _db.ClientConnection.Add(con);
            }
            _db.SaveChanges();
            return con;
        }

        /////All Requests List
        public List<Request> AllRequestsList(int id)
        {
            try
            {
                return _db.Request.Where(x => x.ClientId == id &&
                (((x.IsSeparated != null && (x.OrgaincWeight > 0 || x.NonOrganicWeight > 0)) && x.Schedule.Time < DateTime.Now)
                || x.Schedule.Time > DateTime.Now))
                .Select(x => new Request()
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
                    },
                    Address = new Address()
                    {
                        StreetName = x.Address.StreetName
                         ,
                        Region = new Region()
                        {
                            Id = x.Address.Region.Id,
                            Name = x.Address.Region.Name
                        }
                    }
                    ,
                    Collector = new User()
                    {
                        Id = x.Collector.Id,
                        UserName = x.Collector.UserName
                    }
                }).ToList();
            }
            catch {
                return new List<Request>() { };
            }
            
            }


    }
}
