using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProjectMvc.Models
{
    public class ImagesProduct
    {
        public int ID { get; set; }
        [DisplayName("Upload File")]
        public string ImagesUrl { get; set; }
        public virtual Product ProductObject { get; set; }
    }
}