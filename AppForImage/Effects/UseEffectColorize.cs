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
        public UseEffectColorize(Mat source)
        {
            source.CopyTo(sourceImage);
        }
        public void ChangeSrcForEffect(Mat src)
        {
            src.CopyTo(sourceImage);
        }
        public Mat ChangeRed(int valueChange)
        {
            Mat UsebleImage = new Mat();
            sourceImage.CopyTo(UsebleImage);
            for (int y = 0; y < UsebleImage.Height; y++)
            {
                for (int x = 0; x < UsebleImage.Width; x++)
                {
                    Vec3b sourceBgr = sourceImage.At<Vec3b>(y, x);
                    Vec3b bgr = UsebleImage.At<Vec3b>(y, x);
                    if ((sourceBgr[2] + valueChange) < 0)
                    {
                        bgr[2] = 0;
                    }
                    else if ((sourceBgr[2] + valueChange) > 255)
                    {
                        bgr[2] = 255;
                    }
                    else
                    {
                        bgr[2] = (byte)(sourceBgr[2] + valueChange);
                    }
                    UsebleImage.At<Vec3b>(y, x) = bgr;
                }
            }
            return UsebleImage;
        }
        public Mat ChangeGreen(int valueChange)
        {
            Mat UsebleImage = new Mat();
            sourceImage.CopyTo(UsebleImage);
            for (int y = 0; y < UsebleImage.Height; y++)
            {
                for (int x = 0; x < UsebleImage.Width; x++)
                {
                    Vec3b sourceBgr = sourceImage.At<Vec3b>(y, x);
                    Vec3b bgr = UsebleImage.At<Vec3b>(y, x);
                    if ((sourceBgr[1] + valueChange) < 0)
                    {
                        bgr[1] = 0;
                    }
                    else if ((sourceBgr[1] + valueChange) > 255)
                    {
                        bgr[1] = 255;
                    }
                    else
                    {
                        bgr[1] = (byte)(sourceBgr[1] + valueChange);
                    }
                    UsebleImage.At<Vec3b>(y, x) = bgr;
                }
            }
            return UsebleImage;
        }
        public Mat ChangeBlue(int valueChange)
        {
            Mat UsebleImage = new Mat();
            sourceImage.CopyTo(UsebleImage);
            for (int y = 0; y < UsebleImage.Height; y++)
            {
                for (int x = 0; x < UsebleImage.Width; x++)
                {
                    Vec3b sourceBgr = sourceImage.At<Vec3b>(y, x);
                    Vec3b bgr = UsebleImage.At<Vec3b>(y, x);
                    if ((sourceBgr[0] + valueChange) < 0)
                    {
                        bgr[0] = 0;
                    }
                    else if ((sourceBgr[0] + valueChange) > 255)
                    {
                        bgr[0] = 255;
                    }
                    else
                    {
                        bgr[0] = (byte)(sourceBgr[0] + valueChange);
                    }
                    UsebleImage.At<Vec3b>(y, x) = bgr;
                }
            }
            return UsebleImage;
        }
    }
}
