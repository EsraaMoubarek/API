using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class FeedbackCategoryRepo
    {
        WasteAppDbContext db;
        public FeedbackCategoryRepo(WasteAppDbContext db)
        {
            this.db = db;
        }

        public List<FeedbackCategory> GetAll()
        {
            return db.FeedbackCategory.Select(c => new FeedbackCategory { Id=c.Id,Category=c.Category}).ToList();
        }
    }
}
