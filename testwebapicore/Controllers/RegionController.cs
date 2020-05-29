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
    [Route("Region")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        RegionRepo r;
        public RegionController(RegionRepo r)
        {
            this.r = r;
        }
        public ActionResult getall()
        {
            

            return Ok(r.GetRegions());
           
        }
        public IEnumerable<Region> GetRegion()
        {
            return r.AllRegions();
        }
    }
}