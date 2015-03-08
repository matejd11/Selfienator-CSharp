using Arduino.Selfienator.Core;
using Arduino.Selfienator.Models;
using System.Windows.Input;

namespace Arduino.Selfienator.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private int _windowHashCode;

        public MainWindowViewModel()
        {

        }

        public MainWindowViewModel(int hashCode)
        {
            _windowHashCode = hashCode;
        }

        public ICommand zapComm { get { return new ActionCommand(zapni); } } 
        public ICommand vypComm { get { return new ActionCommand(vypni); } }

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
