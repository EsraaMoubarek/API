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
        }
        public void AddFixedQuestions(List<string> survey)
        {
            DateTime d = DateTime.Today;
            Survey s = _db.Survey.FirstOrDefault(a => a.GenerationDate == d);
            foreach (var item in survey)
            {
                SurveyQuestions survey1 = new SurveyQuestions();
                  survey1.SurveyId = s.Id;
                survey1.ChoiceA = "Not Satisfied";
                survey1.ChoiceB = "partially Satisfied";
                survey1.ChoiceC = "Satisfied";
                survey1.ChoiceD = "More than Satisfied";
                survey1.Question = item;
                _db.SurveyQuestions.Add(survey1);
                _db.SaveChanges();
            }   
        }
            public void AddSurveyQuestions(List<SurveyQuestions> survey)
        {
            DateTime d = DateTime.Today;
          Survey s=  _db.Survey.FirstOrDefault(a => a.GenerationDate == d);
            foreach (var item in survey)
            {
                SurveyQuestions questions = new SurveyQuestions();
                questions.SurveyId = s.Id;
                questions.Question = item.Question/*"hhh"*/;
                questions.ChoiceA = item.ChoiceA/*"hhh"*/;
                questions.ChoiceB = item.ChoiceB /*"hhh"*/;
                questions.ChoiceC = item.ChoiceC /*"hhh"*/;
                _db.SurveyQuestions.Add(questions);
                _db.SaveChanges();
            }
            //foreach (var item in survey)
            //{
            //    _db.SurveyQuestions.Add(item);
            //}
           // _db.SurveyQuestions.AddRange(survey);
            
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
