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
    public class ControllerConvert
    {
        public BitmapImage MatToBitmap(Mat matimg, string imageFormat)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = new MemoryStream(MatToByteArray(matimg, imageFormat));
            bmp.EndInit();
            return bmp;
        }
        public byte[] MatToByteArray(Mat mat, string imageFormat)
        {
            int[] param = new int[2] { 1, 80 };
            Cv2.ImEncode(imageFormat, mat, out byte[] btArr, param);
            return btArr;
        }
        public Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }
    }
}
