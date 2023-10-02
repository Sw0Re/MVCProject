using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProjeKampı.Calender
{
    public class Calenderr
    {
        public String title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public bool allDay { get; set; }
    }
}