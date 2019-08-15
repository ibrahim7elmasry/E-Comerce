using ProjectMvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectMvc.viewModel
{
    public class ProductWithImag
    {
       

        public int ProductId { get; set; }
        public string productName { get; set; }
        public string ProductDetails { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Sale { get; set; }
        public virtual category CategoryObj { get; set; }
        public string ImagesUrl { get; set; }
        


    }
}