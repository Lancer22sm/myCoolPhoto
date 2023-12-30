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
        readonly WindowEffectBlur _windowEffectBlur;
        readonly UseEffectBlur _useEffectBlur;
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
                valueBlur /= 10;
                valueMedianBlur /= 10;
                valueBoxFilter /= 10;
                valueBilateralFilter /= 10;
            }
            if (valueBlur == 0) valueBlur = 1;
            if (valueMedianBlur % 2 == 0) valueMedianBlur++;
            if (valueBoxFilter == 0) valueBoxFilter = 1;
            if (valueBilateralFilter == 0) valueBilateralFilter = 1;
            return _useEffectBlur.GeneralEffect(sourceImage, valueBlur, valueMedianBlur, valueBoxFilter, valueBilateralFilter);
        }
    }
}
