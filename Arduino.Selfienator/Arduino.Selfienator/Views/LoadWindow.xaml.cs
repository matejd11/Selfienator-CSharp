using Arduino.Selfienator.ViewModels;
using System.Windows;

namespace Arduino.Selfienator.Views
{
    /// <summary>
    /// Interaction logic for LoadWindow.xaml
    /// </summary>
    public partial class LoadWindow : Window
    {
        public LoadWindow()
        {
            InitializeComponent();
            this.DataContext = new LoadWindowViewModel();
        }
    }
}
