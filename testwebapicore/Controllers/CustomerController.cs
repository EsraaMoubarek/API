using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwtcore1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        // GET api/values
        [HttpGet, Authorize(Roles = "apartment")]
  
            public IEnumerable<string> Get()
            {
                return new string[] { "John Doe", "Jane Doe" };
            }

        
    }
}
