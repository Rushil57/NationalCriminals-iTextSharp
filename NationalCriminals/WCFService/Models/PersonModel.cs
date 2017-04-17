using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFService.Models
{
    public class PersonModel
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Nationality { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
    }
}