using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int ReportId { get; set; }
        public int testId { get; set; }
        public string value { get; set; }
    }
}