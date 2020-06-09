using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testwebapicore.Models;
using testwebapicore.Models.repo;

namespace testwebapicore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        SurveyRepo _surveyRepo;
        public SurveyController(SurveyRepo surveyRepo)
        {
            _surveyRepo = surveyRepo;   
        }
        public void PostAddSurvey(Survey survey)
        {
            _surveyRepo.AddSurvey(survey);

        } 
    }
}