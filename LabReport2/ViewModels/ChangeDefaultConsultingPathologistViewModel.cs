using LabReport2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.ViewModels
{
    public class ChangeDefaultConsultingPathologistViewModel
    {
        public Doctor Doctor { get; set; }
        public List<ConsultingPathologist> ConsultingPathologists { get; set; }
    }
}

