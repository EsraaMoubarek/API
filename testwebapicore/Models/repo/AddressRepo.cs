using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class AddressRepo
    {
        WasteAppDbContext db;
        public AddressRepo(WasteAppDbContext db)
        {
            this.db = db;
        }
        public List<Address> GetAddressesByRegion(int regionId)
        {
            return db.Address.Where(a => a.RegionId == regionId).ToList();
        }
    }
}
