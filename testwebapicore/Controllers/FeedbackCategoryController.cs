using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testwebapicore.Models.repo;

namespace testwebapicore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackCategoryController : ControllerBase
    {
        FeedbackCategoryRepo catdb;
        public FeedbackCategoryController(FeedbackCategoryRepo catdb)
        {
            this.catdb = catdb;
        }

        [HttpGet("getAll")]
        public ActionResult getAll()
        {
            return Ok(catdb.GetAll());
        }
    }
}