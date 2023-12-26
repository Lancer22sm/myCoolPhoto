using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppForImage.Effects
{
    internal class UseEffectColorize : UseEffect
    {
        Mat sourceImage = new();
        Mat usebleImage = new();
        public UseEffectColorize(Mat source)
        {
            sourceImage = source;
            sourceImage.CopyTo(usebleImage);
        }
        public void ChangeSrcForEffect(Mat src)
        {
            sourceImage = src;
            sourceImage.CopyTo(usebleImage);
        }
        public Mat ChangeRed(int valueChange)
        {
            ChangeColor(valueChange, 2);
            return usebleImage;
        }
        public Mat ChangeGreen(int valueChange)
        {
            ChangeColor(valueChange, 1);
            return usebleImage;
        }
        public Mat ChangeBlue(int valueChange)
        {
            ChangeColor(valueChange, 0);
            return usebleImage;
        }
        private void ChangeColor(int valueChange, int color)
        {
            for (int y = 0; y < sourceImage.Height; y++)
            {
                for (int x = 0; x < sourceImage.Width; x++)
                {
                    Vec3b sourceBgr = sourceImage.At<Vec3b>(y, x);
                    Vec3b bgr = usebleImage.At<Vec3b>(y, x);
                    if ((sourceBgr[color] + valueChange) < 0)
                    {
                        bgr[color] = 0;
                    }
                    else if ((sourceBgr[color] + valueChange) > 255)
                    {
                        bgr[color] = 255;
                    }
                    else
                    {
                        bgr[color] = (byte)(sourceBgr[color] + valueChange);
                    }
                    usebleImage.At<Vec3b>(y, x) = bgr;
                }
            }
        }
    }
}
