using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
        public string BillBy { get; set; }
        public int Discount { get; set; }


    }
}