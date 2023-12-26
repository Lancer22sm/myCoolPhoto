using OpenCvSharp;

namespace AppForImage.Models
{
    class ModelImage
    {
        Mat myImage = new();
        Mat changeImage = new();

        public void AddNaturalImage(Mat naturalImage)
        {
            myImage = naturalImage;
        }
        public Mat GetNaturalImage()
        {
            return myImage;
        }
        public void ChangeImage(Mat changedImage)
        {
            changeImage = changedImage;
        }
        public Mat GetChangedImage()
        {
            return changeImage;
        }

    }
}
