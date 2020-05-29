using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class RegionRepo

    {
        WasteAppDbContext db;
        public RegionRepo(WasteAppDbContext db)
        {
            this.db = db;
        }
        public List<Region> GetRegions()
        {
            return db.Region.ToList();
        }
        public Region GetRegionById(int regionId)
        {
            return db.Region.SingleOrDefault(r => r.Id == regionId);
        }
        public List<Region> AllRegions()
        {
            List<Region> Rgns = db.Region.Select(a => new Region { Id = a.Id, Name = a.Name }).ToList();
            return Rgns;
        }

        //return object with regionId and nameArabic
        public object getObjectById(int id)
        {
            return db.Region.Where(a => a.Id == id).Select(b => new { RegionId = b.Id, NameArabic = b.NameArabic });
        }
        //return region name only
        public Region getById(int id)
        {
            return db.Region.Where(a => a.Id == id).Select(b => new Region { Name = b.Name }).First();
        }
    }
}
