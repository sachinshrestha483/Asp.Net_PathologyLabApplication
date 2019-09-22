using LabReport2.Models;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.ViewModels
{
    public class OldPatientReportViewModel
    {
     

            public List<Doctor> Doctors { get; set; }

            public List<TestTitle> TestTitles { get; set; }

            public List<int?> TestTitleIds { get; set; }
            public Report Report { get; set; }

            //public List<Patient> Patients { get; set; }
            public int PatientId { get; set; }

        
    }
}