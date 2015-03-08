using Arduino.Selfienator.ViewModels;
using System.Windows;

namespace Arduino.Selfienator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(this.GetHashCode());
        }
    }
}
