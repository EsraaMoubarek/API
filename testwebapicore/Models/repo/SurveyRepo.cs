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
        public void AddSurvey(Survey survey)
        {
            survey.GenerationDate = DateTime.Today;
            _db.Survey.Add(survey);
            _db.SaveChanges();
        }
        public void AddSurveyQuestions(List<SurveyQuestions> survey)
        {
          Survey s=  _db.Survey.SingleOrDefault(a => a.GenerationDate == DateTime.Today);
            foreach (var item in survey)
            {
                item.SurveyId = s.Id;
            }
            _db.SurveyQuestions.AddRange(survey);
            _db.SaveChanges();
        }
    }
}
