using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.Models
{
    public class Report
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        public TestTitle TestTitle { get; set; }
        public int TestTitleId { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime Date { get; set; }
        public string ReportBy { get; set; }
        public string  InvestigatedBy { get; set; }
        public int Price { get; set; }
        public bool DigitalSignature { get; set; }
        public ConsultingPathologist ConsultingPathologist { get; set; }
        public int ConsultingPathologistId { get; set; }
        public bool isInvestgationDone { get; set; }
    }
}