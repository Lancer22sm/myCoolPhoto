using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AppForImage.Controllers;

namespace AppForImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly WindowTypesOfEffects _typesOfEffects;
        private readonly WindowEffectBlur _windowEffectsBlur = new();
        private readonly WindowEffectColorize _windowEffectColorize = new();
        private readonly ControllerImage _controller = new();
        public MainWindow()
        {
            InitializeComponent();
            _typesOfEffects = new(_controller, _windowEffectsBlur, _windowEffectColorize);
            myImageBackground.Source = _controller.ImageDownload();
            _controller.IsUseMatEffect += ChangesImage;
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
                if (_windowEffectsBlur.stackChangiesHistory.Count > 0)
                {
                    Stack<double> myHistory = _windowEffectsBlur.stackChangiesHistory.Pop();
                    Dictionary<Stack<double>, Slider> dictionary = _windowEffectsBlur.dictionaryStackSliders;
                    foreach (var item in dictionary)
                    {
                        if (item.Key == myHistory)
                        {
                            item.Value.Value = myHistory.Pop();
                        }
                    }
                } else if (_windowEffectColorize.stackChangiesHistory.Count > 0)
                {
                    Stack<double> myHistory = _windowEffectColorize.stackChangiesHistory.Pop();
                    Dictionary<Stack<double>, Slider> dictionary = _windowEffectColorize.dictionaryStackSliders;
                    foreach(var item in dictionary)
                    {
                        if (item.Key == myHistory)
                        {
                            item.Value.Value = myHistory.Pop();
                        }
                    }
                }
                myImageBackground.Source = _controller.GetStack(); ;
            }
        }

        private void myWindow_Closed(object sender, EventArgs e)
        {
            _windowEffectsBlur.Close();
            _windowEffectColorize.Close();
            _typesOfEffects.Close();
        }
    }
}
