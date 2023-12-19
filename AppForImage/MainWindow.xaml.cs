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
        static string[] args = Environment.GetCommandLineArgs();
        string filepath = args[1];
        bool isCache = false;
        int strokeAdd = 0;
        int columnAdd = 0;
        static string pathCache = "C:/Users/Lancer/source/repos/AppForImage/AppForImage/Resources/myImage.jpg";
        Mat myMat = new Mat();
        Mat src = Cv2.ImRead(pathCache);
        BitmapImage bi = new BitmapImage();


        public MainWindow()
        {
            InitializeComponent();
            ImageDownload();
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
            if (isCache)
            {
                MatBlur(strokeAdd, columnAdd);
            } else
            {
                Bitmap myImage = new Bitmap(filepath);
                myImage.Save(pathCache);
                MatBlur(strokeAdd, columnAdd);
                isCache = true;
            }

        }

        public BitmapImage MatToBitmap(Mat matimg)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = new MemoryStream(MatToByteArray(matimg));
            bmp.EndInit();
            return bmp;
        }

        public byte[] MatToByteArray(Mat mat)
        {
            List<byte> lstbyte = new List<byte>();
            byte[] btArr = lstbyte.ToArray();
            int[] param = new int[2] { 1, 80 };
            Cv2.ImEncode(".jpg", mat, out btArr, param);
            return btArr;
        }

            private void MatBlur(int stroke, int column)
        {
            Cv2.Blur(src, myMat, new OpenCvSharp.Size(stroke, column));
            src = myMat;
            bi = MatToBitmap(myMat);
            //myMat.SaveImage(pathCache);
            //bi.BeginInit();
            //bi.UriSource = new Uri(pathCache, UriKind.RelativeOrAbsolute);
            //bi.EndInit();
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
            strokeAdd = Convert.ToInt32(mySlider.Value);
            columnAdd = Convert.ToInt32(mySlider.Value);
        }
    }
}
