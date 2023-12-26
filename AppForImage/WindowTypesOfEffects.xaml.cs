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
using AppForImage.Controllers;

namespace AppForImage
{
    /// <summary>
    /// Логика взаимодействия для TypersOfEffects.xaml
    /// </summary>
    public partial class WindowTypesOfEffects : Window
    {
        private WindowEffectBlur _windowEffectBlur;
        private WindowEffectColorize _windowEffectColorize;
        private ControllerImage _controller;
        public WindowTypesOfEffects(ControllerImage controller, WindowEffectBlur effectblur, WindowEffectColorize windowEffectColorize)
        {
            InitializeComponent();
            _controller = controller;
            _windowEffectBlur = effectblur;
            _windowEffectBlur.MyEventBlurValueChanged += OnValueBlurChange;
            _windowEffectBlur.MyEventSavedImage += OnSavedImageForEffects;
            _windowEffectBlur.MyEventSavedImageStack += OnSavedImageStack;
            _windowEffectColorize = windowEffectColorize;
            _windowEffectColorize.MyEventRedValueChanged += OnValueRedChange;
            _windowEffectColorize.MyEventGreenValueChanged += OnValueGreenChange;
            _windowEffectColorize.MyEventBlueValueChanged += OnValueBlueChange;
            _windowEffectColorize.MyEventSavedImage += OnSavedImageForEffects;
            _windowEffectColorize.MyEventSavedImageStack += OnSavedImageStack;
        }

        private void OnValueRedChange(double value)
        {
            _controller.ChangeColor(Convert.ToInt32(value), 0, 0);
        }

        private void OnValueGreenChange(double value)
        {
            _controller.ChangeColor(0, Convert.ToInt32(value), 0);
        }

        private void OnValueBlueChange(double value)
        {
            _controller.ChangeColor(0, 0, Convert.ToInt32(value));
        }
        private void OnValueBlurChange(double valueBlur, double valueMedianBlur, double valueBoxFilter, double valueBilateralFilter)
        {
            int blurValue = Convert.ToInt32(valueBlur);
            int medianBlurValue = Convert.ToInt32(valueMedianBlur);
            if (medianBlurValue % 2 == 0)
            {
                medianBlurValue++;
            }
            int boxFilterValue = Convert.ToInt32(valueBoxFilter);
            int bilateralFilterValue = Convert.ToInt32(valueBilateralFilter);
            _controller.ChangeBlur(blurValue, medianBlurValue, boxFilterValue, bilateralFilterValue);
        }
        private void OnSavedImageForEffects()
        {
            _controller.ChangeImageFromEffects();
        }
        private void OnSavedImageStack()
        {
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

        private void myButtonColors_Click(object sender, RoutedEventArgs e)
        {
            _windowEffectColorize.Show();
        }
    }
}
