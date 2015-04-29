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
            EventAggregator.Instance.Subsribe(this);
            this.DataContext = new MainWindowViewModel(this.GetHashCode());
        }

        public void OnEventHandler(ECloseWindow e)
        {
            if (e.hashCode == this.GetHashCode())
            {
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EventAggregator.Instance.PublishEvent<ECloseWindow>(new ECloseWindow() { targetWindow = "DebugWindow" });
        }
    }
}
