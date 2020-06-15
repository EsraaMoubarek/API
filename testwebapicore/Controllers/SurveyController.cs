using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using testwebapicore.Models;
using testwebapicore.Models.repo;

namespace testwebapicore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        SurveyRepo _surveyRepo; WasteAppDbContext _db;
        public SurveyController(SurveyRepo surveyRepo, WasteAppDbContext db)
        {
            _surveyRepo = surveyRepo;
            _db = db;
        }
        [HttpGet]
        //public void Getsurvey()
        //{
          //Survey survey=  _surveyRepo.AddSurvey();
          //  return survey;
      // return Ok(survey);
       // }
        [HttpPost]
        public void PostAddSurvey(List<SurveyQuestions> sq)
        {
            _surveyRepo.AddSurvey();
            _surveyRepo.AddSurveyQuestions(sq);
           // return Ok(sq);
        } 
        public IActionResult GetSurvey(int CId)
        {
            List<SurveyQuestions> SurQus;
            int SId = _db.Survey.Max(a => a.Id);
            ServeyUsers serveyUsers = _db.ServeyUsers.FirstOrDefault(a => a.ClientId == CId && a.SurveyId == SId);
            if (serveyUsers == null)
            {
                SurQus = _db.SurveyQuestions.Where(a => a.SurveyId == SId).Select(a => new SurveyQuestions { Question = a.Question, ChoiceA = a.ChoiceA, ChoiceB = a.ChoiceB, ChoiceC = a.ChoiceC, ChoiceD = a.ChoiceD }).ToList();
                return Ok(SurQus);
            }
            else
            {
                return NoContent();
            }
          
        }
        [HttpPost]
        public IActionResult PostSurvUsers(List<string>ans)
        {
            List<ServeyUsers> servU = new List<ServeyUsers>();
            int SId = _db.Survey.Max(a => a.Id);
          List<int>  ques = _db.SurveyQuestions.Where(a => a.SurveyId == SId).Select(a =>a.Id).ToList();
            for (int i=0; i<ans.Count;i++)
            {
                ServeyUsers su = new ServeyUsers();
                su.SurveyId = SId;
                su.QuestionId = ques[i];
                su.Answer = ans[i];
                su.ClientId = 8;
                servU.Add(su);
            }

            _db.ServeyUsers.AddRange(servU);
            _db.SaveChanges();
            return Created("ay7aga", 201); 
        }
    }
}