using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testwebapicore.Models.repo;

namespace testwebapicore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        UserRepo _UserRepo;
        ClientRepo _clientRepo;
        public ManagerController(UserRepo UserRepo,ClientRepo ClientRepo)
        {
            _UserRepo = UserRepo; _clientRepo = ClientRepo;
        }
        public List<object> GetClientInRegion()
        {
            return _clientRepo.ClientsInRegion();
        }
        public List<object> GetTypeOfClientsInRegion()
        {
            return _clientRepo.TypeOfClientsInRegion();
        }
    }
}