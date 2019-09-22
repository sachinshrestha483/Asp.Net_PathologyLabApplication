using LabReport2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.ViewModels
{
    public class RevenueDetailsViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Revenue { get; set; }

        public int  NumberOfReports{ get; set; }
        public int NumberOfPatients { get; set; }

        public List<Doctor> UniqueDoctors { get; set; }
        public List<int> UniqueDoctorRefCounts { get; set; }

        public List<TestTitle> UniqueTestTitles { get; set; }
        public List<int> UniqueTestTitleCounts { get; set; }

        //public List<TestTitle> UniqueReports { get; set; }
        //public List<int> UniqueReportCounts { get; set; }
        public List<DateTime> UniqueDates { get; set; }
        public List<int> UniqueReportsOnDate { get; set; }
        public List<DataPoint> DataPoints { get; set; }
        public List<Billitem> Billitems { get; set; }


    }
}