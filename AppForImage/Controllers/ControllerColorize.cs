using AppForImage.Effects;
using OpenCvSharp;
using System;

namespace AppForImage.Controllers
{
    internal class ControllerColorize
    {
        readonly UseEffectColorize _useEffectColorize = new();
        readonly WindowEffectColorize _windowEffectColorize;
        public ControllerColorize(WindowEffectColorize windowEffectColorize)
        {
            _windowEffectColorize = windowEffectColorize;
        }
        public void ChangeUseImageForColorize(Mat useImage)
        {
            _useEffectColorize.OnSaveOtherEffect(useImage);
        }
        public Mat ChangeFullColor(Mat myMat)
        {
            int redvalue = Convert.ToInt32(_windowEffectColorize.mySliderRed.Value);
            int greenvalue = Convert.ToInt32(_windowEffectColorize.mySliderGreen.Value);
            int bluevalue = Convert.ToInt32(_windowEffectColorize.mySliderBlue.Value);
            if (redvalue != 0) myMat = _useEffectColorize.ChangeRed(myMat, redvalue);
            if (greenvalue != 0) myMat = _useEffectColorize.ChangeGreen(myMat, greenvalue);
            if (bluevalue != 0) myMat = _useEffectColorize.ChangeBlue(myMat, bluevalue);
            return myMat;
        }
    }
}
