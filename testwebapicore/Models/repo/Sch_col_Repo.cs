using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class Sch_col_Repo
    {
        WasteAppDbContext db;
        public Sch_col_Repo(WasteAppDbContext db)
        {
            this.db = db;
        }
        
        public ScheduleCollector AddSchedule_col(ScheduleCollector s)
        {
            
            
                db.ScheduleCollector.Add(s);
                db.SaveChanges();
            
            return (s);
        }
        ///
        public ScheduleCollector delsch_col(int id)
        {
            var res = db.ScheduleCollector.Select(n => n).Where(n => n.ScheduleId == id);
            foreach (var item in res)
            {
                db.ScheduleCollector.Remove(item);

            }
            db.SaveChanges();
            return (res.FirstOrDefault());
        }

        //return list of ScheduleCollector objects for specific scedule 
        public List<ScheduleCollector> getByScheduleId(int id)
        {
            return db.ScheduleCollector.Where(s => s.ScheduleId == id).Select(a => new ScheduleCollector { ScheduleId = a.ScheduleId, CollectorId = a.CollectorId }).ToList();
        }
    }
}
