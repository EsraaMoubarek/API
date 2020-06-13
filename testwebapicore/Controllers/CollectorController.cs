using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testwebapicore.Models;
using testwebapicore.Models.repo;

namespace testwebapicore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectorController : ControllerBase
    {
        
        UserRepo _db;
        public CollectorController(UserRepo db)
        {
            _db = db;
        }

        [HttpGet]
        //[Authorize]
        [Route("clientlist")]
        public ActionResult ClientList(int CollectorID)
        {
            List<ClientListModel> clientLists = new List<ClientListModel>();
            // return Content("IN Action");
            if (CollectorID != 0)
            {
                return Ok(_db.GetClientList(CollectorID));
            }
            else
            {
                return Content("NO Clients Today");
            }

        }

        //public IEnumerable<string> test()
        //{
        //    return new string[] { "tessssst", "Jane Doe" };

        //}
        [/*Authorize,*/ HttpGet, Route("weight")]
        public ActionResult AddWeight(int ClientID, int OrgaincWeight, int NonOrganicWeight, int ScheduleID, bool? IsSeparated)
        {
            int reqId =
          _db.AddWeight(ClientID, OrgaincWeight, NonOrganicWeight, ScheduleID, IsSeparated);
            int reqID = reqId;
            _db.AddPoints(ClientID, NonOrganicWeight, reqId);
            return Ok(reqId);
        }
        [ HttpGet, Route("profile")]
        public ActionResult CollectorProfile(int CollectorID)
        {
            return Ok(_db.CollectorProfile(CollectorID));
        }
    }
}