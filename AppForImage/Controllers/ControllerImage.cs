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
        Mat useImageBlur = new();
        Mat useImageColor = new();

        Stack<Mat> stackChanges = new();
        static string imageFormat = FindMyImageFormat();
        public event Action IsUseMatEffect;
        bool isChangePreviewImage = false;

        Mat previewImage = new Mat();
        Mat previewImageSource = Cv2.ImRead(filepath, ImreadModes.ReducedColor8);
        Mat previewImageChangerSource = new Mat();
        Mat previewBlur = new();
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
            previewImageSource.CopyTo(previewImageChangerSource);
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
        public void StartChangePreviewImage() // перед зажатием слайдеров
        {
            isChangePreviewImage = true;
        }
        public void StopChangePreviewImage() // перед отжатием слайдеров
        {
            isChangePreviewImage = false;
        }
        public void ChangeBlur(int blurValue, int medianBlurValue, int boxFilterValue, int bilateralFilterValue)
        {
            if(isChangePreviewImage)
            {
                previewBlur = _useEffectBlur.GeneralEffect(previewImageSource, blurValue, medianBlurValue, boxFilterValue, bilateralFilterValue);
                _controllerColorize.ChangeUseImageForColorize(previewBlur);
                previewImage = _controllerColorize.ChangeFullColor(previewBlur);
                previewImage.CopyTo(previewColor);
                //MessageBox.Show(previewColor.ToString() + "Preview");
                IsUseMatEffect?.Invoke();
            }
            else
            {
                useImageBlur = _useEffectBlur.GeneralEffect(src, blurValue, medianBlurValue, boxFilterValue, bilateralFilterValue);
                _controllerColorize.ChangeUseImageForColorize(useImageBlur);
                useImageColor = _controllerColorize.ChangeFullColor(useImageBlur);
                useImage = useImageColor;
                _modelImage.ChangeImage(useImage);
            }
        }
        public void ChangeColor(int redvalue, int greenvalue, int bluevalue)
        {
            if(isChangePreviewImage)
            {
                //MessageBox.Show(previewColor.ToString() + "Preview");
                _controllerColorize.ChangeUseImageForColorize(previewColor); // подумай тут
                previewImage = _controllerColorize.ChangeColor(previewColor, redvalue, greenvalue, bluevalue);
                IsUseMatEffect?.Invoke();
            }
            else
            {
                //MessageBox.Show(useImageColor.ToString() + "useImage");
                _controllerColorize.ChangeUseImageForColorize(useImageColor); // подумай тут
                useImage = _controllerColorize.ChangeColor(useImageColor, redvalue, greenvalue, bluevalue);
                _modelImage.ChangeImage(useImage);
            }
        }
        public BitmapImage GetStack()
        {
            if (stackChanges.Count > 0)
            {
                Mat backMat = stackChanges.Pop();
                //backMat.CopyTo(useImage);
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
