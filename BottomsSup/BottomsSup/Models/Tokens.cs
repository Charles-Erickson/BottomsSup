using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BottomsSup.Models
{
    public class Tokens
    {
        [Key]
        public int TokenId { get; set; }
        public string TokenName { get; set; }
        public double TokenPrice { get; set; }
    }
}