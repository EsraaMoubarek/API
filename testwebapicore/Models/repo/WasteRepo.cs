using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class WasteRepo
    {
        WasteAppDbContext _db;
        public WasteRepo(WasteAppDbContext db)
        {
            this._db = db;
        }
        public decimal? SelectWasteToCalcAvgTotPrice()
        {
            decimal? totalPrice = 0;
            List<Waste> Wastes = _db.Waste.Select(a => new Waste { Name = a.Name, Price = a.Price, Percent = a.Percent }).ToList();
            foreach (var item in Wastes)
            {
                totalPrice += (item.Percent * item.Price) / 100;
            }
            decimal? AvgTotalPrice = totalPrice / 1000;

            return AvgTotalPrice;
        }

    }
}
