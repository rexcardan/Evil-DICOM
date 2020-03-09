using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Image
{
    public class ImageMatrix : Matrix
    {
        public string SeriesInstanceUID { get; set; }
        public DateTime? ImageDate { get; set; }
        public string SeriesId { get; set; }
        public string ImageId { get; set; }
        public string ImageOrientationDescription { get; internal set; }

        public ImageMatrix()
        {
        }
    }
}
