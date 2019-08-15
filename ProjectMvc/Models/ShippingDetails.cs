using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectMvc.Models
{
    public class ShippingDetails
    {
        [Key]
        public int shippingId { get; set; }
        public int memberId { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public int orderid { get; set; }
        public int amoundpaid { get; set; }
        public bool paymenttype { get; set; }
        public ApplicationUser ApplicationUserObj { get; set; }
    }
}