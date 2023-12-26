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
        Mat usebleImageBlur = new();
        Mat usebleImageMedianBlur = new();
        Mat usebleImageBoxFilterBlur = new();
        public Mat usebleImageBilateralFilterBlur = new();

        public UseEffectBlur(Mat source)
        {
            sourceImage = source;
        }
        public void ChangeSrcForEffect(Mat src)
        {
            sourceImage = src;
            GeneralEffect(1, 1, 1, 1);
        }

        public Mat GeneralEffect(int valueBlur, int medianBlur, int boxFilter, int bilateralFilter)
        {
            Cv2.Blur(sourceImage, usebleImageBlur, new OpenCvSharp.Size(valueBlur, valueBlur));
            Cv2.MedianBlur(usebleImageBlur, usebleImageMedianBlur, medianBlur);
            Cv2.BoxFilter(usebleImageMedianBlur, usebleImageBoxFilterBlur, usebleImageBoxFilterBlur.Depth(), new OpenCvSharp.Size(boxFilter, boxFilter));
            double valuePixels = Convert.ToDouble(bilateralFilter);
            Cv2.BilateralFilter(usebleImageBoxFilterBlur, usebleImageBilateralFilterBlur, bilateralFilter, valuePixels, valuePixels);
            return usebleImageBilateralFilterBlur;
        }

    }
}
