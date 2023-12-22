using System;
using System.Windows;
using System.Windows.Input;

namespace AppForImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TypesOfEffects _typesOfEffects = new();
        EffectBlur _blur = new();
        ControllerImage _controller = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonEffects_Click(object sender, RoutedEventArgs e)
        {
            _typesOfEffects.Show();
        }

        private void myWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Z && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                // код при нажатии Ctrl+Z
                _controller.GetStack();
            }
        }
        private void mySlider_MouseEnter(object sender, MouseEventArgs e)
        {
            _controller.PushStack();
        }

        private void myWindow_Closed(object sender, EventArgs e)
        {
            _blur.Close();
            _typesOfEffects.Close();
        }
    }
}
