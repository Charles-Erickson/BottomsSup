using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BottomsSup.Models
{
    public class Bank
    {
        [Key]
        public int BankId { get; set; }
        public double Money { get; set; }

    }
}