using Arduino.Selfienator.Core;
using Arduino.Selfienator.Core.Events;
using Arduino.Selfienator.Models;
using System;
using System.IO.Ports;
using System.Windows;
using System.Windows.Input;

namespace Arduino.Selfienator.ViewModels
{
    public class LoadWindowViewModel : ViewModel
    {
        private string[] _listOfPorts;
        private string _selectedPort;
        private int[] _listOfBitRates;
        private int _selectetBitRate;
        private bool _isEditAllowed;

        public LoadWindowViewModel()
            : this(0)
        {
        }

        public LoadWindowViewModel(int hashCode)
        {
            _windowHashCode = hashCode;
            listOfPorts = SerialPort.GetPortNames();
            listOfBitRates = new[] { 2400, 4800, 9600, 14400, 19200, 28800, 38400, 57600, 76800, 11500, 230400, 250000, 500000, 1000000 };
            selectedBitRate = 9600;
        }

        public ICommand connectComm { get { return new ActionCommand(connect, canConnect); } }

        public ICommand refreshComm { get { return new ActionCommand(refresh); } }

        private void refresh(object obj)
        {
            listOfPorts = SerialPort.GetPortNames();
        }

        private bool canConnect(object obj)
        {
            var values = (object[])obj;
            if (values[0] == null || values[1] == null)
            {
                return false;
            }
            if (!String.IsNullOrWhiteSpace((string)values[0]) && (string)values[0] != "nenájdené")
            {
                return true;
            }
            return false;
        }

        private void connect(object obj)
        {
            var values = (object[])obj;
            string portName = (string)values[0];
            int bitRate = (int)values[1];

            //TODO: Check if port is still opened, Open mainWindow, Open port

            if (Serial.GetInstance(portName, bitRate) != null)
            {
                EventAggregator.getInstance().PublishEvent<ECloseWindow>(new ECloseWindow() { hashCode = _windowHashCode });
                WindowFactory<MainWindow>.getInstance().CreateNewWindow();
            }

        }

        public string[] listOfPorts
        {
            get { return _listOfPorts; }
            set
            {
                _listOfPorts = value;
                isEditAllowed = true;
                if (value.Length == 0)
                {
                    _listOfPorts = new string[] { "nenájdené" };
                    isEditAllowed = false;
                }
                if (_listOfPorts.Length == 1)
                {
                    selectedPort = _listOfPorts[0];
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

        public int[] listOfBitRates
        {
            get { return _listOfBitRates; }
            set
            {
                _listOfBitRates = value;
                NotifyPropertyChanged();
            }
        }

        public int selectedBitRate
        {
            get { return _selectetBitRate; }
            set
            {
                _selectetBitRate = value;
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
