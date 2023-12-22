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
    public partial class EffectBlur : Window
    {
        ControllerImage _controller = new();
        public EffectBlur()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void mySliderBlur_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int valueBlur = Convert.ToInt32(mySliderBlur.Value);
            _controller.MatBlur(valueBlur);
        }
    }
}
