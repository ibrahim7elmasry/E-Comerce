using ProjectMvc.Models;
using ProjectMvc.ViewMdel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMvc.viewModel
{
    public class UpdateProductImage
    {
        public List<ImagesProduct> ImagesProductList { get; set; }
        public FileModel FileModelObj { get; set; }
    }
}