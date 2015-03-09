using Arduino.Selfienator.Core;
using Arduino.Selfienator.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        }

        private void RichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}
