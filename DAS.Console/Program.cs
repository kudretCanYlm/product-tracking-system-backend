using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAS.Model.Model.Product;
using DAS.Model.Model.User;
using OpenCvSharp;

namespace DAS.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var src = new Mat("dog.jpg", ImreadModes.Grayscale);
            var dst = new Mat();

            System.Console.WriteLine("width: " + src.Rows + "\nheight: " + src.Cols);

            Cv2.CvtColor(src, dst, ColorConversionCodes.BayerBG2BGR);

            Cv2.Canny(src, dst, 50, 200);
            using (new Window("src image", src))
            using (new Window("dst image", dst))
            {
                Cv2.WaitKey();
            }

        }
    }
}
