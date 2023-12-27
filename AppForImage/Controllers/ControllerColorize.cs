using AppForImage.Effects;
using OpenCvSharp;
using System;

namespace AppForImage.Controllers
{
    internal class ControllerColorize
    {
        UseEffectColorize _useEffectColorize;
        WindowEffectColorize _windowEffectColorize;
        Mat _useImage;
        public ControllerColorize(WindowEffectColorize windowEffectColorize, Mat useImage)
        {
            _useImage = useImage;
            _useEffectColorize = new(_useImage);
            _windowEffectColorize = windowEffectColorize;
        }
        public void ChangeUseImageForColorize(Mat useImage)
        {
            _useImage = useImage;
        }
        public Mat ChangeColor(int redvalue, int greenvalue, int bluevalue)
        {
            if (greenvalue == 0 & bluevalue == 0 & redvalue != 0)
            {
                _useImage = _useEffectColorize.ChangeRed(redvalue);
            }
            else if (bluevalue == 0 & redvalue == 0 & greenvalue != 0)
            {
                _useImage = _useEffectColorize.ChangeGreen(greenvalue);
            }
            else if (redvalue == 0 & greenvalue == 0 & bluevalue != 0)
            {
                _useImage = _useEffectColorize.ChangeBlue(bluevalue);
            }
            return _useImage;
        }
        public Mat ChangeFullColor()
        {
            _useEffectColorize.OnSaveOtherEffect();
            int redvalue = Convert.ToInt32(_windowEffectColorize.mySliderRed.Value);
            int greenvalue = Convert.ToInt32(_windowEffectColorize.mySliderGreen.Value);
            int bluevalue = Convert.ToInt32(_windowEffectColorize.mySliderBlue.Value);
            if (redvalue != 0) _useImage = _useEffectColorize.ChangeRed(redvalue);
            if (greenvalue != 0) _useImage = _useEffectColorize.ChangeGreen(greenvalue);
            if (bluevalue != 0) _useImage = _useEffectColorize.ChangeBlue(bluevalue);
            return _useImage;
        }


        public Mat ChangeColor(Mat myMat, int redvalue, int greenvalue, int bluevalue)
        {
            if (greenvalue == 0 & bluevalue == 0 & redvalue != 0)
            {
                _useImage = _useEffectColorize.ChangeRed(myMat, redvalue);
            }
            else if (bluevalue == 0 & redvalue == 0 & greenvalue != 0)
            {
                _useImage = _useEffectColorize.ChangeGreen(myMat, greenvalue);
            }
            else if (redvalue == 0 & greenvalue == 0 & bluevalue != 0)
            {
                _useImage = _useEffectColorize.ChangeBlue(myMat, bluevalue);
            }
            return _useImage;
        }
        public Mat ChangeFullColor(Mat myMat)
        {
            _useEffectColorize.OnSaveOtherEffect();
            int redvalue = Convert.ToInt32(_windowEffectColorize.mySliderRed.Value);
            int greenvalue = Convert.ToInt32(_windowEffectColorize.mySliderGreen.Value);
            int bluevalue = Convert.ToInt32(_windowEffectColorize.mySliderBlue.Value);
            if (redvalue != 0) _useImage = _useEffectColorize.ChangeRed(myMat, redvalue);
            if (greenvalue != 0) _useImage = _useEffectColorize.ChangeGreen(myMat, greenvalue);
            if (bluevalue != 0) _useImage = _useEffectColorize.ChangeBlue(myMat, bluevalue);
            return _useImage;
        }
    }
}
