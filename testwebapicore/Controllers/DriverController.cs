using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Namotion.Reflection;
using testwebapicore.Models;

namespace WasteAppCoreDriver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        WasteAppDbContext db;
        public DriverController(WasteAppDbContext _db)
        {
            this.db = _db;    
        }

        [HttpGet("getDriverSchedule/{driverId}")]
        public ActionResult getDriverSchedule(int driverId)
        {
            
            DateTime today = DateTime.Now.Date;
           
            List<Schedule> scheduleIDs =db.Schedule.Where(b => b.DriverId == driverId && b.Time.Date == today.Date).Select(a =>new Schedule { Id = a.Id, RegionId = (int)a.RegionId.Value}).ToList();
            
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
                addresses.Add(db.Request.Where(x => x.ScheduleId == schId)
                    .Select(a => new  { BuildingNumber =(int) a.BuildingNumber, AddressId = (int)a.AddressId,  }).ToList<object>());
               
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

                for(int x=0;x<adds.Count;x++)
                {
                System.Type type = adds[x].GetType();

                int addId = (int)type.GetProperty("AddressId").GetValue(adds[x]);
                int regId = db.Address.FirstOrDefault(l => l.Id == addId).RegionId;
                regions.Add(db.Region.Where(a => a.Id == regId ).Select(b=>new  { RegionId = b.Id, NameArabic= b.NameArabic }));
                    
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

                todayAddresses.Add(db.Address.Where(a => a.Id == addId).Select(b => new { AddressId = b.Id, StreetNameArabic = b.StreetNameArabic, Latitude = b.Latitude, Longitude = b.Longitude,BuildingNo=buildNo, RegionInfo=regions[x] }).First());
                                              
            }
            todayAddresses.RemoveAt(0);

           return Ok(todayAddresses);

        }
    }
}