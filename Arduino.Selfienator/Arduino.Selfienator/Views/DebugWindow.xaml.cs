using Arduino.Selfienator.Core;
using Arduino.Selfienator.Core.Events;
using Arduino.Selfienator.Core.Events.Debug;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Arduino.Selfienator.Views
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window, ISubscriber<ECloseWindow>, ISubscriber<ESetFocus>
    {
        public DebugWindow()
        {
            InitializeComponent();
            EventAggregator.Instance.Subsribe(this);
            this.DataContext = new DebugViewModel(this.GetHashCode());
        }

        public void OnEventHandler(ECloseWindow e)
        {
            if (e.hashCode == this.GetHashCode())
            {
                this.Close();
            }
            if (e.targetWindow == "DebugWindow")
            {
                this.Close();
            }
        }

        public void OnEventHandler(ESetFocus e)
        {
            if (e.targetWindow == "DebugWindow")
            {
                this.Focus();
            } 
        }

        private void RichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void RichTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ((RichTextBox)sender).ScrollToEnd();
            //TODO: scroll all the way down
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EventAggregator.Instance.PublishEvent<ECancelDebug>(new ECancelDebug());
        }
    }
}
