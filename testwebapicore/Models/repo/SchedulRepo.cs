using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class SchedulRepo
    {
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
    }
}
