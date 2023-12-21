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
        static string[] args = Environment.GetCommandLineArgs();
        static string filepath = args[1];
        static string imageFormat = FindMyImageFormat();
        bool isCache = false;
        static string pathCache = "C:/Users/Lancer/source/repos/AppForImage/AppForImage/Resources/myImage.jpg";
        Mat myMat = new Mat();
        Mat src = Cv2.ImRead(filepath);
        BitmapImage bi = new BitmapImage();
        Mat[] savedImage = new Mat[10];
        int i = 0;


        public MainWindow()
        {
            InitializeComponent();
            ImageDownload();
        }

        static string FindMyImageFormat()
        {
            string[] findImageForm = filepath.Split('.');
            return "." + findImageForm[findImageForm.Length-1];
        }

        void ImageDownload()
        {
            
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(filepath, UriKind.RelativeOrAbsolute);
            bi.EndInit();

            //вывод картинки
            myImageBackground.Source = bi;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Bitmap myImage = new Bitmap(filepath);
            Bitmap myImage = _controllerConvert.BitmapImage2Bitmap(bi);
            myImage.Save(pathCache);
        }
        private void MatBlur(int stroke, int column)
        {
            Cv2.Blur(src, myMat, new OpenCvSharp.Size(stroke, column));
            bi = _controllerConvert.MatToBitmap(myMat, imageFormat);
            myImageBackground.Source = bi;
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            myButtonBlur.Opacity += 50;
        }

        private void myButtonBlur_MouseLeave(object sender, MouseEventArgs e)
        {
            myButtonBlur.Opacity -= 50;
        }

        private void mySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int strokeAdd = Convert.ToInt32(mySlider.Value);
            int columnAdd = Convert.ToInt32(mySlider.Value);
            MatBlur(strokeAdd, columnAdd);

        }

        private void myWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Z && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                // код при нажатии Ctrl+Z
            }
        }
    }
}
