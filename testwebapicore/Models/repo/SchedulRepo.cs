using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class SchedulRepo
    {
        DateTime today = DateTime.Now.Date;
        WasteAppDbContext db;
        public SchedulRepo( WasteAppDbContext db)
        {
            this.db = db;
        }
        public List<Schedule> GetSchedules()
        {
            return db.Schedule.ToList();
        }
        public Schedule AddSchedule(Schedule s)
        {
             db.Schedule.Add(s);
            db.SaveChanges();
            return (s);
        }
        //
        public Schedule deleteSchedule(int id)
        {
            Schedule s = db.Schedule.FirstOrDefault(a => a.Id == id);
            db.Schedule.Remove(s);
            db.SaveChanges();
            return (s);
        }

        //return schedule object with scheduleId and regionId only for specific driver
        public List<Schedule> getDriverTodaySchedule(int drvId)
        {


            return db.Schedule.Where(b => b.DriverId == drvId && b.Time.Date == today.Date).Select(a => new Schedule { Id = a.Id, RegionId = (int)a.RegionId.Value }).ToList();
        }
        //return list of today schedule
        public List<Schedule> getTodaySchedule()
        {

            return db.Schedule.Where(a => a.Time.Date == today.Date).Select(a => new Schedule { Id = a.Id, Time = a.Time, DriverId = a.DriverId, RegionId = a.RegionId }).ToList();
        }
    }
}
