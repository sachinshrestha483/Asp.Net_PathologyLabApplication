using LabReport2.Models;
using LabReport2.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabReport2.Controllers
{
    [Authorize(Roles = RoleName.CanManageEverything)]

    public class ConsultingPathologistController : Controller
    {
        private ApplicationDbContext _context;
        public ConsultingPathologistController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Consulting
        public ActionResult Index()
        {
            var consultingPathologists = _context.ConsultingPathologists.ToList();
            var consultingPathologistId = _context.Defaultvalues.Single(d => d.Id == 1).DefaultalueId;
            var consultingPathologist = _context.ConsultingPathologists.Single(p => p.id == consultingPathologistId);
            var consultingPathologistViewModel = new ConsultingPathologistViewModel { ConsultingPathologist=consultingPathologist, ConsultingPathologists=consultingPathologists  };

            return View(consultingPathologistViewModel);
        }

        public ActionResult ConsultingPathologistForm()
        {
            var consultingPathologistViewModel = new ConsultingPathologistViewModel { ConsultingPathologist = new ConsultingPathologist { id = 0 } };
            return View(consultingPathologistViewModel);
        }
        public ActionResult SaveConsultingPathologist(ConsultingPathologistViewModel consultingPathologistViewModel)
        {
            if (!ModelState.IsValid)
            {
                //return View("ConsultingPathologistForm", consultingPathologistViewModel);
                return HttpNotFound(consultingPathologistViewModel.ConsultingPathologist.id.ToString());
            }
            else if (consultingPathologistViewModel.ConsultingPathologist.id==0)
            {
                consultingPathologistViewModel.ConsultingPathologist.PostName = "Consulting Pathologist";
                var consultingPathologist = consultingPathologistViewModel.ConsultingPathologist;

                try
                {
                    if (consultingPathologistViewModel.DigitalSignaturePhoto.ContentLength > 0)
                    {

                        string imageFileName = Path.GetFileName(DateTime.Now.ToString("ddmmyyyyssfff") + consultingPathologistViewModel.DigitalSignaturePhoto.FileName);
                        string folderPath = Path.Combine(Server.MapPath("~/DigitalSignature"), imageFileName);
                        consultingPathologistViewModel.DigitalSignaturePhoto.SaveAs(folderPath);
                        //consultingPathologistViewModel.Addresks = folderPath;
                        consultingPathologist.DigitalSignaturePhotoaddress = "/DigitalSignature/" + imageFileName;
                    }
                }
                catch (Exception)
                {

                    
                }
                //if (consultingPathologistViewModel.DigitalSignaturePhoto.ContentLength > 0)
                //{

                //    string imageFileName = Path.GetFileName(DateTime.Now.ToString("ddmmyyyyssfff") + consultingPathologistViewModel.DigitalSignaturePhoto.FileName);
                //    string folderPath = Path.Combine(Server.MapPath("~/DigitalSignature"), imageFileName);
                //    consultingPathologistViewModel.DigitalSignaturePhoto.SaveAs(folderPath);
                //    //consultingPathologistViewModel.Addresks = folderPath;
                //    consultingPathologist.DigitalSignaturePhotoaddress = "/DigitalSignature/"+imageFileName;
                //}
                _context.ConsultingPathologists.Add(consultingPathologist);

                
            }
            else
            {
                var pathologistinDb = _context.ConsultingPathologists.Single(p => p.id == consultingPathologistViewModel.ConsultingPathologist.id) ;
                string Photoaddress="";
                try
                {
                    if (consultingPathologistViewModel.DigitalSignaturePhoto.ContentLength > 0)
                    {

                        string imageFileName = Path.GetFileName(DateTime.Now.ToString("ddmmyyyyssfff") + consultingPathologistViewModel.DigitalSignaturePhoto.FileName);
                        string folderPath = Path.Combine(Server.MapPath("~/DigitalSignature"), imageFileName);
                        consultingPathologistViewModel.DigitalSignaturePhoto.SaveAs(folderPath);
                        //consultingPathologistViewModel.Addresks = folderPath;
                        Photoaddress = "/DigitalSignature/" + imageFileName;
                    }
                }
                catch (Exception)
                {


                }

                pathologistinDb.Name = consultingPathologistViewModel.ConsultingPathologist.Name;
                pathologistinDb.Phone = consultingPathologistViewModel.ConsultingPathologist.Phone;
                pathologistinDb.Degree = consultingPathologistViewModel.ConsultingPathologist.Degree;
                pathologistinDb.DigitalSignaturePhotoaddress = consultingPathologistViewModel.ConsultingPathologist.DigitalSignaturePhotoaddress;
                pathologistinDb.PostName = consultingPathologistViewModel.ConsultingPathologist.PostName;
                pathologistinDb.RegNo = consultingPathologistViewModel.ConsultingPathologist.RegNo;
                pathologistinDb.DigitalSignaturePhotoaddress = Photoaddress;
                

            }
            _context.SaveChanges();

            return RedirectToAction("index");

        }
        public ActionResult EditConsultingPathologist(int id)
        {
            var consultingPathologist = _context.ConsultingPathologists.SingleOrDefault(p => p.id == id);
            if (consultingPathologist==null)
            {
                return HttpNotFound();
            }
            var viewModel = new ConsultingPathologistViewModel { ConsultingPathologist = consultingPathologist };
            return View("ConsultingPathologistForm", viewModel);

        }


    }
}