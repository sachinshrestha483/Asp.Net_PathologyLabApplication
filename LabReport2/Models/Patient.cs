using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabReport2.Models
{
    public class Patient
    {
        public int Id { get; set; }
        
        [Required]
        public string PatientName { get; set; }
        [StringLength(12)]
        public string Phone { get; set; }
        [Required]
        public int? Age { get; set; }
        [StringLength(6)]
        public String Gender { get; set; }
        
        public string Address { get; set; }
        public DateTime? AddedOn { get; set; }
        [StringLength(50)]

        public string AdddedBy { get; set; }
    }
}