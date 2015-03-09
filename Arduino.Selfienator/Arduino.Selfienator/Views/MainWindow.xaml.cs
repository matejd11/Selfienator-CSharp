using Arduino.Selfienator.Core;
using Arduino.Selfienator.Core.Events;
using Arduino.Selfienator.ViewModels;
using System.Windows;

namespace Arduino.Selfienator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISubscriber<ECloseWindow>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(this.GetHashCode());
        }

        public void OnEventHandler(ECloseWindow e)
        {
            if (e.hashCode == this.GetHashCode())
            {
                this.Close();
            }
        }
    }
}
