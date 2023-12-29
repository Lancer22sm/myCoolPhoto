using AppForImage.Effects;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForImage.Controllers
{
    internal class ControllerBlur
    {
        WindowEffectBlur _windowEffectBlur;
        UseEffectBlur _useEffectBlur;
        public ControllerBlur(WindowEffectBlur windowEffectBlur, int chanelsImage)
        {
            _windowEffectBlur = windowEffectBlur;
            _useEffectBlur = new(chanelsImage);
        }

        public Mat ChangeFullBlur(Mat sourceImage, bool onChangePreview)
        {
            int valueBlur = Convert.ToInt32(_windowEffectBlur.mySliderBlur.Value);
            int valueMedianBlur = Convert.ToInt32(_windowEffectBlur.mySliderMedianBlur.Value);
            int valueBoxFilter = Convert.ToInt32(_windowEffectBlur.mySliderBoxFilter.Value);
            int valueBilateralFilter = Convert.ToInt32(_windowEffectBlur.mySliderBilateralFilter.Value);
            if (onChangePreview)
            {
                valueBlur = (valueBlur / 10) + 1;
                valueMedianBlur = (valueMedianBlur / 10) + 1;
                valueBoxFilter = (valueBoxFilter / 10) + 1;
                valueBilateralFilter = (valueBilateralFilter / 10) + 1;
            }
            if (valueMedianBlur % 2 == 0)
            {
                valueMedianBlur++;
            }
            return _useEffectBlur.GeneralEffect(sourceImage, valueBlur, valueMedianBlur, valueBoxFilter, valueBilateralFilter);
        }
    }
}
