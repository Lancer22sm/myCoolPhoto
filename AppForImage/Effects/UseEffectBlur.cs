using OpenCvSharp;
using System;

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
        int _chanelsImage;

        public UseEffectBlur(Mat source, int chanelsImage)
        {
            sourceImage = source;
            _chanelsImage = chanelsImage;
            GeneralEffect(1, 1, 1, 1);
        }
        public override void ChangeSrcForEffect(Mat src)
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
            if (_chanelsImage <= 3)
            {
                double valuePixels = Convert.ToDouble(bilateralFilter);
                Cv2.BilateralFilter(usebleImageBoxFilterBlur, usebleImageBilateralFilterBlur, bilateralFilter, valuePixels, valuePixels);
                usebleImageReturn = usebleImageBilateralFilterBlur;
            }
            else usebleImageReturn = usebleImageBoxFilterBlur;
            return usebleImageReturn;
        }

    }
}
