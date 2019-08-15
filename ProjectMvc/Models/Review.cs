using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMvc.Models
{
    public class Review
    {
        public int ID { get; set; }
        public string Comments { get; set; }
        public virtual Product ProductOpject { get; set; }
        public virtual ApplicationUser ApplicationUserObject { get; set; }
    }
}