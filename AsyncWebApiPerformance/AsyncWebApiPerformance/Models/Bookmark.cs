using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsyncWebApiPerformance.Models
{
    public class Bookmark
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
    }
}