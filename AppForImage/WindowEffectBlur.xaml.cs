using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppForImage
{
    public partial class WindowEffectBlur : Window
    {
        public event Action<double, double, double, double> MyEventBlurValueChanged;
        public event Action MyEventSavedImage; // используй для кнопки сохранения
        public event Action MyEventSavedImageStack;
        public event Action<double, double, double, double> MyEventOnEndChangeImage;
        private bool isSaved = true;
        private Stack<double> stackBlurValue = new();
        private Stack<double> stackMedianBlurValue = new();
        private Stack<double> stackBoxFilterValue = new();
        private Stack<double> stackBilateralFilterValue = new();
        public Stack<Stack<double>> _stackChangiesHistory;
        public Dictionary<Stack<double>, Slider> dictionaryStackSliders;
        private bool isEnabledSliders = false;



        public WindowEffectBlur(Stack<Stack<double>> stackChangiesHistory)
        {
            InitializeComponent();
            _stackChangiesHistory = stackChangiesHistory;
            dictionaryStackSliders = new()
            {
                { stackBlurValue, mySliderBlur },
                { stackMedianBlurValue, mySliderMedianBlur },
                { stackBoxFilterValue, mySliderBoxFilter },
                { stackBilateralFilterValue, mySliderBilateralFilter }
            };
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void mySliderBlur_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(isEnabledSliders)
            {
                MyEventBlurValueChanged?.Invoke(mySliderBlur.Value, mySliderMedianBlur.Value, mySliderBoxFilter.Value, mySliderBilateralFilter.Value);
            }
        }

        private void mySliderBlur_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSaved)
            {
                isEnabledSliders = true;
                stackBlurValue.Push(mySliderBlur.Value);
                _stackChangiesHistory.Push(stackBlurValue);
                MyEventSavedImageStack?.Invoke();
                isSaved = false;
            }
        }

        private void mySliderBlur_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isEnabledSliders = false;
            isSaved = true;
            MyEventOnEndChangeImage?.Invoke(mySliderBlur.Value, mySliderMedianBlur.Value, mySliderBoxFilter.Value, mySliderBilateralFilter.Value);
        }
        private void mySliderMedianBlur_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(isEnabledSliders)
            {
                MyEventBlurValueChanged?.Invoke(mySliderBlur.Value, mySliderMedianBlur.Value, mySliderBoxFilter.Value, mySliderBilateralFilter.Value);
            }
        }

        private void mySliderMedianBlur_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSaved)
            {
                isEnabledSliders = true;
                stackMedianBlurValue.Push(mySliderMedianBlur.Value);
                _stackChangiesHistory.Push(stackMedianBlurValue);
                MyEventSavedImageStack?.Invoke();
                isSaved = false;
            }
        }

        private void mySliderMedianBlur_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isEnabledSliders = false;
            isSaved = true;
            MyEventOnEndChangeImage?.Invoke(mySliderBlur.Value, mySliderMedianBlur.Value, mySliderBoxFilter.Value, mySliderBilateralFilter.Value);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void mySliderBoxFilter_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(isEnabledSliders)
            {
                MyEventBlurValueChanged?.Invoke(mySliderBlur.Value, mySliderMedianBlur.Value, mySliderBoxFilter.Value, mySliderBilateralFilter.Value);
            }
        }

        private void mySliderBoxFilter_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSaved)
            {
                isEnabledSliders = true;
                stackBoxFilterValue.Push(mySliderBoxFilter.Value);
                _stackChangiesHistory.Push(stackBoxFilterValue);
                MyEventSavedImageStack?.Invoke();
                isSaved = false;
            }
        }

        private void mySliderBoxFilter_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isEnabledSliders = false;
            isSaved = true;
            MyEventOnEndChangeImage?.Invoke(mySliderBlur.Value, mySliderMedianBlur.Value, mySliderBoxFilter.Value, mySliderBilateralFilter.Value);
        }

        private void mySliderBilateralFilter_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSaved)
            {
                isEnabledSliders = true;
                stackBilateralFilterValue.Push(mySliderBilateralFilter.Value);
                _stackChangiesHistory.Push(stackBilateralFilterValue);
                MyEventSavedImageStack?.Invoke();
                isSaved = false;
            }
        }

        private void mySliderBilateralFilter_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isSaved = true;
            isEnabledSliders = false;
            MyEventBlurValueChanged?.Invoke(mySliderBlur.Value, mySliderMedianBlur.Value, mySliderBoxFilter.Value, mySliderBilateralFilter.Value);
        }
    }
}
