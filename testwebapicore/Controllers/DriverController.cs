using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Namotion.Reflection;
using testwebapicore.Models;
using testwebapicore.Models.repo;

namespace WasteAppCoreDriver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {

        SchedulRepo schdb;
        AddressRepo addb;
        RegionRepo regdb;
        RequestRepo reqdb;


        public DriverController(SchedulRepo schdb, AddressRepo addb, RegionRepo regdb, RequestRepo reqdb)
        {
            this.schdb = schdb;
            this.addb = addb;
            this.regdb = regdb;
            this.reqdb = reqdb;


        }
        [HttpGet("test")]
        public IEnumerable<string> test()
        {
            return new string[] { "John Doe", "Jane Doe" };
        }


        [HttpGet("getDriverSchedule/{driverId}")]
        public ActionResult getDriverSchedule(int driverId)
        {
            DateTime today = DateTime.Now.Date;
            List<Schedule> scheduleIDs = schdb.getDriverTodaySchedule(driverId);

            List<List<object>> addresses = new List<List<object>> {
            new List<object>
            {
                new
                {
                     BuildingNumber=0,
                     AddressId=0,
                }

            },
            };

            for (int i = 0; i < scheduleIDs.Count(); i++)
            {
                System.Type type = scheduleIDs[i].GetType();
                int schId = (int)type.GetProperty("Id").GetValue(scheduleIDs[i]);
                addresses.Add(reqdb.getListByScheduleId(schId));

            }
            addresses.RemoveAt(0);

            List<object> adds = new List<object>();
            for (int x = 0; x < addresses.Count; x++)
            {
                for (int y = 0; y < addresses[x].Count; y++)
                {
                    adds.Add(addresses[x][y]);
                }
            }

            List<object> regions = new List<object>
            {
               new { RegionId=0,
               NameArabic="",
               },
            };

            for (int x = 0; x < adds.Count; x++)
            {
                System.Type type = adds[x].GetType();

                int addId = (int)type.GetProperty("AddressId").GetValue(adds[x]);
                int regId = addb.getById(addId).RegionId;
                regions.Add(regdb.getObjectById(regId));

            }
            regions.RemoveAt(0);

            var todayAddresses = new List<object> {
              new{
                   AddressId=0,
                   StreetNameArabic="street",
                   Latitude=00.0 ,
                   Longitude=0.00 ,
                   buildNo=0,
                   RegionInfo="region"

              }, };



            for (int x = 0; x < adds.Count(); x++)
            {
                System.Type type = adds[x].GetType();
                int addId = (int)type.GetProperty("AddressId").GetValue(adds[x]);
                int buildNo = (int)type.GetProperty("BuildingNumber").GetValue(adds[x]);
                object regn = regions[x];
                todayAddresses.Add(addb.getObjectById(addId, buildNo, regn));

            }
            todayAddresses.RemoveAt(0);

            return Ok(todayAddresses);

        }
    }
}