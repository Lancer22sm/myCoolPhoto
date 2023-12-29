using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppForImage
{
    public partial class WindowEffectColorize : Window
    {
        public event Action MyEventColorValueChanged;
        public event Action MyEventOnEndChangeImage;
        public event Action MyEventSavedImage; // используй для кнопки сохранения
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
            MyEventOnEndChangeImage?.Invoke();
        }

        private void mySliderRed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(isEnabledSliders)
            {
                MyEventColorValueChanged?.Invoke();            
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
            MyEventOnEndChangeImage?.Invoke();
        }

        private void mySliderGreen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(isEnabledSliders)
            {
                MyEventColorValueChanged?.Invoke();
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
            MyEventOnEndChangeImage?.Invoke();
        }

        private void mySliderBlue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(isEnabledSliders)
            {
                MyEventColorValueChanged?.Invoke();
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
