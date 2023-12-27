using AppForImage.Effects;
using AppForImage.Models;
using OpenCvSharp;
using OpenCvSharp.Quality;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AppForImage.Controllers
{
    public class ControllerImage
    {
        ModelImage _modelImage = new();
        ControllerConvert _controllerConvert = new();
        UseEffectBlur _useEffectBlur;
        WindowEffectColorize _windowEffectColorize;
        ControllerColorize _controllerColorize;
        static string[] args = Environment.GetCommandLineArgs();
        static string filepath = args[1];
        //static string filepath = "C:/Users/Lancer/Pictures/9K0kU-dxOAQ.jpg";
        Mat src = Cv2.ImRead(filepath, ImreadModes.Unchanged);
        Mat useImage = new();
        Stack<Mat> stackChanges = new();
        static string imageFormat = FindMyImageFormat();
        public event Action IsUseMatEffect;
        bool isChangePreviewImage = false;
        Mat previewImage = new Mat();
        Mat previewImageSource = new Mat();
        Mat resize_image = new();
        Mat previewColor = new();

        public ControllerImage(WindowEffectColorize windowEffectColorize)
        {
            _windowEffectColorize = windowEffectColorize;
            _modelImage.AddNaturalImage(src);
            _modelImage.ChangeImage(src);
            _useEffectBlur = new(src, src.Channels());
            _useEffectBlur.GeneralEffect(1, 1, 1, 1);
            _controllerColorize = new(_windowEffectColorize, _useEffectBlur.usebleImageReturn);
            ChangeBlur(1, 1, 1, 1);
        }
        public Mat GetMyChangedImage()
        {
            return _modelImage.GetChangedImage();
        }
        public void ChangeImageFromEffects()
        {
            src = _modelImage.GetChangedImage();
            //_useEffectBlur.ChangeSrcForEffect(src);
            //_useEffectColorize.ChangeSrcForEffect(_useEffectBlur.usebleImageBilateralFilterBlur);
        }
        public BitmapImage GetMyImagePreview()
        {
            return _controllerConvert.MatToBitmap(previewImage, imageFormat, true);
        }
        public BitmapImage GetMyImage()
        {
            return _controllerConvert.MatToBitmap(_modelImage.GetChangedImage(), imageFormat, false);
        }
        static string FindMyImageFormat()
        {
            string[] findImageForm = filepath.Split('.');
            return "." + findImageForm[findImageForm.Length - 1];
        }
        public BitmapImage ImageDownload()
        {
            return _controllerConvert.MatToBitmap(_modelImage.GetNaturalImage(), imageFormat, false);
        }
        public void StartChangePreviewImage()
        {
            double powerResize = 1;
            int[] param = new int[2] { 1, 10 };
            if (src.Height + src.Width > 2000) powerResize = 0.8;
            if (src.Height + src.Width > 3000) powerResize = 0.7;
            if (src.Height + src.Width > 4000) powerResize = 0.6;
            if (src.Height + src.Width > 5000) powerResize = 0.5;
            if (src.Height + src.Width > 6000) powerResize = 0.4;
            Cv2.Resize(src, resize_image, new OpenCvSharp.Size(), powerResize, powerResize, InterpolationFlags.Linear);
            Cv2.ImEncode(imageFormat, resize_image, out byte[] btArr, param);
            previewImageSource = Cv2.ImDecode(btArr, ImreadModes.Unchanged);
            isChangePreviewImage = true;
        }
        public void StopChangePreviewImage()
        {
            isChangePreviewImage = false;
        }
        public void ChangeBlur(int blurValue, int medianBlurValue, int boxFilterValue, int bilateralFilterValue)
        {
            if(isChangePreviewImage)
            {
                previewColor = _useEffectBlur.GeneralEffect(previewImageSource, blurValue, medianBlurValue, boxFilterValue, bilateralFilterValue);
                previewImage = _controllerColorize.ChangeFullColor(previewColor);
                IsUseMatEffect?.Invoke();
            }
            else
            {
                useImage = _useEffectBlur.GeneralEffect(blurValue, medianBlurValue, boxFilterValue, bilateralFilterValue);
                useImage = _controllerColorize.ChangeFullColor();
                _modelImage.ChangeImage(useImage);
            }
        }
        public void ChangeColor(int redvalue, int greenvalue, int bluevalue)
        {
            if(isChangePreviewImage)
            {
                previewImage = _controllerColorize.ChangeColor(previewImageSource, redvalue, greenvalue, bluevalue);
                IsUseMatEffect?.Invoke();
            }
            else
            {
                useImage = _controllerColorize.ChangeColor(redvalue, greenvalue, bluevalue);
                _modelImage.ChangeImage(useImage);
            }
        }
        public BitmapImage GetStack()
        {
            if (stackChanges.Count > 0)
            {
                Mat backMat = stackChanges.Pop();
                backMat.CopyTo(useImage);
            }
            return _controllerConvert.MatToBitmap(_modelImage.GetChangedImage(), imageFormat, false);
        }
        public void PushStack()
        {
            Mat CopiesToStack = new();
            _modelImage.GetChangedImage().CopyTo(CopiesToStack);
            stackChanges.Push(CopiesToStack);
        }
    }
}
