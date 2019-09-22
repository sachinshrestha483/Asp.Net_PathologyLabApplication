using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabReport2.Models
{
    public class TestTitle
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Test Title")]
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(50)]
        public string AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public string Notes { get; set; }

        public int Price { get; set; }

    }
}