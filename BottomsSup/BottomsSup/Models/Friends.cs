using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BottomsSup.Models
{
    public class Friends
    {
        [Key]
        public int FriendId { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client{ get; set; }

        [ForeignKey("Clients")]
        public int FriendsId { get; set; }
        public Client Clients { get; set; }
    }
}