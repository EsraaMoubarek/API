using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class promcodes_repo
    {
        //esraa


        WasteAppDbContext _db;
        public promcodes_repo(WasteAppDbContext db)
        {
            this._db = db;
        }

       public PromotionCodes addpromcode(PromotionCodes pc)
        {
            _db.PromotionCodes.Add(pc);
            _db.SaveChanges();
            return pc;
        }

    }
}
