using LabReport2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.ViewModels
{
    public class NewPatientReportViewModel
    {
        public Patient Patient { get; set; }
      
        public List<Doctor>  Doctors { get; set; }
       
        public List<TestTitle> TestTitles{ get; set; }

        public List<int?> TestTitleIds { get; set; }
        public Report Report { get; set; }


    }
}