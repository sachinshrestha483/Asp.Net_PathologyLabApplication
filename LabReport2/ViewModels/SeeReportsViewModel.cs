using LabReport2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.ViewModels
{
    public class SeeReportsViewModel
    {
        public Patient Patient { get; set; }
        public List<Report> Reports { get; set; }
        public HashSet<string> UniqueTestTitles { get; set; }
        public int TotalReportCount { get; set; }
    }
}