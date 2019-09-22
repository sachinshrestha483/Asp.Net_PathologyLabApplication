using LabReport2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.ViewModels
{
    public class AddResultViewModel
    {
        public TestTitle TestTitle { get; set; }
        public List<SubtestTitle> SubtestTitles { get; set; }
        public List<Test> Tests { get; set; }
        public IList<int>TestIds  { get; set; }
        public IList<string> Results { get; set; }
        public  Report Report { get; set; }
        public Patient Patient  { get; set; }
    }
}