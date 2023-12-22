using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace AppForImage
{
    internal class ControllerImage
    {
        private readonly MainWindow _mainWindow = new();
        private readonly ModelImage _modelImage = new();
        private readonly ControllerConvert _controllerConvert = new();
        static string[] args = Environment.GetCommandLineArgs();
        public static string filepath = args[1];
        Mat myMat = new Mat();
        Mat src = Cv2.ImRead(filepath);
        BitmapImage bi = ImageDownload();
        Stack<Mat> stackChanges = new Stack<Mat>();
        static string imageFormat = FindMyImageFormat();

        public ControllerImage()
        {
            _modelImage.AddNaturalImage(src);
            _mainWindow.myImageBackground.Source = bi;
        }

        static string FindMyImageFormat()
        {
            string[] findImageForm = filepath.Split('.');
            return "." + findImageForm[findImageForm.Length - 1];
        }
        static BitmapImage ImageDownload()
        {
            BitmapImage downloadImage = new();
            downloadImage.BeginInit();
            //downloadImage.UriSource = new Uri(filepath, UriKind.RelativeOrAbsolute);
            downloadImage.UriSource = new Uri("C:/Users/Lancer/source/repos/AppForImage/AppForImage/Resources/myImage.jpg", UriKind.RelativeOrAbsolute);
            downloadImage.EndInit();

            return downloadImage;
        }
        public BitmapImage MatBlur(int valueBlur)
        {
            Cv2.Blur(src, myMat, new Size(valueBlur, valueBlur));
            return _controllerConvert.MatToBitmap(myMat, imageFormat);
        }
        public void GetStack()
        {
            if(stackChanges.Count > 0)
            {
                Mat backMat = stackChanges.Pop();
                _modelImage.ChangeImage(backMat);
                _mainWindow.myImageBackground.Source = _controllerConvert.MatToBitmap(_modelImage.GetChangeImage(), imageFormat);
            }
        }
        public void PushStack()
        {
            Mat copiesMat = new Mat();
            myMat.CopyTo(copiesMat);
            _modelImage.ChangeImage(copiesMat);
            stackChanges.Push(_modelImage.GetChangeImage());
        }

    }
}
