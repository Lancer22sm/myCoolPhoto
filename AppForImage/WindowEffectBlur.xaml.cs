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
        public event Action<double> MyeventBlurValueChanged;
        public event Action<double> MyeventMedianBlurValueChanged;
        public event Action MyEventSavedImage;
        private bool isSaved = true;
        public WindowEffectBlur()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void mySliderBlur_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MyeventBlurValueChanged?.Invoke(mySliderBlur.Value);
        }

        private void mySliderBlur_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSaved)
            {
                MyEventSavedImage?.Invoke();
                isSaved = false;
            }
        }

        private void mySliderBlur_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isSaved = true;
        }
        private void mySliderMedianBlur_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MyeventMedianBlurValueChanged?.Invoke(mySliderMedianBlur.Value);
        }

        private void mySliderMedianBlur_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSaved)
            {
                MyEventSavedImage?.Invoke();
                isSaved = false;
            }
        }

        private void mySliderMedianBlur_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isSaved = true;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
