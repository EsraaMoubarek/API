using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testwebapicore.Models.repo;

namespace testwebapicore.Controllers
{
    
    [ApiController]
    public class DriversController : ControllerBase
    {
        UserRepo u;
        public DriversController(UserRepo u)
        {
            this.u = u;
        }
        [Route("driver")]
        public ActionResult getdrivers()
        {


            return Ok(u.GetUsers().Where(a=>a.RoleId==1));

        }
        [Route("collector")]

        public ActionResult getcollector()
        {


            return Ok(u.GetUsers().Where(a => a.RoleId == 2));

        }
    }
}