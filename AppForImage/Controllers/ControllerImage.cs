using AppForImage.Effects;
using AppForImage.Models;
using OpenCvSharp;
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
        //static string filepath = "C:/Users/Lancer/Pictures/таблицы.png";
        Mat src = Cv2.ImRead(filepath, ImreadModes.Unchanged);
        Mat useImage = new();
        Stack<Mat> stackChanges = new();
        static string imageFormat = FindMyImageFormat();
        public event Action IsUseMatEffect;

        public ControllerImage(WindowEffectColorize windowEffectColorize)
        {
            _windowEffectColorize = windowEffectColorize;
            _modelImage.AddNaturalImage(src);
            _modelImage.ChangeImage(src);
            _useEffectBlur = new(src, src.Channels());
            _useEffectBlur.GeneralEffect(1, 1, 1, 1);
            _controllerColorize = new(_windowEffectColorize, _useEffectBlur.usebleImageReturn);
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
        public void ChangeBlur(int blurValue, int medianBlurValue, int boxFilterValue, int bilateralFilterValue)
        {
            useImage = _useEffectBlur.GeneralEffect(blurValue, medianBlurValue, boxFilterValue, bilateralFilterValue);
            useImage = _controllerColorize.ChangeFullColor();
            _modelImage.ChangeImage(useImage);
            IsUseMatEffect?.Invoke();
        }
        public void ChangeColor(int redvalue, int greenvalue, int bluevalue)
        {
            useImage = _controllerColorize.ChangeColor(redvalue, greenvalue, bluevalue);
            _modelImage.ChangeImage(useImage);
            IsUseMatEffect?.Invoke();
        }
        public BitmapImage GetStack()
        {
            if (stackChanges.Count > 0)
            {
                Mat backMat = stackChanges.Pop();
                backMat.CopyTo(useImage);
                IsUseMatEffect?.Invoke();
            }
            return _controllerConvert.MatToBitmap(_modelImage.GetChangedImage(), imageFormat);
        }
        public void PushStack()
        {
            Mat CopiesToStack = new();
            _modelImage.GetChangedImage().CopyTo(CopiesToStack);
            stackChanges.Push(CopiesToStack);
        }
    }
}
