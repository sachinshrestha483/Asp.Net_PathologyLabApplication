using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LabReport2.Models;
using LabReport2.ViewModels;
using Newtonsoft.Json;

namespace LabReport2.Controllers
{
    [Authorize(Roles=RoleName.CanManageEverything)]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        public AdminController()//constructor
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Admin
        public ActionResult Index()
        {
            var numberOfReportThisMonth = _context.Reports.Where(r => r.Date.Year == DateTime.Now.Year).Where(r=>r.Date.Month==DateTime.Now.Month).Count();
            var numberOfReportToday = _context
                .Reports
                .Where(r => r.Date.Year == DateTime.Now.Year)
                .Where(r=>r.Date.Month==DateTime.Now.Month)
                .Where(r=>r.Date.Day==DateTime.Now.Day)
                .ToList()
                .Count();
            var numberOfReportPreviousMonth = _context.Reports.Where(r => r.Date.Year == DateTime.Now.Year).Where(r => r.Date.Month == DateTime.Now.Month-1).Count();

            var lastMonthSale = _context.Reports
                .Where(r => r.Date.Year == DateTime.Now.Year)
                .Where(r => r.Date.Month == DateTime.Now.Month-1)
                .Select(r => r.Price)
                .ToList()
                .Sum();

            var lastMonthDiscount = _context.Bills
                .Where(b => b.Date.Year == DateTime.Now.Year)
                .Where(b => b.Date.Month == DateTime.Now.Month-1)
                .Select(b => b.Discount)
                .ToList()
                .Sum();

            var lastMonthRevenue = lastMonthSale - lastMonthDiscount;


            var currentMonthSale = _context.Reports
                .Where(r => r.Date.Year == DateTime.Now.Year)
                .Where(r => r.Date.Month == DateTime.Now.Month)
                .Select(r => r.Price)
                .ToList()
                .Sum();
            var currentMonthDiscount = _context.Bills
                .Where(b => b.Date.Year == DateTime.Now.Year)
                .Where(b => b.Date.Month == DateTime.Now.Month)
                .Select(b => b.Discount)
                .ToList()
                .Sum();

            var currentMonthRevenue = currentMonthSale - currentMonthDiscount;

            var viewModel = new AdminViewModel
            {
                CurrentMonthRevenue =currentMonthRevenue,
                LastMonthRevenue = lastMonthRevenue,
                NumberOfReportPreviousMonth =numberOfReportPreviousMonth,
                NumberOfReportThisMonth = numberOfReportThisMonth,
                NumberofRerportToday = numberOfReportToday

            };
            return View(viewModel);
        }

        public ActionResult RevenueCalCulator(DateTime startDate, DateTime endDate)
        {
           
               int Sales = _context.Reports
                               .Where(r => r.Date >= startDate)
                               .Where(r => r.Date <= endDate)
                               .Select(r => r.Price)
                               .ToList()
                               .Sum();
                 int Discount = _context.Bills
                     .Where(r => r.Date >= startDate)
                    .Where(r => r.Date <= endDate)
                    .Select(b => b.Discount)
                    .ToList()
                    .Sum();


            
           
            int revenue = Sales - Discount;

            //var currentMonthSale = _context.Reports
            //    .Where(r => r.Date>= startDate)
            //    .Where(r=>r.Date<=endDate)
            //    .Select(r => r.Price)
            //    .Sum();
            //var currentMonthDiscount = _context.Bills
            //     .Where(r => r.Date >= startDate)
            //    .Where(r => r.Date <= endDate)
            //    .Select(b => b.Discount)
            //    .Sum();

            //var currentMonthRevenue = currentMonthSale - currentMonthDiscount;
            var viewModel = new RevenueCalculatorViewModel { Revenue = revenue, StartDate = startDate, EndDate = endDate };
            return View(viewModel);
        }
        public ActionResult RevenueDetalis(RevenueCalculatorViewModel revenueCalculatorViewModel)
        {
            var startDate = revenueCalculatorViewModel.StartDate;
            var endDate = revenueCalculatorViewModel.EndDate;

            //            var numberOfReports, number of patients, try to get graph of report numbers with nme,most referred doctor, and no of refered by him 
            var reports = _context.Reports
                .Where(r => r.Date >= startDate)
                .Where(r => r.Date <= endDate)
                .Include(r => r.Patient)
                .Include(r => r.Doctor)
                .Include(r => r.TestTitle);

            int Sales = reports 
                .Select(r => r.Price)
                .ToList()
                .Sum();
            int Discount = _context.Bills
                .Where(r => r.Date >= startDate)
                .Where(r => r.Date <= endDate)
                .Select(b => b.Discount)
                .ToList()
                .Sum();




            int revenue = Sales - Discount;

            






            var numberofReports = reports        
                                .Select(r => r.Id)
                                .ToList()
                                .Count();

            var numberofPatients =reports .Select(r => r.PatientId)
                                          .ToHashSet()
                                          .ToList()
                                          .Count();


            var testTitleIds = reports
                                         .Where(r => r.Date >= startDate)
                                         .Where(r => r.Date <= endDate)
                                         .Select(r => r.TestTitle.Id)
                                         .ToList();
            var uniqueTestTitleIds = testTitleIds.ToHashSet();

            var uniquetestTitleCounts = new List<int>();
            foreach (var testTitleid in uniqueTestTitleIds)
            {
                var count = testTitleIds.Where(t => t == testTitleid).ToList().Count();
                uniquetestTitleCounts.Add(count);
            }
            var uniqueTestTitles = new List<TestTitle>();
            foreach (var id in uniqueTestTitleIds)
            {
                var testTitle = _context.TestTitles.Single(t => t.Id == id);
                uniqueTestTitles.Add(testTitle);
            }

            var doctorIdUniqueList = reports.Select(r => r.DoctorId).ToHashSet().ToList();
            var doctorsList = reports.Select(r => r.DoctorId).ToList();

            var doctorRefCounts = new List<int>();
            foreach (var doctor in  doctorIdUniqueList )
            {
                var count = doctorsList.Where(t => t == doctor).ToList().Count();
                doctorRefCounts.Add(count);
            }
            var doctorsUniqueList = new List<Doctor>();
            foreach (var doctorId in doctorIdUniqueList)
            {
                var doctor = _context.Doctors.Single(d => d.Id == doctorId);
                doctorsUniqueList.Add(doctor);
            }
            var uniqueDates = new List<DateTime>();

            for (var sd = startDate; sd<= endDate; sd = sd.AddDays(1))
            {
                uniqueDates.Add(sd);
            }

            var dateReportCounts = new List<int>();
            foreach (var date in uniqueDates)
            {
                var count = reports.Where(r => r.Date == date).ToList().Count();
                dateReportCounts.Add(count);

            }

            List<DataPoint> dataPoints = new List<DataPoint>();
            for (int i = 0; i < uniqueTestTitles.Count(); i++)
            {
                dataPoints.Add(new DataPoint(uniqueTestTitles[i].Name,uniquetestTitleCounts[i] ));

            }
            List<DataPoint> dataPoints1 = new List<DataPoint>();
            for (int i = 0; i < uniqueDates.Count(); i++)
            {
                dataPoints1.Add(new DataPoint(string.Format("{0:d MMM yyyy}", uniqueDates[i]),dateReportCounts[i] ));

            }
            List<DataPoint> dataPoints2 = new List<DataPoint>();

            for (int i = 0; i < doctorsUniqueList.Count(); i++)
            {
                dataPoints2.Add(new DataPoint(doctorsUniqueList[i].Name, doctorRefCounts[i]));
            }

            var uniqueDateRevenues = new List<int>();
            foreach (var date in uniqueDates)
            {
                var saleOnDate = reports.Where(r => r.Date == date).Select(r=>r.Price).ToList().Sum();
                var discountOnDate = _context.Bills.Where(b => b.Date == date).Select(b => b.Discount).ToList().Sum();
                var revenueOnDate = saleOnDate - discountOnDate;
                uniqueDateRevenues.Add(revenueOnDate);

            }
            List<DataPoint> dataPoints3 = new List<DataPoint>();

            for (int i = 0; i < uniqueDates.Count(); i++)
            {
                dataPoints3.Add(new DataPoint(string.Format("{0:d MMM yyyy}", uniqueDates[i]), uniqueDateRevenues[i]));
            }


            //string.Format("{0:d MMM yyyy}", item.Report.Date)
            //dataPoints.Add(new DataPoint("Albert", 10));
            //dataPoints.Add(new DataPoint("Tim", 30));
            //dataPoints.Add(new DataPoint("Wilson", 17));
            //dataPoints.Add(new DataPoint("Joseph", 39));
            //dataPoints.Add(new DataPoint("Robert", 30));
            //dataPoints.Add(new DataPoint("Sophia", 25));
            //dataPoints.Add(new DataPoint("Emma", 15));

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);
            ViewBag.DataPoints3 = JsonConvert.SerializeObject(dataPoints3);



         var billItems = _context.BillItems
        .Include(t => t.Report)
        .Include(t => t.Report.Doctor)
        .Include(t => t.Report.Patient)
        .Include(t => t.Report.TestTitle)
        .Include(t => t.Report.ConsultingPathologist)
         .Where(t => t.Report.Date >= startDate)
         .Where(r => r.Report.Date <= endDate)
        .ToList().OrderByDescending(t => t.BillId).ToList();



            var viewModel = new RevenueDetailsViewModel
            {
                UniqueDoctorRefCounts = doctorRefCounts,
                EndDate = endDate,
                NumberOfPatients = numberofPatients,
                NumberOfReports = numberofReports,
                Revenue = revenue,
                StartDate = startDate,
                UniqueDates = uniqueDates,
                Billitems = billItems



            };
            return View(viewModel);
        }

        //public ActionResult DailyReport()
        //{
        //    //var startDate = DateTime.Now;
        //    //var endDate = DateTime.Now;


        //    //int Sales = _context.Reports
        //    //                      .Where(r => r.Date >= startDate)
        //    //                      .Where(r => r.Date <= endDate)
        //    //                      .Select(r => r.Price)
        //    //                      .ToList()
        //    //                      .Sum();
        //    //int Discount = _context.Bills
        //    //    .Where(r => r.Date >= startDate)
        //    //   .Where(r => r.Date <= endDate)
        //    //   .Select(b => b.Discount)
        //    //   .ToList()
        //    //   .Sum();



        //    //int revenue = Sales - Discount;
        //    //var viewModel = new RevenueCalculatorViewModel { Revenue = revenue, StartDate = startDate, EndDate = endDate };
        //    //return RedirectToAction("RevenueDetails",viewModel);
        //    int isDailyReport = 1;

        //    TempData["ID"] = isDailyReport;

        //    return RedirectToAction("RevenueDetails");
            
        //}
    }

}



