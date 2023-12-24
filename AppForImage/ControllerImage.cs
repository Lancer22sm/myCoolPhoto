using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AppForImage
{
    public class ControllerImage
    {
        ModelImage _modelImage = new();
        ControllerConvert _controllerConvert = new();
        UseEffectBlur _useEffectBlur;
        static string[] args = Environment.GetCommandLineArgs();
        static string filepath = args[1];
        //static string filepath = "C:/Users/Lancer/Pictures/GameCenter/Warface/Warface_sample.jpg";
        Mat src = Cv2.ImRead(filepath);
        Stack<Mat> stackChanges = new();
        static string imageFormat = FindMyImageFormat();
        public event Action IsUseMatEffect;

        public ControllerImage()
        {
            _modelImage.AddNaturalImage(src);
            _modelImage.ChangeImage(src);
            _useEffectBlur = new(src);
        }
        public void ChangeColor()
        {
            Mat dst = _modelImage.GetChangedImage();
            for (int y = 0; y < dst.Height; y++)
            {
                for (int x = 0; x < dst.Width; x++)
                {
                    Vec3b bgr = dst.At<Vec3b>(y, x);
                    bgr[0] = 255;
                    //bgr[1] = bgr[2];
                    dst.At<Vec3b>(y, x) = bgr;
                }
            }
            _modelImage.ChangeImage(dst);
            IsUseMatEffect?.Invoke();
        }
        public Mat GetMyChangedImage()
        {
            return _modelImage.GetChangedImage();
        }
        public void ChangeImageForEffects(Mat changedImage)
        {
            _modelImage.ChangeImage(changedImage);
            _useEffectBlur.ChangeSrcForEffect(_modelImage.GetChangedImage());
        }
        public BitmapImage GetMyImage()
        {
            return _controllerConvert.MatToBitmap(_modelImage.GetChangedImage(), imageFormat);
        }
        static string FindMyImageFormat()
        {
            string[] findImageForm = filepath.Split('.');
            return "." + findImageForm[findImageForm.Length - 1];
        }
        public BitmapImage ImageDownload()
        {
            return _controllerConvert.MatToBitmap(_modelImage.GetNaturalImage(), imageFormat);
        }
        public void MatBlur(int valueBlur)
        {
            Mat copiesMat = _useEffectBlur.GeneralEffect(valueBlur);
            _modelImage.ChangeImage(copiesMat);
            IsUseMatEffect?.Invoke();
        }
        public void BilateralFilter(int valueBlur)
        {
            Mat copiesMat = _useEffectBlur.BilateralFilter(valueBlur);
            _modelImage.ChangeImage(copiesMat);
            IsUseMatEffect?.Invoke();
        }
        public void BoxFilter(int valueBlur)
        {
            Mat copiesMat = _useEffectBlur.BoxFilter(valueBlur);
            _modelImage.ChangeImage(copiesMat);
            IsUseMatEffect?.Invoke();
        }
        public void MedianBlur(int valueBlur)
        {
            Mat copiesMat = _useEffectBlur.MedianBlur(valueBlur);
            _modelImage.ChangeImage(copiesMat);
            IsUseMatEffect?.Invoke();
        }
        public BitmapImage GetStack()
        {
            if(stackChanges.Count > 0)
            {
                Mat backMat = stackChanges.Pop();
                _modelImage.ChangeImage(backMat);
            }
            return _controllerConvert.MatToBitmap(_modelImage.GetChangedImage(), imageFormat);
        }
        public void PushStack()
        {
            stackChanges.Push(_modelImage.GetChangedImage());
        }
    }
}
