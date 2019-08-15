using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProjectMvc.ViewMdel
{
    public class ImageSliderViewModel
    {
        public int sliderId { get; set; }
        public string sliderTile { get; set; }
        [DisplayName("Upload File")]
        public string sliderimage { get; set; }

        public HttpPostedFileBase imageFile { get; set; }
    }
}