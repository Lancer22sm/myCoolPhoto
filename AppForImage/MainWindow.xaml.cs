using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace AppForImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly WindowTypesOfEffects _typesOfEffects;
        private readonly WindowEffectBlur _windowEffectsBlur = new();
        private readonly ControllerImage _controller = new();
        public MainWindow()
        {
            InitializeComponent();
            _typesOfEffects = new(_controller, _windowEffectsBlur);
            myImageBackground.Source = _controller.ImageDownload();
            _controller.UseMatBlur += ChangesImage;
        }

        private void ButtonEffects_Click(object sender, RoutedEventArgs e)
        {
            _typesOfEffects.Show();
        }
        private void ChangesImage()
        {
            myImageBackground.Source = _controller.GetMyImage();
        }

        private void myWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Z && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                // код при нажатии Ctrl+Z
                if (_windowEffectsBlur.stackChangiesHistory.Count == 0) return;
                Stack<double> myHistory = _windowEffectsBlur.stackChangiesHistory.Pop();
                if (myHistory == _windowEffectsBlur.stackBlurValue)
                {
                    _windowEffectsBlur.mySliderBlur.Value = myHistory.Pop();
                }
                else if(myHistory == _windowEffectsBlur.stackMedianBlurValue) 
                {
                    _windowEffectsBlur.mySliderMedianBlur.Value = myHistory.Pop();
                }
                else if(myHistory == _windowEffectsBlur.stackBoxFilterValue)
                {
                    _windowEffectsBlur.mySliderBoxFilter.Value = myHistory.Pop();
                }
                else if(myHistory == _windowEffectsBlur.stackBilateralFilterValue)
                {
                    _windowEffectsBlur.mySliderBilateralFilter.Value = myHistory.Pop();
                }
                myImageBackground.Source = _controller.GetStack();
            }
        }

        private void myWindow_Closed(object sender, EventArgs e)
        {
            _windowEffectsBlur.Close();
            _typesOfEffects.Close();
        }
    }
}
