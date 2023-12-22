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
    public partial class TypesOfEffects : Window
    {
        private EffectBlur _effectBlur;
        private ControllerImage _controller;
        public TypesOfEffects(ControllerImage controller, EffectBlur effectblur)
        {
            _controller = controller;
            _effectBlur = effectblur;
            _effectBlur.MyeventBlurValueChanged += OnValueChange;
            InitializeComponent();
        }

        private void OnValueChange(double value)
        {
            _controller.MatBlur(Convert.ToInt32(value));
        }

        private void ButtonBlur_Click(object sender, RoutedEventArgs e)
        {
            _effectBlur.Show();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
