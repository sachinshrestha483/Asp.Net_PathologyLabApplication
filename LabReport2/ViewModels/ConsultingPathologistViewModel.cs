using LabReport2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.ViewModels
{
    public class ConsultingPathologistViewModel
    {
        public List<ConsultingPathologist> ConsultingPathologists{ get; set; }
        public ConsultingPathologist ConsultingPathologist { get; set; }
        public HttpPostedFileBase DigitalSignaturePhoto { get; set; }

    }
}