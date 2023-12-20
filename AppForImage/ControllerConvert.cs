using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AppForImage
{
    internal class ControllerConvert
    {
        public BitmapImage MatToBitmapJpg(Mat matimg)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = new MemoryStream(MatToByteArray(matimg, ".jpg"));
            bmp.EndInit();
            return bmp;
        }
        public BitmapImage MatToBitmapPng(Mat matimg)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = new MemoryStream(MatToByteArray(matimg, ".png"));
            bmp.EndInit();
            return bmp;
        }
        private byte[] MatToByteArray(Mat mat, string imageFormat)
        {
            List<byte> lstbyte = new List<byte>();
            byte[] btArr = lstbyte.ToArray();
            int[] param = new int[2] { 1, 80 };
            Cv2.ImEncode(imageFormat, mat, out btArr, param);
            return btArr;
        }
        public Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }
    }
}
