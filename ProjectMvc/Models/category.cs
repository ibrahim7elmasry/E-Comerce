using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectMvc.Models
{
    public class category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [DisplayName("Upload File")]
        public string CategoryImage { get; set; }
        public virtual ICollection<Product> ProductLis { get; set; }
    }
}