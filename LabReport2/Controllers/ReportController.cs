using LabReport2.ViewModels;
using LabReport2.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabReport2.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        private ApplicationDbContext _context;
        public ReportController()//constructor
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult NewPatientReportForm()
        {
            var testTitles = _context.TestTitles.ToList();
            var doctors = _context.Doctors.ToList();
            var newPatientReportViewModel = new NewPatientReportViewModel { Patient = new Patient { } , Report=new Report { Date=DateTime.Now  }, TestTitles=testTitles,Doctors=doctors};
            return View(newPatientReportViewModel);
        }

        public ActionResult OldPatientForm()
        {
            var doctors = _context.Doctors.ToList();

            var testTitles = _context.TestTitles.ToList();

            var patients = _context.Patients.ToList();
            var oldPatientReportViewModel = new OldPatientReportViewModel { Doctors = doctors, Report = new Report { Date = DateTime.Now } , TestTitles=testTitles};
            return View(oldPatientReportViewModel);
        }
        [HttpPost]
        public ActionResult SaveOldPatientReport(OldPatientReportViewModel oldPatientReportViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("OldPatientForm", oldPatientReportViewModel);
            }
            var patient = _context.Patients.SingleOrDefault(r => r.Id == oldPatientReportViewModel.PatientId);
            if (patient==null)
            {
                return HttpNotFound("Patient Not Found");
            }

            if (oldPatientReportViewModel.TestTitleIds[0] == null)
            {
                return HttpNotFound("Form Not Submitted Correctly");
            }
            var reports = new List<Report>();

            foreach (var testTitleId in oldPatientReportViewModel.TestTitleIds)
            {
                if (!testTitleId.HasValue)
                {
                    continue;
                }
                var testTitle = _context.TestTitles.Single(t => t.Id == testTitleId);

                var report = new Report
                {
                    //PatientId = patient.Id,
                    TestTitleId = testTitle.Id,
                    //InvestigatedBy = "",
                    //ReportBy = "",
                    //ConsultingPathologistId = _context.Defaultvalues.Single(d => d.Id == 1).Id,
                     //Date=oldPatientReportViewModel.Report.Date,
                      //DateAdded=DateTime.Now,
                       Price=testTitle.Price,
                        //DoctorId=oldPatientReportViewModel.Report.DoctorId,
                    //DigitalSignature=oldPatientReportViewModel.Report.DigitalSignature
                       


                };
                _context.Reports.Add(report);
                reports.Add(report);

            }
            var oldPatientReportBill = new OldPatientReportBillViewModel {
                Reports =reports,
                PatientId =patient.Id,
                Patient =patient,
                Report =oldPatientReportViewModel.Report
                , Bill=new Bill { Discount=0}
                  

            };
            return View("OldPatientBillForm",oldPatientReportBill);
            
        }
        

       public ActionResult SaveNewPatientReport(NewPatientReportViewModel newPatientReportViewModel)
       {
            if (!ModelState.IsValid)
            {
                var patientReportViewModel = new NewPatientReportViewModel { Patient = newPatientReportViewModel.Patient };
                return View("NewPatientReportForm", patientReportViewModel);
            }
            var patient = newPatientReportViewModel.Patient;
            _context.Patients.Add(patient);
            var reports = new List<Report>();
            if (newPatientReportViewModel.TestTitleIds[0]==null)
            {
                return HttpNotFound("Form Not Submitted Correctly");
            }

            foreach (var testTitleId in newPatientReportViewModel.TestTitleIds)
            {
                if (!testTitleId.HasValue)
                {
                    continue;
                }
                var testTitle = _context.TestTitles.Single(t => t.Id == testTitleId);

                var Report = new Report
                {
                    PatientId = patient.Id,
                    TestTitleId = testTitle.Id,
                    Price = testTitle.Price,
                    


                };
                _context.Reports.Add(Report);
                reports.Add(Report);
            }

            var billFormViewModel = new BillFormViewModel
            {
                Reports=reports,
                Patient =patient,
                Report=newPatientReportViewModel.Report,
                 Bill=new Bill { Discount=0},

            };





            return View("BillForm", billFormViewModel);
        }

        public ActionResult BillForm(BillFormViewModel billFormViewModel)
        {
            var bill = billFormViewModel;
            return View(bill);
        }

        public ActionResult OldPatientBillForm(OldPatientReportBillViewModel oldPatientReportBillViewModel)
        {
            return View(oldPatientReportBillViewModel);
        }
        [HttpPost]
        public ActionResult SaveBill(BillFormViewModel billFormViewModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    return HttpNotFound();
            //}
            var patient = billFormViewModel.Patient;
            _context.Patients.Add(patient);
            _context.SaveChanges();
            var patientId = patient.Id;
            var reports = new List<Report>();


          




            billFormViewModel.Bill.Date = DateTime.Now;
            billFormViewModel.Bill.DoctorId = billFormViewModel.Report.DoctorId;
            billFormViewModel.Bill.PatientId = patientId;
            billFormViewModel.Bill.BillBy = "";
            
            var bill = billFormViewModel.Bill;
            _context.Bills.Add(bill);
            _context.SaveChanges();

            foreach (var report in billFormViewModel.Reports)
            {
                //if (!test.HasValue)
                //{
                //    continue;
                //}
                 var Newreport = new Report
                {
                    PatientId = patient.Id,
                    Date = billFormViewModel.Report.Date,
                    TestTitleId = report.TestTitleId,
                    Price = report.Price,
                    DateAdded = DateTime.Now,
                    DoctorId = billFormViewModel.Report.DoctorId,
                    ReportBy = "",
                    InvestigatedBy = "",
                    DigitalSignature =billFormViewModel.Report.DigitalSignature,
                    ConsultingPathologistId = _context.Defaultvalues.Single(d => d.Id == 1).Id


                };
                _context.Reports.Add(Newreport);
                reports.Add(Newreport);
                _context.SaveChanges();



            }

            foreach (var report in reports)
            {
                var billitem = new Billitem { BillId = bill.Id, ReportId = report.Id };
                _context.BillItems.Add(billitem);
                _context.SaveChanges();
            }
            return RedirectToAction("index", "Report");



        }

      
        [HttpPost]
        public ActionResult SaveOldPatientBill(OldPatientReportBillViewModel oldPatientReportBillViewModel)
        {

            //Patient Age Change
            var patient = _context.Patients.Single(p => p.Id == oldPatientReportBillViewModel.PatientId);
            if (!(patient.Age==oldPatientReportBillViewModel.Patient.Age))
            {
                patient.Age = oldPatientReportBillViewModel.Patient.Age;
                _context.SaveChanges();
            }
            //
            var reports = new List<Report>();
            
            foreach (var report in oldPatientReportBillViewModel.Reports)
            {
                var newReport = new Report
                {
                    ConsultingPathologistId = _context.Defaultvalues.Single(d => d.Id == 1).Id,
                     Date=oldPatientReportBillViewModel.Report.Date,
                      DateAdded=DateTime.Now,
                       DigitalSignature=oldPatientReportBillViewModel.Report.DigitalSignature,
                        DoctorId=oldPatientReportBillViewModel.Report.DoctorId,
                         InvestigatedBy=oldPatientReportBillViewModel.Report.InvestigatedBy,
                          PatientId=oldPatientReportBillViewModel.PatientId,
                           ReportBy=oldPatientReportBillViewModel.Report.ReportBy,
                            TestTitleId=report.TestTitleId,
                    Price =report.Price
                             



                };
                _context.Reports.Add(newReport);
                _context.SaveChanges();
                reports.Add(newReport);

            }

            var bill = new Bill
            {
                PatientId = oldPatientReportBillViewModel.PatientId,
                 DoctorId=oldPatientReportBillViewModel.Report.DoctorId,
                  BillBy="",
                   Date=oldPatientReportBillViewModel.Report.Date,
                    Discount=oldPatientReportBillViewModel.Bill.Discount

            };
            _context.Bills.Add(bill);
            _context.SaveChanges();



            foreach (var report in reports)
            {
                var newBillItem = new Billitem
                {
                    BillId = bill.Id,
                    ReportId = report.Id
                };
                _context.BillItems.Add(newBillItem);
                _context.SaveChanges();
            }
            return null;
        }

        public ActionResult AllReports()
        {
            return null;
        }
        public ActionResult AllResults()
        {
            return null;
        }
       
    }
}