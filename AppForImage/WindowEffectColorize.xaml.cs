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
        public event Action MyEventSavedImageStack;
        private Stack<double> stackRedValue = new();
        private Stack<double> stackGreenValue = new();
        private Stack<double> stackBlueValue = new();
        public Stack<Stack<double>> _stackChangiesHistory;
        public Dictionary<Stack<double>, Slider> dictionaryStackSliders;
        private bool isSaved = true;
        private bool isEnabledSliders = false;
        public WindowEffectColorize(Stack<Stack<double>> stackChangiesHistory)
        {
            InitializeComponent();
            _stackChangiesHistory = stackChangiesHistory;
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
                isEnabledSliders = true;
                stackRedValue.Push(mySliderRed.Value);
                _stackChangiesHistory.Push(stackRedValue);
                MyEventSavedImageStack?.Invoke();
                isSaved = false;
            }
        }

        private void mySliderRed_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isEnabledSliders = false;
            isSaved = true;
            MyEventSavedImage?.Invoke();
        }

        private void mySliderRed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(isEnabledSliders)
            {
                MyEventRedValueChanged?.Invoke(mySliderRed.Value);
            }
        }

        private void mySliderGreen_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSaved)
            {
                isEnabledSliders = true;
                stackGreenValue.Push(mySliderGreen.Value);
                _stackChangiesHistory.Push(stackGreenValue);
                MyEventSavedImageStack?.Invoke();
                isSaved = false;
            }
        }

        private void mySliderGreen_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isEnabledSliders = false;
            isSaved = true;
            MyEventSavedImage?.Invoke();
        }

        private void mySliderGreen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(isEnabledSliders)
            {
                MyEventGreenValueChanged?.Invoke(mySliderGreen.Value);
            }
        }

        private void mySliderBlue_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSaved)
            {
                isEnabledSliders = true;
                stackBlueValue.Push(mySliderBlue.Value);
                _stackChangiesHistory.Push(stackBlueValue);
                MyEventSavedImageStack?.Invoke();
                isSaved = false;
            }
        }

        private void mySliderBlue_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isEnabledSliders = false;
            isSaved = true;
            MyEventSavedImage?.Invoke();
        }

        private void mySliderBlue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(isEnabledSliders)
            {
                MyEventBlueValueChanged?.Invoke(mySliderBlue.Value);
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
