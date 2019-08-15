using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectMvc.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string productName { get; set; }
        public string ProductDetails { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ModifedeDate { get; set; }
        public string Description { get; set; }
        public bool isFeatured { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Sale { get; set; }
        public virtual category CategoryObj { get; set; }
       
        public virtual ICollection<Review> ReviewList { get; set; }
        public virtual ICollection<ImagesProduct>  IamgesList { get; set; }
    }
}