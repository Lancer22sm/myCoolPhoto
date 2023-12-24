using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AppForImage
{
    /// <summary>
    /// Логика взаимодействия для EffectBlur.xaml
    /// </summary>
    public partial class WindowEffectBlur : Window
    {
        public event Action<double> MyEventBlurValueChanged;
        public event Action<double> MyEventMedianBlurValueChanged;
        public event Action<double> MyEventBoxFilterValueChanged;
        public event Action<double> MyEventBilateralFilterValueChanged;
        public event Action MyEventSavedImage;
        private bool isSaved = true;
        private Stack<double> stackBlurValue = new();
        private Stack<double> stackMedianBlurValue = new();
        private Stack<double> stackBoxFilterValue = new();
        private Stack<double> stackBilateralFilterValue = new();
        public Stack<Stack<double>> stackChangiesHistory = new();
        public Dictionary<Stack<double>, Slider> dictionaryStackSliders;
        private bool isEnabledSliders = false;



        public WindowEffectBlur()
        {
            InitializeComponent();
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
                MyEventBlurValueChanged?.Invoke(mySliderBlur.Value);
            }
        }

        private void mySliderBlur_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSaved)
            {
                isEnabledSliders = true;
                stackBlurValue.Push(mySliderBlur.Value);
                stackChangiesHistory.Push(stackBlurValue);
                MyEventSavedImage?.Invoke();
                isSaved = false;
            }
        }

        private void mySliderBlur_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isEnabledSliders = false;
            isSaved = true;
        }
        private void mySliderMedianBlur_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(isEnabledSliders)
            {
                MyEventMedianBlurValueChanged?.Invoke(mySliderMedianBlur.Value);
            }
        }

        private void mySliderMedianBlur_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSaved)
            {
                isEnabledSliders = true;
                stackMedianBlurValue.Push(mySliderMedianBlur.Value);
                stackChangiesHistory.Push(stackMedianBlurValue);
                MyEventSavedImage?.Invoke();
                isSaved = false;
            }
        }

        private void mySliderMedianBlur_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isEnabledSliders = false;
            isSaved = true;
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
                MyEventBoxFilterValueChanged?.Invoke(mySliderBoxFilter.Value);
            }
        }

        private void mySliderBoxFilter_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSaved)
            {
                isEnabledSliders = true;
                stackBoxFilterValue.Push(mySliderBoxFilter.Value);
                stackChangiesHistory.Push(stackBoxFilterValue);
                MyEventSavedImage?.Invoke();
                isSaved = false;
            }
        }

        private void mySliderBoxFilter_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isEnabledSliders = false;
            isSaved = true;
        }

        private void mySliderBilateralFilter_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSaved)
            {
                isEnabledSliders = true;
                stackBilateralFilterValue.Push(mySliderBilateralFilter.Value);
                stackChangiesHistory.Push(stackBilateralFilterValue);
                MyEventSavedImage?.Invoke();
                isSaved = false;
            }
        }

        private void mySliderBilateralFilter_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isSaved = true;
            isEnabledSliders = false;
            MyEventBilateralFilterValueChanged?.Invoke(mySliderBilateralFilter.Value);
        }
    }
}
