using ProjectMvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProjectMvc.ViewMdel
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [DisplayName("Upload File")]
        public string CategoryImage { get; set; }
        public virtual ICollection<Product> ProductLis { get; set; }
        public HttpPostedFileBase imageFile { get; set; }

    }
}