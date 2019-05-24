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

        [Display(Name = "Total Sales of the Day")]
        [DataType(DataType.Currency)]
        public double TotalSales { get; set; }

        [Display(Name = "Cost of Labor For Day")]
        [DataType(DataType.Currency)]
        public double TotalLabor { get; set; }
        public double LaborPercentage { get; set; }

        [ForeignKey("Bar")]
        public int BarId { get; set; }
        public Bar Bar { get; set; }
    }
}