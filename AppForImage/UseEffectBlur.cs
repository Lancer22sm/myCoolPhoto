using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppForImage
{
    public class UseEffectBlur : UseEffect
    {
        Mat sourceImage = new();
        Mat UsebleImage = new();

        public UseEffectBlur(Mat source)
        {
            sourceImage = source;
        }
        public void ChangeSrcForEffect(Mat src)
        {
            sourceImage = src;
        }

        public Mat GeneralEffect(int valueBlur)
        {
            Cv2.Blur(sourceImage, UsebleImage, new OpenCvSharp.Size(valueBlur, valueBlur));
            Mat copiesMat = new();
            UsebleImage.CopyTo(copiesMat);
            return copiesMat;
        }
        public Mat MedianBlur(int valueBlur)
        {
            Cv2.MedianBlur(sourceImage, UsebleImage, valueBlur);
            Mat copiesMat = new();
            UsebleImage.CopyTo(copiesMat);
            return copiesMat;
        }
    }
}
