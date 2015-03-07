using Arduino.Selfienator.Core;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Arduino.Selfienator.ViewModels
{
    public class LoadWindowViewModel : ViewModel
    {
        private string[] _listOfPorts;
        private string _selectedPort;

        public LoadWindowViewModel()
        {
            listOfPorts = SerialPort.GetPortNames();
        }

        public ICommand connectComm { get { return new ActionCommand(connect); } }

        private void connect(object obj)
        {
            string portName = (string)obj;

            MessageBox.Show(portName);

        }

        public string[] listOfPorts
        {
            get { return _listOfPorts; }
            set
            {
                _listOfPorts = value;
                NotifyPropertyChanged();
            }
        }

        public string selectedPort
        {
            get { return _selectedPort; }
            set
            {
                _selectedPort = value;
                NotifyPropertyChanged();
            }
        }
    }
}
