using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class SurveyRepo
    {
        WasteAppDbContext _db;
        public SurveyRepo(WasteAppDbContext db)
        {
            this._db = db;
        }
        public void RegisterRestrnt(Survey survey)
        {
            _db.Survey.Add(survey);
            _db.SaveChanges();
        }
    }
}
