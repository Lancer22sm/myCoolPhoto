using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using OpenCvSharp;

namespace AppForImage
{
    class ModelImage
    {
        Mat myImage = new();
        Mat changeImage = new();

        public void AddNaturalImage(Mat naturalImage)
        {
            myImage = naturalImage;
        }
        public Mat GetNaturalImage()
        {
            return myImage;
        }
        public void ChangeImage(Mat changedImage) 
        {
            changeImage = changedImage;
        }
        public Mat GetChangedImage()
        {
            return changeImage;
        }
        
    }
}
