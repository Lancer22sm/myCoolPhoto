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
        readonly ModelImage _modelImage = new();
        readonly ControllerConvert _controllerConvert = new();
        readonly WindowEffectBlur _windowEffectBlur;
        readonly WindowEffectColorize _windowEffectColorize;
        readonly ControllerColorize _controllerColorize;
        readonly ControllerBlur _controllerBlur;
        static readonly string[] args = Environment.GetCommandLineArgs();
        static readonly string filepath = args[1];
        //static string filepath = "C:/Users/Lancer/Pictures/9K0kU-dxOAQ.jpg";
        
        Mat src = Cv2.ImRead(filepath, ImreadModes.Unchanged);
        Mat useImage = new();
        Mat useImageBlur = new();

        readonly Mat previewImageSource = Cv2.ImRead(filepath, ImreadModes.ReducedColor8);
        Mat previewImage = new();
        Mat previewBlur = new();

        Stack<Mat> stackChanges = new();
        static readonly string imageFormat = FindMyImageFormat();
        public event Action IsUseMatEffect;
        bool isChangePreviewImage = false;

        public ControllerImage(WindowEffectColorize windowEffectColorize, WindowEffectBlur windowEffectBlur)
        {
            _windowEffectColorize = windowEffectColorize;
            _windowEffectBlur = windowEffectBlur;
            _modelImage.AddNaturalImage(src);
            _modelImage.ChangeImage(src);
            _controllerBlur = new(_windowEffectBlur, src.Channels());
            _controllerColorize = new(_windowEffectColorize);
        }
        public Mat GetMyChangedImage()
        {
            return _modelImage.GetChangedImage();
        }
        public void ChangeSourceImageFromEffects()
        {
            src = _modelImage.GetChangedImage();
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
        public void StartChangePreviewImage() // перед зажатием слайдеров
        {
            isChangePreviewImage = true;
        }
        public void StopChangePreviewImage() // перед отжатием слайдеров
        {
            isChangePreviewImage = false;
        }
        public void ChangeImageFromEffects()
        {
            if(isChangePreviewImage)
            {
                previewBlur = _controllerBlur.ChangeFullBlur(previewImageSource, true);
                _controllerColorize.ChangeUseImageForColorize(previewBlur);
                previewImage = _controllerColorize.ChangeFullColor(previewBlur);
                IsUseMatEffect?.Invoke();
            }
            else
            {
                useImageBlur = _controllerBlur.ChangeFullBlur(src, false);
                _controllerColorize.ChangeUseImageForColorize(useImageBlur);
                useImage = _controllerColorize.ChangeFullColor(useImageBlur);
                _modelImage.ChangeImage(useImage);
            }
        }
        public BitmapImage GetStack()
        {
            if (stackChanges.Count > 0)
            {
                Mat backMat = stackChanges.Pop();
                _modelImage.ChangeImage(backMat);
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
