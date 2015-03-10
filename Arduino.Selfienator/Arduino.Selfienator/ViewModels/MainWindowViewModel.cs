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
        private CommandPropertyHelper _x;
        private CommandPropertyHelper _y;

        private Thread TMoveXArrow;

        public MainWindowViewModel()
            : this(0)
        {

        }

        public MainWindowViewModel(int hashCode)
        {
            x = new CommandPropertyHelper();
            y = new CommandPropertyHelper();
            xArrow = new ArrowHelper();
            yArrow = new ArrowHelper() { angle = 180 };
            _windowHashCode = hashCode;
            directions = new int[] { 0, 1 };
            TMoveXArrow = new Thread(MoveXArrow);
            TMoveXArrow.IsBackground = true;
            TMoveXArrow.Start();

        }
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
        public CommandPropertyHelper x
        {
            get { return _x; }
            set
            {
                _x = value;
                NotifyPropertyChanged();
            }
        }
        public CommandPropertyHelper y
        {
            get { return _y; }
            set
            {
                _y = value;
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

            if ((string)obj == "A")
            {
                var commands = new Commands();
                var angles = new double[] { x.angle, y.angle };
                var directions = new int[] { x.direction, y.direction };
                var delays = new int[] { x.delay, y.delay };
                var names = new char[] { 'X', 'Y' };

                x.angle %= 360;

                Serial.GetInstance().send(Serial.getCommands().motor(angles, directions, delays, names));

                xArrow.startExecuting(x.angle, x.direction, x.delay);

                yArrow.startExecuting(y.angle, y.direction, y.delay);
            }

            if ((string)obj == "X")
            {
                var commands = new Commands();

                x.angle %= 360;

                Serial.GetInstance().send(Serial.getCommands().motorX(x.angle, x.direction, x.delay));

                xArrow.startExecuting(x.angle, x.direction, x.delay);
            }

            if ((string)obj == "Y")
            {
                var commands = new Commands();

                y.angle %= 360;

                Serial.GetInstance().send(Serial.getCommands().motorY(y.angle, y.direction, y.delay));

                yArrow.startExecuting(y.angle, y.direction, y.delay);
            }

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
            while (true)
            {
                xArrow.Update();
                yArrow.Update();
                Thread.Sleep(0);
            }
        }

    }
}
