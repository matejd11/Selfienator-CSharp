using Arduino.Selfienator.Core;
using Arduino.Selfienator.Models;
using Arduino.Selfienator.Views;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Arduino.Selfienator.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private ArrowHelper _xArrow;
        private ArrowHelper _yArrow;
        private Thread TMoveXArrow;

        public MainWindowViewModel()
            : this(0)
        {

        }

        public MainWindowViewModel(int hashCode)
        {
            xArrow = new ArrowHelper();
            yArrow = new ArrowHelper() { angle = 180};
            _windowHashCode = hashCode;
            directions = new int[] { 0, 1 };
            TMoveXArrow = new Thread(MoveXArrow);
            TMoveXArrow.IsBackground = true;
            TMoveXArrow.Start();

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
        public ArrowHelper yArrow
        {
            get { return _yArrow; }
            set
            {
                _yArrow = value;
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
            if ((string)obj == "X")
            {
                _xArrow.angle += 5;
            }
            else if ((string)obj == "Y")
            {
                _yArrow.angle += 5;
            }
        }
        private void left(object obj)
        {
            if ((string)obj == "X")
            {
                _xArrow.angle -= 5;
            }
            else if ((string)obj == "Y")
            {
                _yArrow.angle -= 5;
            }
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

            angle %= 360;

            Serial.GetInstance().send(Serial.getCommands().motor(new double[] { angle, 360 - angle }, new int[] { direction, 1 - direction }, new int[] { delay, 10 - delay }, new char[] { 'A', 'B' }));

            xArrow.startExecuting(angle, direction, delay);

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

        private void MoveXArrow(object obj)
        {
            while(true){
                xArrow.Update();
                yArrow.Update();
                Thread.Sleep(0);
            }
        }

    }
}
