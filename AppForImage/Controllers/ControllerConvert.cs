using OpenCvSharp;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace AppForImage.Controllers
{
    public class ControllerConvert
    {
        int lowQualityPng = 1;
        int lowQualityJpg = 10;
        int highQualityPng = 9;
        int highQualityJpg = 100;
        public BitmapImage MatToBitmap(Mat matimg, string imageFormat, bool isChangePreview)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            if (isChangePreview) bmp.StreamSource = new MemoryStream(MatToByteArray(matimg, imageFormat, lowQualityPng, lowQualityJpg));
            else bmp.StreamSource = new MemoryStream(MatToByteArray(matimg, imageFormat, highQualityPng, highQualityJpg));
            bmp.EndInit();
            return bmp;
        }
        public byte[] MatToByteArray(Mat mat, string imageFormat, int qualityPng, int qualityJpg)
        {
            int[] param = new int[2] { qualityPng, qualityJpg };
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
