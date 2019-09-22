using LabReport2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReport2.ViewModels
{
    public class TestViewModel
    {
        public Test  Test { get; set; }
        public List<Test> Tests { get; set; }
        public SubtestTitle SubtestTitle { get; set; }
    }
}