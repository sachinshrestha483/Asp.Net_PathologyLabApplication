using LabReport2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using System.Web;
using System.Web.Mvc;
using LabReport2.ViewModels;

namespace LabReport2.Controllers
{
    public class PatientReportController : Controller
    {
        private ApplicationDbContext _context;
        public PatientReportController()//constructor
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: PatientReport
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CompleteReport()
        {
            var billItems = _context.BillItems
                 .Include(t => t.Report)
                 .Include(t => t.Report.Doctor)
                 .Include(t => t.Report.Patient)
                 .Include(t => t.Report.TestTitle)
                 .Include(t => t.Report.ConsultingPathologist)
                 .Where(t => t.Report.isInvestgationDone == true)
                 .ToList().OrderByDescending(t => t.BillId).ToList();
            var completeReportsCount = billItems.Count();
            var completeReportViewModel = new CompleteReportViewModel
            {
                PendingreportCount = completeReportsCount,
                Billitems = billItems
            };
            return View(completeReportViewModel);

        }
        public ActionResult PendingReport()
        {
            //var pendingReports = _context.Reports.OrderBy(t=>t.Id)
            //    .Include(t=>t.Doctor)
            //    .Include(t=>t.Patient)
            //    .Include(t=>t.TestTitle)
            //    .Include(t=>t.ConsultingPathologist)
            //    .Where(t => t.isInvestgationDone == false)
            //    .ToList();
            var billItems = _context.BillItems
                .Include(t=>t.Report)
                .Include(t=>t.Report.Doctor)
                .Include(t => t.Report.Patient)
                .Include(t => t.Report.TestTitle)
                .Include(t => t.Report.ConsultingPathologist)
                .Where(t=>t.Report.isInvestgationDone==false)
                .OrderByDescending(t=>t.BillId).ToList();
            var pendingReportsCount = billItems.Count();

            var pendingReportViewModel = new PendingReportViewModel {
                PendingreportCount = pendingReportsCount,
                 Billitems=billItems
            };
            return View(pendingReportViewModel);

        }

        public ActionResult AddResult(int Id)
        {
            var report = _context.Reports
                .Include(t=>t.TestTitle)
                .Include(t=>t.Patient)
                .Include(t=>t.Doctor)
                .SingleOrDefault(t => t.Id == Id);

            if (report==null)
            {
                return HttpNotFound("Test Title Id");
            }

            if (report.isInvestgationDone==true)
            {
                return View("PendingReport");
            }

            var testTitle = report.TestTitle;
            var subtestTitles = _context.SubtestTitles.Where(t => t.TestTitleId == testTitle.Id).ToList();
            var tests = new List<Test>();
            foreach (var subtestTitle in subtestTitles)
            {
                var newTest = _context.Tests.Where(t => t.SubtestTitleId == subtestTitle.Id).ToList();
                tests.AddRange(newTest);
            }
            var testTitles = new List<int>();
            var patient = report.Patient;

            var addResultViewModel = new AddResultViewModel
            {
                SubtestTitles = subtestTitles,
                TestTitle = testTitle,
                 Tests=tests,
                Patient=patient, 
                 Report=report
               
            };
            
            return View(addResultViewModel);

        }


        public ActionResult ShowData(AddResultViewModel addResultViewModel)
        {
            //for checking report with this id exist or not
            var report = _context.Reports
                .Include(t=>t.Patient)
                .Include(t=>t.TestTitle)
                .Include(t=>t.ConsultingPathologist)
                .Include(t=>t.Doctor)
                .Single(r => r.Id == addResultViewModel.Report.Id);
            var resultValues = addResultViewModel.Results;
            var testIds = addResultViewModel.TestIds;
            var reportId = addResultViewModel.Report.Id;
            var i = 0;
            var results = new List<Result>();
            foreach (var item in resultValues)
            {
                
                if (item == null||item=="")
                {
                    i++;
                    continue;
                    
                }

                var newResult = new Result();
                newResult.testId = testIds[i];
                newResult.value = resultValues[i];
                newResult.ReportId = addResultViewModel.Report.Id;
                _context.Result.Add(newResult);
                var reportInDb = _context.Reports.Single(r => r.Id == reportId);
                reportInDb.isInvestgationDone = true;
                _context.SaveChanges();
                results.Add(newResult);
               
               

                i++;
            }


            //for showing report

            //var subtests = _context.SubtestTitles.Where(s => s.TestTitleId == report.TestTitleId).ToList();
            //var tests = new List<Test>();
            //foreach (var testId in testIds)
            //{
            //    var test = _context.Tests.Single(t => t.Id == testId);
            //    tests.Add(test);

            //}
            //var showReportViewModel = new ShowReportViewModel { Report = report,
            //    Tests =tests ,
            //    SubtestTitles =subtests,
            //   Results= results};
            return null;
        }


        public ActionResult ViewReport(int id )
        {

            var report = _context.Reports
                .Include(t=>t.Patient)
                .Include(t=>t.ConsultingPathologist)
                .Include(t=>t.TestTitle)
                .Include(t=>t.Doctor)
                .SingleOrDefault(t => t.Id == id);
            if (report==null)
            {
                return HttpNotFound();
            }
            var results = _context.Result.Where(t => t.ReportId == report.Id).ToList();
            var subtests = _context.SubtestTitles.Where(t => t.TestTitleId == report.TestTitleId).ToList();
            var tests = new List<Test>();
            foreach (var subtest in subtests)
            {
                var newtest = _context.Tests.Where(t => t.SubtestTitleId == subtest.Id);
                tests.AddRange(newtest);
            }


            var showReportViewModel = new ShowReportViewModel
            {
                Report = report,
                Tests = tests,
                SubtestTitles = subtests,
                Results = results
            };
            return View(showReportViewModel);
        }
    }
}