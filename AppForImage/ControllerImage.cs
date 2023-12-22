using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AppForImage
{
    public class ControllerImage
    {
        ModelImage _modelImage = new();
        ControllerConvert _controllerConvert = new();
        static string[] args = Environment.GetCommandLineArgs();
        static string filepath = args[1];
        Mat myMat = new Mat();
        Mat src = Cv2.ImRead(filepath);
        Stack<Mat> stackChanges = new Stack<Mat>();
        static string imageFormat = FindMyImageFormat();
        public event Action UseMatBlur;

        public ControllerImage()
        {
            _modelImage.AddNaturalImage(src);
        }
        public BitmapImage GetMyImage()
        {
            return _controllerConvert.MatToBitmap(_modelImage.GetChangeImage(), imageFormat);
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

            Cv2.Blur(src, myMat, new OpenCvSharp.Size(valueBlur, valueBlur));
            Mat copiesMat = new();
            myMat.CopyTo(copiesMat);
            PushStack(copiesMat);
            UseMatBlur?.Invoke();
        }
        public BitmapImage GetStack()
        {
            if(stackChanges.Count > 0)
            {
                Mat backMat = stackChanges.Pop();
                _modelImage.ChangeImage(backMat);
            }
            return _controllerConvert.MatToBitmap(_modelImage.GetChangeImage(), imageFormat);
        }
        public void PushStack(Mat copies)
        {
            _modelImage.ChangeImage(copies);
            stackChanges.Push(_modelImage.GetChangeImage());
        }
    }
}
