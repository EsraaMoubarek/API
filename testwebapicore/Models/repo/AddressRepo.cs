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
            return db.Address.Where(a => a.RegionId == regionId).Select(a => new Address()
            {
                Id = a.Id,
                StreetName = a.StreetName
            }).ToList();
        }
        
        public List<string> streets(int RId)
        {
            List<string> Strs = db.Address.Where(b => b.RegionId == RId).Select(a => a.StreetName).ToList();
            return Strs;
        }
        public IEnumerable<Address> AddressIdAndStrs(int rId)
        {
            List<Address> add = db.Address.Where(a => a.RegionId == rId).Select(b => new Address { Id = b.Id, StreetName = b.StreetName }).ToList();//.sin
            return add;
        }
        public int AddressId(string stN, int rId)
        {
            List<int> add = db.Address.Where(a => a.StreetName == stN & a.RegionId == rId).Select(b => b.Id).ToList();//.sin
            int Addr = add[0];                                                                                    // SingleorDefault(b => b.Id);
            return Addr;
            //Address add = _db.Address.SingleOrDefault(a => a.StreetName == stN & a.RegionId == rId);//.sin
            //int Addr = add.Id;                                                                                    // SingleorDefault(b => b.Id);
            //return Addr;

        }
    }
}
