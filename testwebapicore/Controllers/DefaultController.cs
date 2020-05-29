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
    public class DefaultController : ControllerBase
    {
        UserRepo userRepo;
        public DefaultController(UserRepo userRepo)
        {
            this.userRepo = userRepo;
        }

        [HttpPost, Route("registerApartment")]
        public ActionResult Registration(Client client)
        {
            if (client != null)
            {
                userRepo.RegisterApartment(client);

                //CreatedAtction To Go To Home Or Logging First its important
                return Created("successfully Created", client);
            }
            else
            {
                return Content("failed To Register Try Again");
            }

        }
    }
}