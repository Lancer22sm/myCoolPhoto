using OpenCvSharp;

namespace AppForImage.Models
{
    class ModelImage
    {
        Mat myImage = new();
        Mat changeImage = new();

        public void AddNaturalImage(Mat naturalImage)
        {
            naturalImage.CopyTo(myImage);
        }
        public Mat GetNaturalImage()
        {
            return myImage;
        }
        public void ChangeImage(Mat changedImage)
        {
            changedImage.CopyTo(changeImage);
        }
        public Mat GetChangedImage()
        {
            return changeImage;
        }

    }
}
