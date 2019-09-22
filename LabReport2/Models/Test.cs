using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabReport2.Models
{
    public class Test
    {
        public int Id { get; set; }
        public SubtestTitle SubtestTitle { get; set; }

        public int SubtestTitleId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public int? MaxValue { get; set; }
        public int? MinValue { get; set; }
        public string Unit { get; set; }
    }
}