using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AppForImage
{
    internal class UseEffects
    {
        ControllerConvert _controllerConvert = new();
        public BitmapImage MatBlur(int valueBlur, Mat src, Mat myMat, string imageFormat)
        {
            Cv2.Blur(src, myMat, new Size(valueBlur, valueBlur));
            return _controllerConvert.MatToBitmap(myMat, imageFormat);
        }
    }
}
