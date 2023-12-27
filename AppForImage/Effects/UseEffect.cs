using OpenCvSharp;

namespace AppForImage.Effects
{
    public abstract class UseEffect
    {
        Mat sourceImage;
        Mat UsebleImage;

        public Mat GeneralEffect()
        {
            return UsebleImage;
        }
        public abstract void ChangeSrcForEffect(Mat src);
    }
}
