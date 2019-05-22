using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BottomsSup.Models
{
    public class Bar
    {
        [Key]
        public int BarId { get; set; }
        //[Display("Bar Name")]
        public string BarName { get; set; }
        //public IEnumerable<Tokens> Tokens { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string PhoneNumber { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}