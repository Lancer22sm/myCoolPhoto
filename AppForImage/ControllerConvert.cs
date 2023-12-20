using OpenCvSharp;
using System;
using System.Collections.Generic;
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
        public BitmapImage MatToBitmap(Mat matimg)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = new MemoryStream(MatToByteArray(matimg));
            bmp.EndInit();
            return bmp;
        }
        private byte[] MatToByteArray(Mat mat)
        {
            List<byte> lstbyte = new List<byte>();
            byte[] btArr = lstbyte.ToArray();
            int[] param = new int[2] { 1, 80 };
            Cv2.ImEncode(".jpg", mat, out btArr, param);
            return btArr;
        }
    }
}
