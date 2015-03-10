using Arduino.Selfienator.Core;
using Arduino.Selfienator.Models;
using Arduino.Selfienator.Views;
using System;
using System.Windows;
using System.Windows.Input;

namespace Arduino.Selfienator.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
         private ArrowHelper _xArrow; 

        public MainWindowViewModel()
            : this(0)
        {

        }

        public MainWindowViewModel(int hashCode)
        {
            _xArrow = new ArrowHelper();
            _windowHashCode = hashCode;
            directions = new int[] { 0, 1 };
        }
        public int angle { get; set; }
        public int direction { get; set; }
        public int delay { get; set; }
        public int[] directions { get; set; }

        public ArrowHelper xArrow
        {
            get { return _xArrow; }
            set
            {
                _xArrow = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand zapComm { get { return new ActionCommand(zapni); } }
        public ICommand vypComm { get { return new ActionCommand(vypni); } }
        public ICommand sendComm { get { return new ActionCommand(send); } }
        public ICommand debugOnComm { get { return new ActionCommand(startDebug); } }
        public ICommand leftComm { get { return new ActionCommand(left); } }
        public ICommand rightComm { get { return new ActionCommand(right); } }

        //DEBUG
        private void right(object obj)
        {
            _xArrow.angle += 5;
            
        }
        private void left(object obj)
        {
            _xArrow.angle -= 5;
        }
        //ENDDEBUG

        private void startDebug(object obj)
        {
            WindowFactory<DebugWindow>.getInstance().CreateNewWindow();
        }

        private void send(object obj)
        {
            //DEBUG
            var commands = new Commands();

            Serial.GetInstance().send(Serial.getCommands().motor(new double[] { angle, 360 - angle }, new int[] { direction, 1 - direction }, new int[] { delay, 10 - delay }, new char[] { 'A', 'B' }));
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
