using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsyncWebApiPerformance.Models
{
    public class MyTask
    {
        public int Id { get; set; }
        public string Starts { get; set; }
        public string Ends { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}