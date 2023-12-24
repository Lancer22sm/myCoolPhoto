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
    /// Логика взаимодействия для WindowEffectColorize.xaml
    /// </summary>
    public partial class WindowEffectColorize : Window
    {
        public event Action<double> MyEventRedValueChanged;
        public event Action<double> MyEventGreenValueChanged;
        public event Action<double> MyEventBlueValueChanged;
        public event Action MyEventSavedImage;
        private Stack<double> stackRedValue = new();
        private Stack<double> stackGreenValue = new();
        private Stack<double> stackBlueValue = new();
        public Stack<Stack<double>> stackChangiesHistory = new();
        public Dictionary<Stack<double>, Slider> dictionaryStackSliders;
        private bool isSaved = true;
        public WindowEffectColorize()
        {
            InitializeComponent();
            dictionaryStackSliders = new()
            {
                { stackRedValue, mySliderRed },
                { stackGreenValue, mySliderGreen },
                { stackBlueValue, mySliderBlue }
            };
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void mySliderRed_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSaved)
            {
                stackRedValue.Push(mySliderRed.Value);
                stackChangiesHistory.Push(stackRedValue);
                MyEventSavedImage?.Invoke();
                isSaved = false;
            }
        }

        private void mySliderRed_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isSaved = true;
        }

        private void mySliderRed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MyEventRedValueChanged?.Invoke(mySliderRed.Value);
        }

        private void mySliderGreen_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSaved)
            {
                stackGreenValue.Push(mySliderGreen.Value);
                stackChangiesHistory.Push(stackGreenValue);
                MyEventSavedImage?.Invoke();
                isSaved = false;
            }
        }

        private void mySliderGreen_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isSaved = true;
        }

        private void mySliderGreen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MyEventGreenValueChanged?.Invoke(mySliderGreen.Value);
        }

        private void mySliderBlue_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSaved)
            {
                stackBlueValue.Push(mySliderBlue.Value);
                stackChangiesHistory.Push(stackBlueValue);
                MyEventSavedImage?.Invoke();
                isSaved = false;
            }
        }

        private void mySliderBlue_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isSaved = true;
        }

        private void mySliderBlue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MyEventBlueValueChanged?.Invoke(mySliderBlue.Value);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
