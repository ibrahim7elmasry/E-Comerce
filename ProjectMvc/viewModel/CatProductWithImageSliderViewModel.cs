using ProjectMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMvc.viewModel
{
    public class CatProductWithImageSliderViewModel
    {
        public SliderImage SliderImageObg { get; set; }
        public List<category> ListOfCategory { get; set; }
        public List<ProductWithImag> ProductWithImagList { get; set; }
    }
}