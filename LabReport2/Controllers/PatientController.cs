using LabReport2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LabReport2.ViewModels;

namespace LabReport2.Controllers
{
    public class PatientController : Controller
    {
        private ApplicationDbContext _context;
        public PatientController()//constructor
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Patient
        public ActionResult Index()
        {
            var patients = _context.Patients.ToList();
            return View(patients);
        }

        public ActionResult PatientForm()
        {
            var patient = new Patient();
            return View(patient);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return View("PatientForm", patient);
            }
            else if (patient.Id == 0)
            {
                //Setting Valur For Addedon
                patient.AddedOn = DateTime.Now;
                _context.Patients.Add(patient);
                _context.SaveChanges();
                return RedirectToAction("PatientForm", "Patient");
            }
            var patientinDb = _context.Patients.Single(p => p.Id == patient.Id);
            patientinDb.PatientName = patient.PatientName;
            patientinDb.Phone = patient.Phone;
            patientinDb.Gender = patient.Gender;
            patientinDb.Address = patient.Address;
            patientinDb.Age = patient.Age;
            _context.SaveChanges();

            return RedirectToAction("Index", "Patient");

        }

        public ActionResult Edit(int? id)
        {
            var patient = _context.Patients.SingleOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View("PatientForm", patient);

        }

        public ActionResult SeeReports(int id)
        {
            var patient = _context.Patients.SingleOrDefault(p => p.Id == id);
            var reports = _context.Reports.
                Include(r=>r.TestTitle)
                .Include(r=>r.ConsultingPathologist)
                .Include(r=>r.Doctor)
                .Where(r => r.PatientId == patient.Id)
                .ToList();
            
            var uniqueTestTitles = new HashSet<string>();
            var reportCount = reports.Count();
            foreach (var report in reports)
            {
                uniqueTestTitles.Add(report.TestTitle.Name);
            }
            var viewModel = new SeeReportsViewModel { Patient = patient, Reports = reports, UniqueTestTitles=uniqueTestTitles, TotalReportCount=reportCount };

            return View(viewModel);
        }
    }
}