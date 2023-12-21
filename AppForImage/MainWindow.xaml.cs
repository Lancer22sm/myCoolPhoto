using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using OpenCvSharp;

namespace AppForImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        ControllerConvert _controllerConvert = new ();
        UseEffects _useEffects = new();
        static string[] args = Environment.GetCommandLineArgs();
        static string filepath = args[1];
        static string imageFormat = FindMyImageFormat();
        static string pathCache = "C:/Users/Lancer/source/repos/AppForImage/AppForImage/Resources/myImage.jpg";
        Mat myMat = new Mat();
        Mat src = Cv2.ImRead(filepath);
        BitmapImage bi = ImageDownload();
        Stack<Mat> stackChanges = new Stack<Mat>();


        public MainWindow()
        {
            InitializeComponent();
            UseImage();
        }

        static string FindMyImageFormat()
        {
            string[] findImageForm = filepath.Split('.');
            return "." + findImageForm[findImageForm.Length-1];
        }

        void UseImage()
        {
            myImageBackground.Source = bi;
        }

        static BitmapImage ImageDownload()
        {
            BitmapImage downloadImage = new();
            downloadImage.BeginInit();
            downloadImage.UriSource = new Uri(filepath, UriKind.RelativeOrAbsolute);
            downloadImage.EndInit();

            return downloadImage;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Bitmap myImage = new Bitmap(filepath);
            Bitmap myImage = _controllerConvert.BitmapImage2Bitmap(bi);
            myImage.Save(pathCache);
        }

        private void mySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int valueBlur = Convert.ToInt32(mySlider.Value);
            myImageBackground.Source = _useEffects.MatBlur(valueBlur, src, myMat, imageFormat);

        }

        private void myWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Z && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                // код при нажатии Ctrl+Z
                if(stackChanges.Count > 0)
                {
                    Mat backMat = stackChanges.Pop();
                    bi = _controllerConvert.MatToBitmap(backMat, imageFormat);
                    myImageBackground.Source = bi;
                }
            }
        }
        private void mySlider_MouseEnter(object sender, MouseEventArgs e)
        {
            Mat copiesMat = new Mat();
            myMat.CopyTo(copiesMat);
            stackChanges.Push(copiesMat);
        }
    }
}
