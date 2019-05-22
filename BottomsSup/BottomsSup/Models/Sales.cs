using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BottomsSup.Models
{
    public class Sales
    {
        [Key]
        public int SalesId { get; set; }

        [Display(Name = "Date of Sales")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfSales { get; set; }

        public string TotalSales { get; set; }
        public string TotalLabor { get; set; }

        [ForeignKey("Bar")]
        public int BarId { get; set; }
        public Bar Bar { get; set; }
    }
}