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
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        FeedbackRepo fdb;
        public FeedbackController(FeedbackRepo fdb)
        {
            this.fdb = fdb;
        }

        [HttpPost("add")]
        public ActionResult addFeedback(Feedback fb)
        {
            if (fb != null)
            {
                fb.Date = DateTime.Now.Date;
                fb.Id = 0;
                fdb.AddFeedback(fb);
                return Created("sent", fb);
            }
            else
                return BadRequest();
        }

        [HttpGet("getFeedbacks")]
        public ActionResult getFeedbacks()
        {
            return Ok(fdb.GetFeedbacks());
        }
    }
}