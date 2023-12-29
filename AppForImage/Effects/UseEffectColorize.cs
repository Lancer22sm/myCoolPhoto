using OpenCvSharp;

namespace AppForImage.Effects
{
    internal class UseEffectColorize : UseEffect
    {
        Mat usebleImage = new();

        public void OnSaveOtherEffect(Mat myMat)
        {
            myMat.CopyTo(usebleImage);
        }
        public Mat ChangeRed(Mat myMat, int valueChange)
        {
            ChangeColor(myMat, valueChange, 2);
            return usebleImage;
        }
        public Mat ChangeGreen(Mat myMat, int valueChange)
        {
            ChangeColor(myMat, valueChange, 1);
            return usebleImage;
        }
        public Mat ChangeBlue(Mat myMat, int valueChange)
        {
            ChangeColor(myMat, valueChange, 0);
            return usebleImage;
        }
        private void ChangeColor(Mat myMat, int valueChange, int color)
        {
            for (int y = 0; y < myMat.Height; y++)
            {
                for (int x = 0; x < myMat.Width; x++)
                {
                    Vec3b sourceBgr = myMat.At<Vec3b>(y, x);
                    Vec3b bgr = usebleImage.At<Vec3b>(y, x);
                    if ((sourceBgr[color] + valueChange) < 0)
                    {
                        bgr[color] = 0;
                    }
                    else if ((sourceBgr[color] + valueChange) > 255)
                    {
                        bgr[color] = 255;
                    }
                    else
                    {
                        bgr[color] = (byte)(sourceBgr[color] + valueChange);
                    }
                    usebleImage.At<Vec3b>(y, x) = bgr;
                }
            }
        }
    }
}
