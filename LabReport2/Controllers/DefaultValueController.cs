using LabReport2.Models;
using LabReport2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabReport2.Controllers
{
    [Authorize(Roles = RoleName.CanManageEverything)]

    public class DefaultValueController : Controller
    {
            private ApplicationDbContext _context;
            public DefaultValueController()
            {
                _context = new ApplicationDbContext();
            }
            protected override void Dispose(bool disposing)
            {
                _context.Dispose();
            }


        public ActionResult ChangeDefaultConsultingPathologistForm()
        {
            var doctors = _context.ConsultingPathologists.ToList();
            var changeDefaultConsultingPathologistViewModel = new ChangeDefaultConsultingPathologistViewModel {  ConsultingPathologists = doctors };
            return View(changeDefaultConsultingPathologistViewModel);
        }
        [HttpPost]
            [ValidateAntiForgeryToken]
        public ActionResult ChangeDefaultConsultingPathologist(Doctor doctor)
        {
            var defaultConsultingPathologist = _context.Defaultvalues.Single(d => d.Id == 1);
            defaultConsultingPathologist.DefaultalueId = doctor.Id;
            _context.SaveChanges();
            return RedirectToAction("index","ConsultingPathologist"); 
        }
    }
}