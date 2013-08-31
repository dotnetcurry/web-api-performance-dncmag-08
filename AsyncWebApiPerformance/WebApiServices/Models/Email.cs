using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServices.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Title { get; set; }
    }
}