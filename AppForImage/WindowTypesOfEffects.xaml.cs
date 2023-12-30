using System;
using System.Windows;
using System.Windows.Input;
using AppForImage.Controllers;

namespace AppForImage
{
    public partial class WindowTypesOfEffects : Window
    {
        private readonly WindowEffectBlur _windowEffectBlur;
        private readonly WindowEffectColorize _windowEffectColorize;
        private readonly ControllerImage _controller;
        public event Action OnEndChange;
        //public event Action OnStartChange;
        public WindowTypesOfEffects(ControllerImage controller, WindowEffectBlur effectblur, WindowEffectColorize windowEffectColorize)
        {
            InitializeComponent();
            _controller = controller;
            _windowEffectBlur = effectblur;
            _windowEffectBlur.MyEventBlurValueChanged += OnValueEffectChange;
            _windowEffectBlur.MyEventSavedImage += OnSavedImageForEffects;
            _windowEffectBlur.MyEventSavedImageStack += OnSavedImageStack;
            _windowEffectBlur.MyEventOnEndChangeImage += OnEndChangeImageForEffects;
            _windowEffectColorize = windowEffectColorize;
            _windowEffectColorize.MyEventColorValueChanged += OnValueEffectChange;
            _windowEffectColorize.MyEventSavedImage += OnSavedImageForEffects;
            _windowEffectColorize.MyEventSavedImageStack += OnSavedImageStack;
            _windowEffectColorize.MyEventOnEndChangeImage += OnEndChangeImageForEffects;
        }
        private void OnEndChangeImageForEffects()
        {
            _controller.StopChangePreviewImage();
            OnValueEffectChange();
            OnEndChange?.Invoke();
            // Перед отжиманием слайдеров
        }
        private void OnValueEffectChange()
        {
            _controller.ChangeImageFromEffects();
        }
        private void OnSavedImageForEffects()
        {
            _controller.ChangeSourceImageFromEffects();
        }
        private void OnSavedImageStack()
        {
            _controller.PushStack();
            //OnStartChange?.Invoke();
            _controller.StartChangePreviewImage();
            // перед зажиманием слайдеров
        }

        private void ButtonBlur_Click(object sender, RoutedEventArgs e)
        {
            _windowEffectBlur.Show();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void myGeneralBackground_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void myButtonColors_Click(object sender, RoutedEventArgs e)
        {
            _windowEffectColorize.Show();
        }
    }
}
