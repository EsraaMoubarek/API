using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace testwebapicore.Models.repo
{
    //esraa
    public class comp_prom_repo
    {
        WasteAppDbContext _db;
        public comp_prom_repo(WasteAppDbContext db)
        {
            this._db = db;
        }

        public ComapnyPromotion addcomp (ComapnyPromotion cp)
        {
            _db.ComapnyPromotion.Add(cp);
            _db.SaveChanges();

            return cp;

        }
        public List<ComapnyPromotion> GetAllCompany()
        {
            return _db.ComapnyPromotion.Select(a => new ComapnyPromotion()
            {
                Id = a.Id,
                Name = a.Name
            }).ToList();
        }


    }
}
