using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForImage.Effects
{
    internal class UseEffectColorize : UseEffect
    {

        Mat sourceImage = new();
        Mat UsebleImage = new();

        public UseEffectColorize(Mat source)
        {
            sourceImage = source;
        }

        public void ChangeSrcForEffect(Mat src)
        {
            sourceImage = src;
        }
        public Mat GeneralEffect(byte redValue, byte greenValue, byte blueValue)
        {
            Mat dst = new();
            sourceImage.CopyTo(dst);
            for (int y = 0; y < dst.Height; y++)
            {
                for (int x = 0; x < dst.Width; x++)
                {
                    Vec3b bgr = dst.At<Vec3b>(y, x);
                    if (blueValue != 0) bgr[0] = blueValue; ;
                    if (greenValue != 0) bgr[1] = greenValue;
                    if (redValue != 0) bgr[2] = redValue;
                    dst.At<Vec3b>(y, x) = bgr;
                }
            }
            return dst;
        }
    }
}
