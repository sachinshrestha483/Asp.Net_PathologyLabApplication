using LabReport2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.ViewModels
{
    public class BillViewModel
    {
        public Bill Bill{ get; set; }
        public List<Billitem> Billitems { get; set; }
        public int Total { get; set; }
    }
}