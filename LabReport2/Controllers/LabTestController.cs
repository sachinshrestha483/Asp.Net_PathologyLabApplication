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
    public class LabTestController : Controller
    {
        private ApplicationDbContext _context;
        public LabTestController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: LabTest
        public ActionResult Index()
        {
            var TestTitles = _context.TestTitles.ToList();
            return View(TestTitles);
        }


        //For  Test Title 
        public ActionResult TestTitleForm()
        {
            var testTitleViewModel = new TestTitleViewModel { TestTitle = new TestTitle(), SubTestTitles = new List<SubtestTitle> { } };
            return View(testTitleViewModel);
        }

        [HttpPost]
        public ActionResult SaveTestTitle(TestTitle testTitle)
        {
            if (!ModelState.IsValid)
            {
                return View("TestTitleForm", testTitle);
            }

            else if (testTitle.Id == 0)
            {
                _context.TestTitles.Add(testTitle);


            }
            else
            {
                var TestTitleinDb = _context.TestTitles.Single(t => t.Id == testTitle.Id);
                TestTitleinDb.Name = testTitle.Name;
                TestTitleinDb.Notes = testTitle.Notes;
                TestTitleinDb.Price = testTitle.Price;



            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditTestTitle(int? id)
        {
            var TestTitleInDb = _context.TestTitles.SingleOrDefault(t => t.Id == id);
            if (TestTitleInDb == null)
            {
                return HttpNotFound();
            }
            var SubTestList = _context.SubtestTitles.Where(t => t.TestTitleId == id);

            var TestTitleViewModel = new TestTitleViewModel { TestTitle=TestTitleInDb, SubTestTitles=SubTestList.ToList() };
            return View("TestTitleForm", TestTitleViewModel);
        }



        //

        //For SubtestTitle
        public ActionResult SubtestForm(int? id)// taking testtitle as input
        {


            var testTitle = _context.TestTitles.SingleOrDefault(t => t.Id == id);

            if (testTitle == null || !(id.HasValue))
            {
                return HttpNotFound("Test Found For Which You Want To Make The Subtest");
            }
            var subTestTitles = _context.SubtestTitles.Where(t=>t.TestTitleId==id);
            var newSubtesTitle = new SubTestViewModel()
            {  
                SubtestTitles = subTestTitles.ToList(),
                TestTitle = testTitle, 
                  
                
                
                
                SubtestTitle = new SubtestTitle() { TestTitleId= (int)id }
            };


            return View(newSubtesTitle);
        }

        public ActionResult SaveSubtestForm(SubtestTitle subtestTitle)
        {
            if (!ModelState.IsValid)
            {
                var newSubtesttitle = new SubTestViewModel()
                {
                    SubtestTitles = _context.SubtestTitles.ToList(),
                    TestTitle = _context.TestTitles.Single(t => t.Id == subtestTitle.TestTitleId),

                    SubtestTitle = new SubtestTitle() { TestTitleId = subtestTitle.TestTitleId }
                };

                return View("SubtestForm", newSubtesttitle);
            }
            else if (subtestTitle.Id == 0)
            {
                _context.SubtestTitles.Add(subtestTitle);
                _context.SaveChanges();

            }

            else
            {
                var subtestinDb = _context.SubtestTitles.Single(s => s.Id == subtestTitle.Id);
                subtestinDb.Name = subtestTitle.Name;
                _context.SaveChanges();

            }
            return RedirectToAction("SubtestForm", new { id=subtestTitle.TestTitleId});
        }

        public ActionResult EditSubtestTitle(int id)// taking subtest as input
        {
            var subtestTitleInDb = _context.SubtestTitles.SingleOrDefault(s => s.Id == id);
            if (subtestTitleInDb==null)
            {
                return HttpNotFound();
            }
            //var subtestTitlesList = _context.SubtestTitles.Where(t => t.Id == subtestTitleInDb.TestTitleId).ToList();
            var subtestTitlesList = _context.SubtestTitles.Where(t=>t.TestTitleId==subtestTitleInDb.TestTitleId).ToList();

            var testTitle = _context.TestTitles.Single(t => t.Id == subtestTitleInDb.TestTitleId);
            var SubtestTitleViewModel = new SubTestViewModel { SubtestTitle = subtestTitleInDb,   SubtestTitles=subtestTitlesList , TestTitle=testTitle };
            return View("SubtestForm", SubtestTitleViewModel);

        }

     
        //

        //Test

        public ActionResult TestForm(int id)// taking subtest id as input 
        {
            var subtest = _context.SubtestTitles.Include(t=>t.TestTitle).SingleOrDefault(s=>s.Id==id);
            if (subtest==null)
            {
                return HttpNotFound("Subtest With Id Not Given");
            }
            var tests = _context.Tests.Where(t => t.SubtestTitleId == id);
            var testViewModel = new TestViewModel { Test=new Test { SubtestTitleId=id },
                SubtestTitle =subtest ,
                   Tests= tests.ToList(),  
            };
            return View(testViewModel);        
        }
        [HttpPost]
        public ActionResult SaveTest(Test test)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound("uhyh");
            }
            else if (test.Id==0)
            {
                _context.Tests.Add(test);
                
            }
            
            _context.SaveChanges();
            return RedirectToAction("TestForm", new { id = test.SubtestTitleId });
        }

        public ActionResult LoadTest(int id)//getting Subtitleid
        {
            var Tests = _context.Tests.Include(t => t.SubtestTitle).Include(t => t.SubtestTitle.TestTitle).Where(t=>t.SubtestTitleId == id);
            return View(Tests.ToList());
        }

        public ActionResult LoadSubtest (int id)//taking testtitle id as input
        {
            var subtests = _context.SubtestTitles.Where(t => t.TestTitleId == id);
            return View(subtests.ToList());
        }

        public ActionResult EditTest(int id)//taking test id as input
        {
            var test = _context.Tests.Include(t=>t.SubtestTitle).Include(t=>t.SubtestTitle.TestTitle).SingleOrDefault(t => t.Id == id);
            if (test==null)
            {
                return HttpNotFound("Test Id Not Valid");
            }
            var testViewModel = new TestViewModel { Test = test };
            return View(testViewModel);
        }

        public ActionResult SaveEditTest(Test test)
        {
            var testInDb = _context.Tests.Single(t => t.Id == test.Id);
            testInDb.Name = test.Name;
            testInDb.Unit = test.Unit;
            testInDb.MaxValue = test.MaxValue;
            testInDb.MinValue = test.MinValue;
            _context.SaveChanges();
            return RedirectToAction("TestForm", new { id = test.SubtestTitleId });

        }

    }

}