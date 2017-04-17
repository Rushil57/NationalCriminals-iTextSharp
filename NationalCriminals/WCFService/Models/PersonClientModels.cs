using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WCFService.Models
{
    public class PersonClientModels
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Nationality { get; set; }

        public int AgeMin { get; set; }
        public int AgeMax { get; set; }
        public int HeightMin { get; set; }
        public int HeightMax { get; set; }
        public int WeightMin { get; set; }
        public int WeightMax { get; set; }

        [Required]
        public int MaxResult { get; set; }

        [Required]
        public string RecipientEmail { get; set; }

    }
}