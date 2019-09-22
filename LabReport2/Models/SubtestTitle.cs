using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabReport2.Models
{
    public class SubtestTitle
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public TestTitle TestTitle { get; set; }
        public int TestTitleId { get; set; }
    }
}