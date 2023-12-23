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
    /// Логика взаимодействия для TypersOfEffects.xaml
    /// </summary>
    public partial class WindowTypesOfEffects : Window
    {
        private WindowEffectBlur _windowEffectBlur;
        private ControllerImage _controller;
        public WindowTypesOfEffects(ControllerImage controller, WindowEffectBlur effectblur)
        {
            _controller = controller;
            _windowEffectBlur = effectblur;
            _windowEffectBlur.MyEventBlurValueChanged += OnValueBlurChange;
            _windowEffectBlur.MyEventBoxFilterValueChanged += OnValueBoxFilterChange;
            _windowEffectBlur.MyEventMedianBlurValueChanged += OnValueMedianBlurChange;
            _windowEffectBlur.MyEventBilateralFilterValueChanged += OnValueBilateralFilterChange;
            _windowEffectBlur.MyEventSavedImage += OnSavedImage;
            InitializeComponent();
        }
        private void OnValueBilateralFilterChange(double value)
        {
            _controller.BilateralFilter(Convert.ToInt32(value));
        }
        private void OnValueBoxFilterChange(double value)
        {
            _controller.BoxFilter(Convert.ToInt32(value));
        }
        private void OnValueBlurChange(double value)
        {
            _controller.MatBlur(Convert.ToInt32(value));
        }
        private void OnValueMedianBlurChange(double value)
        {
            int ticks = Convert.ToInt32(value);
            if(ticks % 2 == 0)
            {
                ticks++;
            }
            _controller.MedianBlur(ticks);
        }
        private void OnSavedImage()
        {
            _controller.ChangeImageForEffects(_controller.GetMyChangedImage());
            _controller.PushStack();
        }

        private void ButtonBlur_Click(object sender, RoutedEventArgs e)
        {
            _windowEffectBlur.Show();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void myGeneralBackground_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
