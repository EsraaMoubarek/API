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
    [Produces("application/json")]
    [Route("api/Client")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        RequestRepo db;
        //
        Client client;
        WasteAppDbContext d = new WasteAppDbContext();

        static List<string> s = new List<string>
        {

        };
        //
        public RequestController(RequestRepo db)
        {
            this.db = db;     
        }
        [HttpGet]
        [Route("newRequest/getClient/{id}")]
        public ActionResult getClient(int id)
        {

            client = db.GetClient(id);
            return Ok(client);
        }


        

        [HttpGet]
        [Route("newRequest/getRegions")]
        public ActionResult getRegions()
        {
            return Ok(db.getRegions());
        }
        [HttpGet]
        [Route("newRequest/getAddresses/{id}")]
        public ActionResult getAddresses(int id)
        {
            return Ok(db.getAddresses(id));
        }

        [HttpGet]
        [Route("newRequest/getSchedules/{id}")]
        public ActionResult getSchedules(int id)
        {
            return Ok(db.getSchedules(id));
        }

        //

        [HttpPost]
        [Route("newRequest/AddNewRequest")]
        public ActionResult AddNewRequest([FromBody] Request request)
        {
            db.AddNewRequest(request);
            return Ok(request);
        }

        [HttpGet]
        [Route("testlist")]
        public ActionResult getlist()
        {
            return Ok(s); ;
        }
    }
}