using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using AppForImage.Controllers;
using OpenCvSharp;

namespace AppForImage
{
    public partial class MainWindow : System.Windows.Window
    {
        private readonly WindowTypesOfEffects _typesOfEffects;
        private readonly WindowEffectColorize _windowEffectColorize;
        private readonly WindowEffectBlur _windowEffectsBlur;
        private readonly ControllerImage _controller;
        private List<RadioButton> _radioButtons = new();
        private int indexRadioButton = 0;
        private Line lastLine = new();
        Dictionary<Stack<double>, Slider> generalDictionary = new();
        public Stack<Stack<double>> stackChangiesHistory = new();
        bool isUseWidgetMask = true; // добавь кнопку типо включить режим выделения, создание точек настроенно
        public MainWindow()
        {
            InitializeComponent();
            _windowEffectColorize = new(stackChangiesHistory);
            _windowEffectsBlur = new(stackChangiesHistory);
            _controller = new(_windowEffectColorize, _windowEffectsBlur);
            _typesOfEffects = new(_controller, _windowEffectsBlur, _windowEffectColorize);
            myImageBackground.Source = _controller.ImageDownload();
            _controller.IsUseMatEffect += PreviewChangesImage;
            _typesOfEffects.OnEndChange += ChangesImage;
            GenerateGeneralDictionary();
            CreateFunctionDoubleClick();
        }
        private void CreateFunctionDoubleClick()
        {
            MouseDoubleClick += (sender1, e1) =>
            {
                if (_radioButtons.Count > 2)
                {
                    Line line = new Line();
                    myGridInImage.Children.Remove(_radioButtons[indexRadioButton]);
                    myGridInImage.Children.Remove(lastLine);
                    _radioButtons.Remove(_radioButtons[indexRadioButton]);
                    indexRadioButton--;
                    RadioButton but1 = _radioButtons[indexRadioButton];
                    RadioButton but2 = _radioButtons[0];
                    line.X1 = but1.RenderTransform.Value.OffsetX + (but1.Width / 2);
                    line.Y1 = but1.RenderTransform.Value.OffsetY + (but1.Height / 2);
                    line.X2 = but2.RenderTransform.Value.OffsetX + (but2.Width / 2);
                    line.Y2 = but2.RenderTransform.Value.OffsetY + (but2.Height / 2);
                    line.Stroke = Brushes.White;
                    line.StrokeThickness = 2;
                    Panel.SetZIndex(line, 2);
                    myGridInImage.Children.Add(line);
                }
            };
        }
        private void GenerateGeneralDictionary()
        {
            Dictionary<Stack<double>, Slider> dictionaryBlur = _windowEffectsBlur.dictionaryStackSliders;
            Dictionary<Stack<double>, Slider> dictionaryColorize = _windowEffectColorize.dictionaryStackSliders;
            foreach (var item in dictionaryBlur)
            {
                generalDictionary.Add(item.Key, item.Value);
            }
            foreach (var item in dictionaryColorize)
            {
                generalDictionary.Add(item.Key, item.Value);
            }
        }
        private void ButtonEffects_Click(object sender, RoutedEventArgs e)
        {
            _typesOfEffects.Show();
        }
        private void PreviewChangesImage()
        {
            myImageBackground.Source = _controller.GetMyImagePreview();
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
                if (stackChangiesHistory.Count > 0)
                {
                    Stack<double> myHistory = stackChangiesHistory.Pop();
                    foreach (var item in generalDictionary)
                    {
                        if (item.Key == myHistory)
                        {
                            item.Value.Value = myHistory.Pop();
                        }
                    }
                }
                myImageBackground.Source = _controller.GetStack();
            }
        }

        private void myWindow_Closed(object sender, EventArgs e)
        {
            _windowEffectsBlur.Close();
            _windowEffectColorize.Close();
            _typesOfEffects.Close();
        }

        private void myImageBackground_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (isUseWidgetMask)
            {
                System.Windows.Point currentPosition = e.GetPosition(myImageBackground);
                double x = currentPosition.X;
                double y = currentPosition.Y;
                RadioButton radioButton = new RadioButton();
                radioButton.Name = $"radioButton{indexRadioButton}";
                radioButton.PreviewMouseDown += RadioButtonsMouseDown;
                radioButton.Width = 20;
                radioButton.Height = 20;
                radioButton.HorizontalAlignment = HorizontalAlignment.Left;
                radioButton.VerticalAlignment = VerticalAlignment.Top;
                Panel.SetZIndex(radioButton, 2);
                radioButton.RenderTransform = new TranslateTransform(x, y);
                _radioButtons.Add(radioButton);
                myGridInImage.Children.Add(radioButton);
                CreateLinesForRadioButtons();
                //_controller.CreatePointsToMask(Convert.ToInt32(x), Convert.ToInt32(y));
                //myImageBackground.Source = _controller.GetMyImage();
            }
        }
        private void RadioButtonsMouseDown(object sender, MouseButtonEventArgs e) // при зажатии клавиши мыши на кнопках
        {
            //RadioButton radioButton = (RadioButton)sender;
            //нужен обработчик для кнопки и линий чтобы их передвигать
        }

        private void CreateLinesForRadioButtons()
        {
            if (_radioButtons.Count >= 2)
            {
                Line line = new Line();
                RadioButton but1 = _radioButtons[indexRadioButton];
                indexRadioButton++;
                RadioButton but2 = _radioButtons[indexRadioButton];
                line.X1 = but1.RenderTransform.Value.OffsetX + (but1.Width / 2);
                line.Y1 = but1.RenderTransform.Value.OffsetY + (but1.Height / 2);
                line.X2 = but2.RenderTransform.Value.OffsetX + (but2.Width / 2);
                line.Y2 = but2.RenderTransform.Value.OffsetY + (but2.Height / 2);
                line.Stroke = Brushes.White;
                line.StrokeThickness = 2;
                Panel.SetZIndex(line, 2);
                myGridInImage.Children.Add(line);
                lastLine = line;
            }
        }
    }
}
