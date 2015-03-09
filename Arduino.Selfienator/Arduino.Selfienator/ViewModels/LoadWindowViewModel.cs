using Arduino.Selfienator.Core;
using Arduino.Selfienator.Views;
using System;
using System.IO.Ports;
using System.Windows.Input;

namespace Arduino.Selfienator.ViewModels
{
    public class LoadWindowViewModel : ViewModel
    {
        private string[] _listOfPorts;
        private string _selectedPort;
        private bool _isEditAllowed;
        private int _windowHashCode;

        public LoadWindowViewModel() : this(0)
        {
        }

        public LoadWindowViewModel(int hashCode)
        {
            _windowHashCode = hashCode;
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

            //TODO: Check if port is still opened, Open mainWindow, Open port

            EventAggregator.getInstance().PublishEvent<ECloseWindow>(new ECloseWindow() { hashCode = _windowHashCode });
            WindowFactory<MainWindow>.getInstance().CreateNewWindow();
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
