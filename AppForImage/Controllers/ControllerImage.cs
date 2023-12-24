﻿using AppForImage.Effects;
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
            _useEffectColorize = new(src);
        }
        public Mat GetMyChangedImage()
        {
            return _modelImage.GetChangedImage();
        }
        public void ChangeImageForEffects(Mat changedImage)
        {
            _modelImage.ChangeImage(changedImage);
            _useEffectBlur.ChangeSrcForEffect(_modelImage.GetChangedImage());
            _useEffectColorize.ChangeSrcForEffect(_modelImage.GetChangedImage());
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
        public void ChangeColor(int Redvalue, int Greenvalue, int Bluevalue)
        {
            byte valueRed = (byte)Redvalue;
            byte valueGreen = (byte)Greenvalue;
            byte valueBlue = (byte)Bluevalue;
            Mat copiesMat = _useEffectColorize.GeneralEffect(valueRed, valueGreen, valueBlue);
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
            if (stackChanges.Count > 0)
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