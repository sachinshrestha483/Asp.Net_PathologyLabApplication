using LabReport2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabReport2.Controllers
{
    [Authorize(Roles = RoleName.CanManageEverything)]

    public class SettingController : Controller
    {
        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }
    }
}