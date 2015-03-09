using Arduino.Selfienator.Core;
using Arduino.Selfienator.Models;
using Arduino.Selfienator.Views;
using System.Windows;
using System.Windows.Input;

namespace Arduino.Selfienator.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {

        public MainWindowViewModel()
            : this(0)
        {

        }

        public MainWindowViewModel(int hashCode)
        {
            _windowHashCode = hashCode;
            directions = new int[] {0, 1};
        }
        public int angle { get; set; }
        public int direction { get; set; }
        public int delay { get; set; }
        public int[] directions { get; set; }


        public ICommand zapComm { get { return new ActionCommand(zapni); } }
        public ICommand vypComm { get { return new ActionCommand(vypni); } }
        public ICommand sendComm { get { return new ActionCommand(send); } }
        public ICommand debugOnComm { get { return new ActionCommand(startDebug); } }

        private void startDebug(object obj)
        {
            WindowFactory<DebugWindow>.getInstance().CreateNewWindow();
        }

        private void send(object obj)
        {
            //DEBUG
            var commands = new Commands();

            Serial.GetInstance().send(Serial.getCommands().motor(new double[] { angle, 360-angle }, new int[] { direction, 1-direction }, new int[]{ delay, 10-delay }, new char[] {'A', 'B'}));
            //ENDDEBUG
        }

        private void vypni(object obj)
        {
            Serial.GetInstance().send("2");
        }

        private void zapni(object obj)
        {
            Serial.GetInstance().send("1");
        }

    }
}
