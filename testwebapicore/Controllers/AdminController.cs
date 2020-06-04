using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testwebapicore.Models;
using testwebapicore.Models.repo;
using System.IO;


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
       
        
        public AdminController(  SchedulRepo schdb, Sch_col_Repo scoldb, RegionRepo regdb, UserRepo u, AddressRepo addb, comp_prom_repo _comp, Promotions_repo _prom)
        {
            this.schdb = schdb;
            this.scoldb = scoldb;
            this.regdb = regdb;
            this.u = u;
            this.addb = addb;
            this.comp = _comp;
            this.prom = _prom;
        }

        
        [Route("driver")]
        public ActionResult getdrivers()
        {


            return Ok(u.GetUsers().Where(a=>a.Role.Role1=="driver"));

        }
        [Route("collector")]

        public ActionResult getcollector()
        {


            return Ok(u.GetUsers().Where(a => a.Role.Role1 == "collector"));

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
    }
}