using AppForImage.Effects;
using AppForImage.Models;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AppForImage.Controllers
{
    public class ControllerImage
    {
        ModelImage _modelImage = new();
        ControllerConvert _controllerConvert = new();
        UseEffectBlur _useEffectBlur;
        UseEffectColorize _useEffectColorize;
        WindowEffectColorize _windowEffectColorize;
        static string[] args = Environment.GetCommandLineArgs();
        static string filepath = args[1];
        //static string filepath = "C:/Users/Lancer/Pictures/GameCenter/Warface/Warface_sample.jpg";
        Mat src = Cv2.ImRead(filepath, ImreadModes.Unchanged);
        Stack<Mat> stackChanges = new();
        static string imageFormat = FindMyImageFormat();
        public event Action IsUseMatEffect;

        public ControllerImage(WindowEffectColorize windowEffectColorize)
        {
            _windowEffectColorize = windowEffectColorize;
            _modelImage.AddNaturalImage(src);
            _modelImage.ChangeImage(src);
            _useEffectBlur = new(src);
            _useEffectBlur.GeneralEffect(1, 1, 1, 1);
            _useEffectColorize = new(_useEffectBlur.usebleImageBilateralFilterBlur);
        }
        public Mat GetMyChangedImage()
        {
            return _modelImage.GetChangedImage();
        }
        public void ChangeImageFromEffectColorize(Mat changedImage)
        {
            Mat save = new();
            changedImage.CopyTo(save);
            _modelImage.ChangeImage(save);
            _useEffectBlur.ChangeSrcForEffect(_modelImage.GetChangedImage());
        }
        public void ChangeImageFromEffectBlur()
        {
            src = _modelImage.GetChangedImage();
            _useEffectBlur.ChangeSrcForEffect(src);
            _useEffectColorize.ChangeSrcForEffect(_useEffectBlur.usebleImageBilateralFilterBlur);
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
            src = _useEffectBlur.GeneralEffect(blurValue, medianBlurValue, boxFilterValue, bilateralFilterValue);
            int redvalue = Convert.ToInt32(_windowEffectColorize.mySliderRed.Value);
            int greenvalue = Convert.ToInt32(_windowEffectColorize.mySliderGreen.Value);
            int bluevalue = Convert.ToInt32(_windowEffectColorize.mySliderBlue.Value);
            _modelImage.ChangeImage(src);
            IsUseMatEffect?.Invoke();
            ChangeColor(redvalue, greenvalue, bluevalue);
        }
        public void ChangeColor(int redvalue, int greenvalue, int bluevalue)
        {
            if (greenvalue == 0 & bluevalue == 0) src = _useEffectColorize.ChangeRed(redvalue);
            else if (bluevalue == 0 & redvalue == 0) src = _useEffectColorize.ChangeGreen(greenvalue);
            else if (redvalue == 0 & greenvalue == 0) src = _useEffectColorize.ChangeBlue(bluevalue);
            _modelImage.ChangeImage(src);
            IsUseMatEffect?.Invoke();
        }
        public BitmapImage GetStack()
        {
            if (stackChanges.Count > 0)
            {
                Mat backMat = stackChanges.Pop();
                _modelImage.ChangeImage(backMat);
                ChangeImageFromEffectBlur();
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
