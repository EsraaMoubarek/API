using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testwebapicore.Models.repo;

namespace testwebapicore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        RequestRepo _requestRepo;
        public RequestController(RequestRepo requestRepo)
        {
            _requestRepo = requestRepo;
        }

        [Route("reqstocollecs"), HttpGet]
        public IActionResult GetRequestsToday()
        {
            return Ok(_requestRepo.AssignRequestsToCollectors());
        }
    }
}