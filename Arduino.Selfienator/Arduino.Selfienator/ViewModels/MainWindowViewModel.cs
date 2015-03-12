using Arduino.Selfienator.Core;
using Arduino.Selfienator.Models;
using Arduino.Selfienator.Views;
using System;
using System.Threading;
using System.Windows.Input;

namespace Arduino.Selfienator.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private ArrowUserControlVM _xArrow;
        private ArrowUserControlVM _yArrow;
        private CommandPropertyHelper _x;
        private CommandPropertyHelper _y;

        private Thread TMoveXArrow;
        private Thread TMoveYArrow;

        public MainWindowViewModel()
            : this(0)
        {

        }

        public MainWindowViewModel(int hashCode)
        {
            x = new CommandPropertyHelper();
            y = new CommandPropertyHelper();
            xArrow = new ArrowUserControlVM();
            yArrow = new ArrowUserControlVM() { arrow = new ArrowHelper() { angle = 180 } };
            _windowHashCode = hashCode;
            directions = new int[] { 0, 1 };
            TMoveXArrow = new Thread(MoveXArrow);
            TMoveXArrow.IsBackground = true;
            TMoveXArrow.Start();
            TMoveYArrow = new Thread(MoveYArrow);
            TMoveYArrow.IsBackground = true;
            TMoveYArrow.Start();
        }
        public int[] directions { get; set; }

        public ArrowUserControlVM xArrow
        {
            get { return _xArrow; }
            set
            {
                _xArrow = value;
                NotifyPropertyChanged();
            }
        }
        public ArrowUserControlVM yArrow
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

        public ICommand sendComm { get { return new ActionCommand(send); } }
        public ICommand debugOnComm { get { return new ActionCommand(startDebug); } }
        public ICommand leftComm { get { return new ActionCommand(left); } }
        public ICommand rightComm { get { return new ActionCommand(right); } }
        public ICommand FocusShotComm { get { return new ActionCommand(FocusShot); } }

        private void FocusShot(object obj)
        {
            if ((string)obj == "FS")
            {
                Serial.GetInstance().send(Serial.getCommands().focusAndShot());
            }
            else if ((string)obj == "F")
            {
                Serial.GetInstance().send(Serial.getCommands().focus());
            }
            else if ((string)obj == "S")
            {
                Serial.GetInstance().send(Serial.getCommands().shot());
            }
        }

        private void right(object obj)
        {
            if ((string)obj == "X")
            {
                _xArrow.AddAngle(5);
            }
            else if ((string)obj == "Y")
            {
                _yArrow.AddAngle(5);
            }
        }
        private void left(object obj)
        {
            if ((string)obj == "X")
            {
                _xArrow.SubAngle(5);
            }
            else if ((string)obj == "Y")
            {
                _yArrow.SubAngle(5);
            }
        }

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

        private void MoveXArrow(object obj)
        {
            while (true)
            {
                xArrow.arrow.Update();
                //Thread.Sleep(0);
            }
        }

        private void MoveYArrow(object obj)
        {
            while (true)
            {
                yArrow.arrow.Update();
                //Thread.Sleep(0);
            }
        }
    }
}
