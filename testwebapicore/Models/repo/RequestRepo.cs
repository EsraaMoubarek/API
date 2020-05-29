using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class RequestRepo
    {
        WasteAppDbContext db;
        public RequestRepo(WasteAppDbContext db)
        {
            this.db = db;
        }
       
        public List<Region> getRegions()
        {

            return db.Region.ToList();
        }

        public List<Address> getAddresses(int id)
        {
            List<Address> addresses = db.Address.Where(a => a.RegionId == id).ToList();
            return (addresses);
        }

        public List<Schedule> getSchedules(int id)
        {

            List<Schedule> schedules = db.Schedule.Where(a => a.RegionId == id).ToList();
            return schedules;
        }
        public Client GetClient(int id)
        {
            Client client = db.Client.SingleOrDefault(a => a.Id == id);
            return client;
        }

        
        public void AddNewRequest(Request request)
        {
            db.Request.Add(request);
            db.SaveChanges();
        }
    }
}
