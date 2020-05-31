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
    
    [ApiController]
    public class AdminController : ControllerBase
    {
        UserRepo u;
        public AdminController(UserRepo u)
        {
            this.u = u;
        }
        [Route("driver")]
        public ActionResult getdrivers()
        {


            return Ok(u.GetUsers().Where(a=>a.Role.Role1=="driver"));

        }
        [Route("collector")]

        public ActionResult getcollector()
        {


            return Ok(u.GetUsers().Where(a => a.Role.Role1 == "collector"));

        }
        ///
        [Route("adddriver")]
        [HttpPost]
        public ActionResult adddriver(User added)
        {
            added.RoleId = 4;
            //added.Role.Role1 = "driver";

            return Ok(u.addUser(added));

           

        }
        ///
        [Route("addcol")]
        [HttpPost]
        public ActionResult addcol(User added)
        {
            //added.Role.Role1 = "collector";
            added.RoleId = 3;


            return Ok(u.addUser(added));



        }

    }
}