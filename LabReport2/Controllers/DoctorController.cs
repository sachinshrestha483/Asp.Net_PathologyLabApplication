using LabReport2.Models;
using LabReport2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabReport2.Controllers
{
    public class DoctorController : Controller
    {
        private ApplicationDbContext _context;
        public DoctorController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Doctor
        public ActionResult Index()
        {
            var doctors = _context.Doctors.ToList();
            var consultingPathologistId = _context.Defaultvalues.Single(d => d.Id == 1).DefaultalueId;
            var consultingPathologist = _context.ConsultingPathologists.Single(d => d.id == consultingPathologistId);
            var doctorViewModel = new DoctorViewModel { Doctors = doctors, ConsultingPathologist= consultingPathologist };
            return View(doctorViewModel);

        }

        public ActionResult DoctorForm()
        {
            var doctor = new Doctor();
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View("DoctorForm", doctor);
            }

            else if (doctor.Id == 0)
            {
                _context.Doctors.Add(doctor);
                doctor.AddedOn = DateTime.Now;
                _context.SaveChanges();
                return RedirectToAction("DoctorForm", "Doctor");
            }
            else
            {
                var doctorindb = _context.Doctors.Single(d => d.Id == doctor.Id);

                doctorindb.Name = doctor.Name;
                doctorindb.Commission = doctor.Commission;
                doctorindb.Phone = doctor.Phone;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Doctor");




        }

        public ActionResult Edit(int id)
        {
            var doctor = _context.Doctors.SingleOrDefault(d => d.Id == id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View("DoctorForm", doctor);
        }

        public ActionResult Delete(int id)
        {
            var doctorindb = _context.Doctors.SingleOrDefault(d => d.Id == id);
            _context.Doctors.Remove(doctorindb);
            _context.SaveChanges();
            return RedirectToAction("Index", "Doctor");
        }



        

    }
}