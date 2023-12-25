using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppForImage.Effects
{
    public class UseEffectBlur : UseEffect
    {
        Mat sourceImage = new();
        Mat UsebleImage = new();

        public UseEffectBlur(Mat source)
        {
            source.CopyTo(sourceImage);
        }
        public void ChangeSrcForEffect(Mat src)
        {
            src.CopyTo(sourceImage);
        }

        public Mat GeneralEffect(int valueBlur)
        {
            Cv2.Blur(sourceImage, UsebleImage, new OpenCvSharp.Size(valueBlur, valueBlur));
            Mat copiesMat = new();
            UsebleImage.CopyTo(copiesMat);
            return copiesMat;
        }

        public Mat BoxFilter(int valueBlur)
        {
            Cv2.BoxFilter(sourceImage, UsebleImage, UsebleImage.Depth(), new OpenCvSharp.Size(valueBlur, valueBlur));
            Mat copiesMat = new();
            UsebleImage.CopyTo(copiesMat);
            return copiesMat;
        }

        public Mat BilateralFilter(int valueBlur)
        {
            double valuePixels = Convert.ToDouble(valueBlur);
            Cv2.BilateralFilter(sourceImage, UsebleImage, valueBlur, valuePixels, valuePixels);
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
