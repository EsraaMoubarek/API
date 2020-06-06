using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class Promotions_repo
    {
        WasteAppDbContext _db;
        public Promotions_repo(WasteAppDbContext db)
        {
            this._db = db;
        }

        public Promotions addprom(Promotions p)
        {
            _db.Promotions.Add(p);
            _db.SaveChanges();

            return p;

        }

    }
}
