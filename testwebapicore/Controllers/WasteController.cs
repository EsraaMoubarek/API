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
    public class WasteController : ControllerBase
    {
        WasteRepo _wasteRepo;
        public decimal? GetSelectWasteToCalcAvgTotPrice()
        {

            return _wasteRepo.SelectWasteToCalcAvgTotPrice();
        }
        public List<Waste> GetWastesData()
        {
            return _wasteRepo.Wastes();
        }
        
        public ActionResult PostWaste(Waste wste)//edit price
        {

            if (wste != null)
            {
                _wasteRepo.edit(wste);

                //CreatedAtction To Go To Home Or Logging First its important
                return Created("successfully Created", wste);
            }
            else
            {
                return Content("failed To Register Try Again");
            }
        }
    }
}