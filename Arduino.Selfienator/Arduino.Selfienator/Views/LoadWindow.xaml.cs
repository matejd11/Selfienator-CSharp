﻿using Arduino.Selfienator.Core;
using Arduino.Selfienator.Core.Events;
using Arduino.Selfienator.ViewModels;
using System.Windows;

namespace Arduino.Selfienator.Views
{
    /// <summary>
    /// Interaction logic for LoadWindow.xaml
    /// </summary>
    public partial class LoadWindow : Window, ISubscriber<ECloseWindow>
    {
        public LoadWindow()
        {
            InitializeComponent();
            EventAggregator.Instance.Subsribe(this);
            this.DataContext = new LoadWindowViewModel(this.GetHashCode());
        }

        public void OnEventHandler(ECloseWindow e)
        {
            if (e.hashCode == this.GetHashCode())
            {
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
