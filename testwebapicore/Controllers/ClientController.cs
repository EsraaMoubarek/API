using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testwebapicore.Models;
using testwebapicore.Models.repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace testwebapicore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        ClientRepo _db;
        public ClientController(ClientRepo db)
        {
            _db = db;
        }

        [HttpPost]
        public ActionResult RegistrationAprtmnt(Client client)
        {
            if (client != null)
            {
                _db.RegisterApartment(client);

                //CreatedAtction To Go To Home Or Logging First its important
                return Created("successfully Created", client);
            }
            else
            {
                return Content("failed To Register Try Again");
            }

        }
        [HttpPost]
        public ActionResult RegistrationRestrnt(Client client)
        {
            if (client != null)
            {
                _db.RegisterRestrnt(client);

                //CreatedAtction To Go To Home Or Logging First its important
                return Created("successfully Created", client);
            }
            else
            {
                return Content("failed To Register Try Again");
            }

        }
        [HttpPost]
        public IEnumerable<string> test()
        {
            return new string[] { "tessssst", "Jane Doe" };
        }
        public List<ClientCategory> GetCategory()
        {
            return _db.AllCategory();
        }



        //for test Skip it please
        [HttpGet, Authorize]
        //[Route("home")]
        public IEnumerable<string> Get()
        {
            return new string[] { "John Doe", "Jane Doe" };
        }

        ///******** Request Part ********/// 
        /// 
        /// <summary>
        /// ////////////// Requests List 
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("{id}")]
        public ActionResult RequestsList(int id)
        {
            return Ok(_db.RequestsList(id));
        }
        /// 
        /// <summary>
        /// ////////////// New Request 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public ActionResult StartNewRequest(int id)
        {
            bool flag = _db.StartNewRequest(id);
            return Ok(flag);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult getClient(int id)
        {
            Client client = _db.GetClient(id);
            return Ok(client);
        }


        [HttpGet]
       // [Route("newRequest/getRegions")]
        public ActionResult getRegions()
        {
            return Ok(_db.getRegions());
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult getAddresses(int id)
        {
            return Ok(_db.getAddresses(id));
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult getSchedules(int id)
        {
            return Ok(_db.getSchedules(id));
        }

        [HttpPost]
      //  [Route("newRequest/AddNewRequest")]
        public ActionResult AddNewRequest([FromBody] Request request)
        {
            _db.AddNewRequest(request);
            return Ok(request);
        }
        /// <summary>
        /// ////////////// Current Request 
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetCurrentRequest(int id)
        {
            return Ok(_db.GetCurrentRequest(id));
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteRequest(int id)
        {
            return Ok(_db.DeleteRequest(id));
        }


        ///******** Promotion Part ********/// 
        /// 
        /// <summary>
        /// ////////////// Promotions List 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult getPromotions() {

            return Ok(_db.getPromotions());
        }

        [HttpPost]
        public ActionResult AddClientPromotion([FromBody]PromotionCodes clientPromotion) { 
            //clientPromotion.TakeDate = DateTime.Now; 
            
            return Ok(_db.AddClientPromotion(clientPromotion));
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetMyPromotions(int id) {
            return Ok(_db.GetMyPromotions(id));
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult getClientPoints(int id) {
            return Ok(_db.getClientPoints(id));
        }
        ///******** Profile ********/// 

        [HttpGet]
        [Route("{id}")]
        public ActionResult getClientData(int id) {
            return Ok(_db.getClientData(id));
        }

        [HttpGet]
        public ActionResult GetClientCategories() {
            return Ok(_db.GetClientCategories());
        }
        [HttpPut]
        public ActionResult UpdateClient(Client client) {
            _db.UpdateClient(client);
            return Ok();
        }

    }
}