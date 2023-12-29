﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        Dictionary<Stack<double>, Slider> generalDictionary = new();
        public Stack<Stack<double>> stackChangiesHistory = new();
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
            Dictionary<Stack<double>, Slider> dictionaryBlur = _windowEffectsBlur.dictionaryStackSliders;
            Dictionary<Stack<double>, Slider> dictionaryColorize = _windowEffectColorize.dictionaryStackSliders;
            foreach(var item in dictionaryBlur)
            {
                generalDictionary.Add(item.Key, item.Value);
            }
            foreach(var item in dictionaryColorize)
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
    }
}
