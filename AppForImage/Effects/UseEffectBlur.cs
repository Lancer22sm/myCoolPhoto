using OpenCvSharp;
using System;

namespace AppForImage.Effects
{
    public class UseEffectBlur : UseEffect
    {
        Mat usebleImageBlur = new();
        Mat usebleImageMedianBlur = new();
        Mat usebleImageBoxFilterBlur = new();
        Mat usebleImageBilateralFilterBlur = new();
        public Mat usebleImageReturn = new();
        readonly int _chanelsImage;

        public UseEffectBlur(int chanelsImage)
        {
            _chanelsImage = chanelsImage;
        }

        public Mat GeneralEffect(Mat useImagePreview, int valueBlur, int medianBlur, int boxFilter, int bilateralFilter)
        {
            Cv2.Blur(useImagePreview, usebleImageBlur, new Size(valueBlur, valueBlur));
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
