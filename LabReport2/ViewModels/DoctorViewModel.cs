using LabReport2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.ViewModels
{
    public class DoctorViewModel
    {
        public  ConsultingPathologist ConsultingPathologist { get; set; }

        public List<Doctor> Doctors { get; set; }
    }
}