using LabReport2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.ViewModels
{
    public class CompleteReportViewModel
    {
        public List<Report> PendingReports { get; set; }
        public int PendingreportCount { get; set; }

        public List<Billitem> Billitems { get; set; }

    }
}

