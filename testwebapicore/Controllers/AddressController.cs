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
    public class AddressController : ControllerBase
    {
        AddressRepo _addressRepo;
        RegionRepo _regionRepo;
        public AddressController(AddressRepo addressRepo, RegionRepo regionRepo)
        {
            _addressRepo = addressRepo;
            _regionRepo = regionRepo;
        }

        [Route("addrsbyreg")]
        public IActionResult GetAddressByRegionId(int regionId)
        {
            Region region = _regionRepo.GetRegionById(regionId);
            if (region != null)
            {
                List<Address> addresses = _addressRepo.GetAddressesByRegion(regionId);
                if (addresses.Count != 0)
                    return Ok(addresses);
                else
                    return NotFound("No availabe addresses in this region");
            }
            else
                return NotFound("This Region was not found");
        }
    }
}