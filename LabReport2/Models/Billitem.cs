using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.Models
{
    public class Billitem
    {
        public int id { get; set; }
        public Bill Bill { get; set; }
        public int BillId { get; set; }
        public Report Report { get; set; }
        public int ReportId { get; set; }
    }
}