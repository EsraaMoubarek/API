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
    [Route("home")]
    [ApiController]
    public class SchduleController : ControllerBase
    {
        SchedulRepo s;
        public SchduleController(SchedulRepo s)
        {
            this.s = s;
        }
        public ActionResult getall()
        {
            //List<Schedule> lst = s.GetSchedules();

            return Ok(s.GetSchedules());
            //return Content();
        }
        [Route("add")]
        [HttpPost]
        public ActionResult add(Schedule sc)
        {
            //List<Schedule> lst = s.GetSchedules();
            //return Content("add");
            return Ok(s.AddSchedule(sc));
            //return Content();
        }
        ///
        ///[Route("del")]
        [HttpDelete("{id}")]
        public ActionResult del([FromRoute]int id)
        {
            return Ok(s.deleteSchedule(id));
        }

        //[HttpGet]
        [Route("monthlyschedulebyregion")]
        public IActionResult GetMonthlySchedule(int regionId)
        {
            return Ok(s.MonthlyScheduleByRegion(regionId));
        }


    }
}