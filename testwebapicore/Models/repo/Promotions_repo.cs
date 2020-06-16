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
        public Promotions UploadPromotionsImage(string fileName,int id)
        {

            Promotions promotion = _db.Promotions.Find(id);
            promotion.Image = fileName;
            _db.SaveChanges();
            return promotion;
        }

        public List<Promotions> GetPromotionsImage() {
            DateTime date = DateTime.Today.AddDays(-7);
         
            //List<Promotions> promotions = _db.Promotions.Where(x => x.DateFrom >= date
            //&& x.Image != null).ToList();

            return _db.Promotions.Where(x => x.DateFrom >= date && x.Image != null)
                .Select(x=> new Promotions() 
                { Details = x.Details, Image = x.Image,RequiredPoints = x.RequiredPoints }).ToList();
        }
        public bool DeletePromotion(int id) {
            List<PromotionCodes> promotionCodes = _db.PromotionCodes
                .Where(x => x.PromtionId == id && x.ClientId != null).ToList();
            if (promotionCodes.Count > 0)
            {
                return false;
            }
            else
            {
                Promotions promotion = _db.Promotions.Find(id);
                _db.Promotions.Remove(promotion);
                _db.SaveChanges();
                return true;
            }
        }
    }
}
