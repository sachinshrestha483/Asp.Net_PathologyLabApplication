using LabReport2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.ViewModels
{
    public class SubTestViewModel
    {
        public TestTitle TestTitle { get; set; }
        public List<SubtestTitle> SubtestTitles { get; set; }
        public SubtestTitle SubtestTitle { get; set; }
        
    }
}