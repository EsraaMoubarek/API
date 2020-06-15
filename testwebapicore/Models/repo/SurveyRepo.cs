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
        public void AddSurvey()
        {
            Survey survey = new Survey();
            survey.GenerationDate = DateTime.Today;
            _db.Survey.Add(survey);
            _db.SaveChanges();
           // return survey;
        }
        public void AddSurveyQuestions(List<SurveyQuestions> survey)
        {
            DateTime d = DateTime.Today;
          List<int> s=  _db.Survey.Where(a => a.GenerationDate == d).Select(b => b.Id).ToList();
            foreach (var item in survey)
            {
                item.SurveyId = s[0];
            }
            foreach (var item in survey)
            {
                _db.SurveyQuestions.Add(item);
            }
           // _db.SurveyQuestions.AddRange(survey);
            _db.SaveChanges();
        }
        //public IEnumerable<SurveyQuestions> GetSurvey(int CId)
        //{
        //    List<SurveyQuestions> SurQus;
        //      int SId = _db.Survey.Max(a => a.Id);
        //    ServeyUsers serveyUsers = _db.ServeyUsers.SingleOrDefault(a => a.ClientId == CId && a.SurveyId == SId);
        //    if(serveyUsers == null)
        //    {
        //      SurQus=  _db.SurveyQuestions.Where(a => a.SurveyId == SId).Select(a => new SurveyQuestions { Question = a.Question, ChoiceA = a.ChoiceA, ChoiceB = a.ChoiceB, ChoiceC = a.ChoiceC, ChoiceD = a.ChoiceD }).ToList();
        //    }
        //    else
        //    {
        //        SurQus=
        //    }
        //    return SurQus;
        //}
    }
}
