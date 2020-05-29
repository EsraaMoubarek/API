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
    }
}
