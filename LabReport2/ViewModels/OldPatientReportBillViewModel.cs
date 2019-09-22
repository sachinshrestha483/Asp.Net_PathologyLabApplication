using LabReport2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.ViewModels
{
    public class OldPatientReportBillViewModel
    {

        //public int DoctorId { get; set; }
        //public string DoctorName { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public List<Report> Reports { get; set; }
        public Bill Bill { get; set; }
        //public DateTime Date { get; set; }
        public List<int?> TestTitleIds { get; set; }
        //public DateTime ReportDate { get; set; }
        //public int Price { get; set; }
        //public bool DigitalSignature { get; set; }
        //public string ReportBy { get; set; }
        //public string InvestigatedBy { get; set; }

        public Report Report { get; set; }




    }
}