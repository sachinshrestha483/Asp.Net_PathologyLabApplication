using LabReport2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.ViewModels
{
    public class ShowReportViewModel
    {
        public List<SubtestTitle> SubtestTitles { get; set; }
        public List<Test> Tests { get; set; }
        public Report Report { get; set; }
        public List<Result> Results { get; set; }
    }
}