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
        private bool _isEditAllowed;

        public LoadWindowViewModel()
        {
            listOfPorts = SerialPort.GetPortNames();
        }

        public ICommand connectComm { get { return new ActionCommand(connect, canConnect); } }

        public ICommand refreshComm { get { return new ActionCommand(refresh); } }

        private void refresh(object obj)
        {
            listOfPorts = SerialPort.GetPortNames();
        }

        private bool canConnect(object obj)
        {
            if (!String.IsNullOrWhiteSpace((string)obj) && (string)obj != "nenájdené")
            {
                return true;
            }
            return false;
        }

        private void connect(object obj)
        {
            string portName = (string)obj;


        }

        public string[] listOfPorts
        {
            get { return _listOfPorts; }
            set
            {
                _listOfPorts = value;
                if (value.Length == 0)
                {
                    _listOfPorts = new string[] {"nenájdené"};
                }
                if (_listOfPorts.Length == 1)
                {
                    selectedPort = _listOfPorts[0];
                    isEditAllowed = false;
                }
                else
                {

                    isEditAllowed = true;
                }
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
        public bool isEditAllowed
        {
            get { return _isEditAllowed; }
            set
            {
                _isEditAllowed = value;
                NotifyPropertyChanged();
            }
        }
    }
}
