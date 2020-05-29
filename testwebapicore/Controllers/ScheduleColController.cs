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
    //[Route("test")]

    [ApiController]
    public class ScheduleColController : ControllerBase
    {
        Sch_col_Repo s;
        public ScheduleColController(Sch_col_Repo s)
        {
            this.s = s;
        }

        [Route("addschcol")]
        [HttpPost]
        public ActionResult add(ScheduleCollector sc)
        {
            
            return Ok(s.AddSchedule_col(sc));
            
        }
        //
        [HttpDelete("{id}")]
        public ActionResult del([FromRoute]int id)
        {

           
            return Ok(s.delsch_col(id));

        }
    }
}