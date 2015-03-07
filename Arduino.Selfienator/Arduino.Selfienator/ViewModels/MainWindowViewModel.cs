using Arduino.Selfienator.Models;
using EkonomyFinal.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Arduino.Selfienator.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {

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
