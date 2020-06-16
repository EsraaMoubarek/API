using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class RequestRepo
    {
        WasteAppDbContext _db;
        public RequestRepo(WasteAppDbContext db)
        {
            _db = db;
        }

        //arrange requests by building number
        public List<Request> ArrangeRequests(List<Request> todayRequests)
        {
            foreach (var item in todayRequests)
            {
                if (item.BuildingNumber == null)
                    item.BuildingNumber = item.Client.BuildingNumber;
            }

            List<Request> orderedRequests = todayRequests.OrderBy(r => r.BuildingNumber).ToList();

            return orderedRequests;
        }

        public int AssignRequestsToCollectors()
        {
            try
            {
                List<Request> requests = _db.Request.Where(r => r.Schedule.Time.Date == DateTime.Now.Date).ToList();

                List<Region> regions = requests.DistinctBy(r => r.Schedule.RegionId).Select(r => new Region()
                {
                    Id = (int)r.Schedule.RegionId,
                    Name = r.Schedule.Region.Name
                }).ToList();

                List<Request> arrangedrequests;
                int numOfCollectors = 0;
                int numOfRequests = 0;
                int numOfRequestsPerCollector = 0;
                List<ScheduleCollector> collectors = new List<ScheduleCollector>();
                Schedule schedule;
                int scheduleId;
                int counter;
                int collectorFlag;
                double[] temp = new double[2];
                int tempCounter = 0;

                foreach (var item in regions)
                {
                    schedule = _db.Schedule.SingleOrDefault(s => s.RegionId == item.Id && s.Time.Date == DateTime.Now.Date);
                    if (schedule != null)
                    {
                        //get number of collectors per region
                        scheduleId = schedule.Id;
                        collectors = _db.ScheduleCollector.Where(s => s.ScheduleId == scheduleId).ToList();
                        numOfCollectors = collectors.Count;
                        //numOfCollectors = _context.ScheduleCollector.Where(s => s.ScheduleId == scheduleId).Count();


                        arrangedrequests = new List<Request>(ArrangeRequests(requests.Where(r => r.Schedule.RegionId == item.Id).ToList()).Select(r => new Request()
                        {
                            Id = r.Id,
                            ApartmentNumber = r.ApartmentNumber,
                            ClientId = r.ClientId,
                            ScheduleId = r.ScheduleId,
                            BuildingNumber = r.BuildingNumber,
                            Points = r.Points,
                            OrgaincWeight = r.OrgaincWeight,
                            NonOrganicWeight = r.NonOrganicWeight,
                            AddressId = r.AddressId,
                            CollectorId = r.CollectorId,
                            Rate = r.Rate,
                            IsSeparated = r.IsSeparated
                        }));
                        numOfRequests = arrangedrequests.Count;
                        if (numOfCollectors != 0)
                        {
                            numOfRequestsPerCollector = numOfRequests / numOfCollectors;
                            temp[tempCounter] = numOfRequestsPerCollector;
                            tempCounter++;

                            counter = 0;
                            collectorFlag = 0;
                            foreach (var request in arrangedrequests)
                            {
                                if (counter % numOfRequestsPerCollector == 0 && counter != 0)
                                {
                                    counter = 0;
                                    collectorFlag++;
                                }
                                if (collectorFlag > numOfCollectors - 1)
                                    collectorFlag = 0;
                                //request.CollectorId = collectors[collectorFlag].CollectorId;
                                _db.Request.FirstOrDefault(r => r.Id == request.Id).CollectorId = collectors[collectorFlag].CollectorId;
                                counter++;
                            }
                        }
                    }
                    
                }

                _db.SaveChanges();
                //requests = _db.Request.Where(r => r.Schedule.Time.Date >= DateTime.Now.Date)
                //    .Select(r => new Request()
                //    {
                //        Id = r.Id,
                //        ClientId = r.ClientId,
                //        CollectorId = r.CollectorId,
                //        ScheduleId = r.ScheduleId
                //    }).ToList();

                //Console.WriteLine("Assign Requests to Collectors");
                return 1;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                return 0;
            }

            //return requests;
        }

        //return object of addressId and buildingNo only
        public List<object> getListByScheduleId(int schId)
        {
            return _db.Request.Where(x => x.ScheduleId == schId).Select(a => new { BuildingNumber = (int)a.BuildingNumber, AddressId = (int)a.AddressId, }).ToList<object>();
        }

    }
}
