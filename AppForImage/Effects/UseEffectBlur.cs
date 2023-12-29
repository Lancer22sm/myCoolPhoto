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
        int _chanelsImage;

        public UseEffectBlur(int chanelsImage)
        {
            _chanelsImage = chanelsImage;
        }

        public Mat GeneralEffect(Mat useImagePreview, int valueBlur, int medianBlur, int boxFilter, int bilateralFilter)
        {
            if (valueBlur > 1) Cv2.Blur(useImagePreview, usebleImageBlur, new Size(valueBlur, valueBlur));
            else usebleImageBlur = useImagePreview;
            if (medianBlur > 1) Cv2.MedianBlur(usebleImageBlur, usebleImageMedianBlur, medianBlur);
            else usebleImageMedianBlur = usebleImageBlur;
            if (boxFilter > 1) Cv2.BoxFilter(usebleImageMedianBlur, usebleImageBoxFilterBlur, usebleImageBoxFilterBlur.Depth(), new OpenCvSharp.Size(boxFilter, boxFilter));
            else usebleImageBoxFilterBlur = usebleImageMedianBlur;
            if (_chanelsImage <= 3)
            {
                double valuePixels = Convert.ToDouble(bilateralFilter);
                if (bilateralFilter > 1) Cv2.BilateralFilter(usebleImageBoxFilterBlur, usebleImageBilateralFilterBlur, bilateralFilter, valuePixels, valuePixels);
                else usebleImageBilateralFilterBlur = usebleImageBoxFilterBlur;
                usebleImageReturn = usebleImageBilateralFilterBlur;
            }
            else usebleImageReturn = usebleImageBoxFilterBlur;
            return usebleImageReturn;
        }
    }
}
