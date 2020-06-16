using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testwebapicore.Models;
using testwebapicore.Models.repo;
using System.IO;
using System.Net.Http.Headers;

namespace testwebapicore.Controllers
{
    
    [ApiController]
    public class AdminController : ControllerBase
    {
        SchedulRepo schdb;
        Sch_col_Repo scoldb;
        RegionRepo regdb;
        AddressRepo addb;
        UserRepo u;
        comp_prom_repo comp;
        Promotions_repo prom;
        promcodes_repo code;
       
        
        public AdminController(SchedulRepo schdb, Sch_col_Repo scoldb, RegionRepo regdb, UserRepo u, AddressRepo addb, comp_prom_repo _comp, Promotions_repo _prom,promcodes_repo _code)
        {
            this.schdb = schdb;
            this.scoldb = scoldb;
            this.regdb = regdb;
            this.u = u;
            this.addb = addb;
            this.comp = _comp;
            this.prom = _prom;
            this.code = _code;
        }

        
        [Route("driver")]
        public ActionResult getdrivers()
        {


            return Ok(u.GetUsers().Where(a=>a.Role.Role1=="driver")
                .Select(x=> new User() {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    RoleId = x.RoleId,
                    PhoneNumber = x.PhoneNumber,
                    Role = new Role() {
                        Id = x.Role.Id,
                        Role1 = x.Role.Role1
                    }
                }));
        }
        [Route("collector")]

        public ActionResult getcollector()
        {


            return Ok(u.GetUsers().Where(a => a.Role.Role1 == "collector").Select(x => new User()
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                RoleId = x.RoleId,
                PhoneNumber = x.PhoneNumber,
                Role = new Role()
                {
                    Id = x.Role.Id,
                    Role1 = x.Role.Role1
                }
            }));

        }
        ///
        [Route("adddriver")]
        [HttpPost]
        public ActionResult adddriver(User added)
        {
            added.RoleId = 4;
            //added.Role.Role1 = "driver";

            return Ok(u.addUser(added));

        }
        ///
        [Route("addcol")]
        [HttpPost]
        public ActionResult addcol(User added)
        {
            //added.Role.Role1 = "collector";
            added.RoleId = 3;


            return Ok(u.addUser(added));

        }
        //Esraa
        [Route("addcomp")]
        [HttpPost]
        public ActionResult addcomp(ComapnyPromotion cp)
        {
             //= new ComapnyPromotion();
            //cp.Name = ncp.Name;
            //IFormFile pic = ncp.Logo;

            //using (MemoryStream ms = new MemoryStream())
            //{
            //    pic.CopyTo(ms);
            //    cp.Logo = ms.ToArray();
            //}


            return Ok(comp.addcomp(cp));

        }
        [Route("addprom")]
        [HttpPost]
        public ActionResult addpromotion(Promotions p)
        {
           


            return Ok(prom.addprom(p));

        }
        [Route("getcomp")]
        [HttpGet]
        public ActionResult getcomp()
        {



            return Ok(comp.GetAllCompany());

        }
        [Route("addcode")]
        [HttpPost]
        public ActionResult addcode(PromotionCodes pc)
        {



            return Ok(code.addpromcode(pc));

        }

        ///

        [HttpGet("api/admin/getAdminSchedule")]
        public ActionResult getAdminSchedule()
        {

            List<Schedule> schedule = schdb.getTodaySchedule();
            List<List<ScheduleCollector>> schCollectors = new List<List<ScheduleCollector>>();
            foreach (var item in schedule)
            {
                schCollectors.Add(scoldb.getByScheduleId(item.Id));

            }
            var collcrsNames = new List<List<string>>();

            for (int x = 0; x < schCollectors.Count(); x++)
            {
                var collist = new List<string>();
                for (int y = 0; y < schCollectors[x].Count(); y++)
                {

                    collist.Add(u.getById(schCollectors[x][y].CollectorId).UserName);

                }

                collcrsNames.Add(collist);

            }
            var regions = new List<string>();
            var drivers = new List<User>();
            var lats = new List<double>();
            var lngs = new List<double>();
            foreach (var item in schedule)
            {
                regions.Add(regdb.getById((int)item.RegionId).Name);
                drivers.Add(u.getById((int)item.DriverId));
                var x = (int)item.RegionId;
                var y = (int)item.RegionId;
                lats.Add((float)addb.getByRegionId(x).Latitude);
                lngs.Add((float)addb.getByRegionId(y).Longitude);

            }
            var todaySchedule = new List<object>
            {
                new
                {
                    Region="",
                    Driver="",
                    CollecList="",
                    lat=0,
                    lng=0,
                },
            };
            for (int x = 0; x < schedule.Count(); x++)
            {
                todaySchedule.Add(new
                {
                    Region = regions[x],
                    Driver = drivers[x].UserName,
                    CollecList = collcrsNames[x],
                    lat = lats[x],
                    lng = lngs[x]
                });
            }
            todaySchedule.RemoveAt(0);
            return Ok(todaySchedule);
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("api/admin/UploadPromotionsImage/{id}")]
        public IActionResult UploadPromotionsImage(int id)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                string str = guid.ToString();

                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue
                        .Parse(file.ContentDisposition).FileName.Trim('"').ToString();

                    fileName = Path.GetFileNameWithoutExtension(fileName) + str
                        + Path.GetExtension(fileName);

                    var fullPath = Path.Combine(pathToSave, fileName);

                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(prom.UploadPromotionsImage(fileName,id));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        [Route("api/admin/GetPromotionsImage")]
        public IActionResult GetPromotionsImage() {
            try
            {
                return Ok(prom.GetPromotionsImage());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}