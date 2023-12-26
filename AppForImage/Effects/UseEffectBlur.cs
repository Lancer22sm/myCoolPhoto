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
        Mat usebleImageBilateralFilterBlur = new();
        public Mat usebleImageReturn = new();

        public UseEffectBlur(Mat source)
        {
            sourceImage = source;
            GeneralEffect(1, 1, 1, 1);
        }
        public void ChangeSrcForEffect(Mat src)
        {
            sourceImage = src;
            GeneralEffect(1, 1, 1, 1);
        }

        public Mat GeneralEffect(int valueBlur, int medianBlur, int boxFilter, int bilateralFilter)
        {
            //MessageBox.Show($"{valueBlur} != {blurValue}\n{medianBlur} != {blurMedian}\n{boxFilter} != {filterBox}\n{bilateralFilter} != {filterBilateral}");
            Cv2.Blur(sourceImage, usebleImageBlur, new OpenCvSharp.Size(valueBlur, valueBlur));
            Cv2.MedianBlur(usebleImageBlur, usebleImageMedianBlur, medianBlur);
            Cv2.BoxFilter(usebleImageMedianBlur, usebleImageBoxFilterBlur, usebleImageBoxFilterBlur.Depth(), new OpenCvSharp.Size(boxFilter, boxFilter));
            double valuePixels = Convert.ToDouble(bilateralFilter);
            Cv2.BilateralFilter(usebleImageBoxFilterBlur, usebleImageBilateralFilterBlur, bilateralFilter, valuePixels, valuePixels);
            usebleImageReturn = usebleImageBilateralFilterBlur;
            return usebleImageReturn;
        }

    }
}
