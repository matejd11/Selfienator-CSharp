using Arduino.Selfienator.Core;
using Arduino.Selfienator.Core.Events;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Arduino.Selfienator.Views
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window, ISubscriber<ECloseWindow>
    {
        public DebugWindow()
        {
            InitializeComponent();
            EventAggregator.getInstance().SubsribeEvent(this);
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

        private void RichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void RichTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ((RichTextBox)sender).ScrollToEnd();
            //TODO: scroll all the way down
        }
    }
}
