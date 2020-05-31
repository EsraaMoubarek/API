using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using testwebapicore.Models;
using testwebapicore.Models.repo;

namespace testwebapicore.Controllers
{
    [Route("api/[controller]/[action]")]
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

        [Route("/api/[controller]/addrsbyreg")]
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
       
        public IEnumerable<string> GetStreets(int Id)
        {
            return _addressRepo.streets(Id);
        }
        public IEnumerable<Address> GetAddress(int Id)
        {
            return _addressRepo.AddressIdAndStrs(Id);
        }
        public int GetAddressId(int rId, string stN)
        {
            return _addressRepo.AddressId(stN, rId);
        }
    }
}