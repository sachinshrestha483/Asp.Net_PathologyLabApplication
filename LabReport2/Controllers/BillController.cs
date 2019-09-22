using LabReport2.Models;
using LabReport2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace LabReport2.Controllers
{
    public class BillController : Controller
    {
        
        private ApplicationDbContext _context;
        public BillController()//constructor
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Bill
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Bill(int id)
        {
            int total=0;
            var bill = _context.Bills.Include(b=>b.Patient).Include(b=>b.Doctor).SingleOrDefault(b => b.Id == id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            var billItems = _context.BillItems.Include(b=>b.Report.TestTitle).Where(b => b.BillId == id);
            
            foreach (var item in billItems)
            {
                
                var price = item.Report.Price;
                total = total + price;

            }

            total = total - bill.Discount;
            var billViewModel = new BillViewModel { Bill = bill, Billitems = billItems.ToList(), Total=total };
            return View(billViewModel);

        }
        
    }
}