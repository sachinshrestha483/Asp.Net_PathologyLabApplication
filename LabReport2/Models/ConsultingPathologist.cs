using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabReport2.Models
{
    public class ConsultingPathologist
    {
        public int id { get; set; }
        public string Name { get; set; }
        public String Degree { get; set; }
        public string PostName { get; set; }
        public int? RegNo { get; set; }
        [StringLength(12)]
        public string Phone { get; set; }

        public string DigitalSignaturePhotoaddress { get; set; }
       
    }
}