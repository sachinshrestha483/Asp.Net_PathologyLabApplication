using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.ViewModels
{
    public class AdminViewModel
    {
         
        public int NumberOfReportThisMonth { get; set; }
        public int NumberofRerportToday { get; set; }
        public int NumberOfReportPreviousMonth { get; set; }
        public int CurrentMonthRevenue { get; set; }
        public int LastMonthRevenue { get; set; }
        
    }
}