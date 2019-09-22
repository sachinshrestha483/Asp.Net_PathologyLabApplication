using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabReport2.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Doctors Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Commision")]
        public int Commission { get; set; }
        [DisplayName("Phone Number")]
        [StringLength(12)]
        public string Phone { get; set; }
        [StringLength(50)]

        public string AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }

    }
}