using Arduino.Selfienator.Core;
using Arduino.Selfienator.Core.Events;
using Arduino.Selfienator.Core.Events.Debug;
using Arduino.Selfienator.Models;
using Arduino.Selfienator.Views;
using System.Threading;
using System.Windows.Input;

namespace Arduino.Selfienator.ViewModels
{
    public class MainWindowViewModel : ViewModel, ISubscriber<ECancelDebug>
    {
        private ArrowUserControlVM _xArrow;
        private ArrowUserControlVM _yArrow;
        private CommandPropertyHelper _x;
        private CommandPropertyHelper _y;

        private Thread TMoveXArrow;
        private Thread TMoveYArrow;

        private bool _debugOpend;

        public MainWindowViewModel()
            : this(0)
        {

        }

        public MainWindowViewModel(int hashCode)
        {
            EventAggregator.getInstance().Subsribe(this);
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

        public bool debugOpend
        {
            get { return _debugOpend; }
            set
            {
                _debugOpend = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand sendComm { get { return new ActionCommand(send); } }
        public ICommand debugOnComm { get { return new ActionCommand(startDebug); } }
        public ICommand leftComm { get { return new ActionCommand(left); } }
        public ICommand rightComm { get { return new ActionCommand(right); } }
        public ICommand FocusShotComm { get { return new ActionCommand(FocusShot); } }
        public ICommand closeComm { get { return new ActionCommand(close); } }
        public ICommand AutomatComm { get { return new ActionCommand(Automat); } }


        private int _direction;

        public int direction
        {
            get { return _direction; }
            set
            {
                _direction = value;
                NotifyPropertyChanged();
            }
        }


        private double _angle;

        public double angle
        {
            get { return _angle; }
            set
            {
                _angle = value;
                NotifyPropertyChanged();
            }
        }

        private double _delay;

        public double delay
        {
            get { return _delay; }
            set
            {
                _delay = value;
                NotifyPropertyChanged();
            }
        }


        private void Automat(object obj)
        {/*
            Thread a = new Thread(() =>
            {
                var y = new int[] { 60, 90, 120 };
                double anglee = 0;
                for (int i = 0; i < 3; i++)
                {
                    var yAngle = y[i];

                    Serial.Instance.send(Serial.Commands.motorY(yAngle, 0, 50));
                    Thread.Sleep(50 * 35);
                    yArrow.startExecuting((int)yAngle, 0, 50);

                    var deltaangle = 30;

                    Serial.Instance.send(Serial.Commands.focusAndShot());
                    Thread.Sleep(1500);

                    var commands = new Commands(); ;
                    while (true)
                    {
                        if (i == 0 || i == 2)
                            anglee += deltaangle;
                        else if (i == 1)
                            anglee -= deltaangle;

                        anglee %= 360;
                        if (anglee < 0)
                        {
                            anglee = 360 + anglee;
                        }

                        if (i == 0 || i == 2)
                        {
                            Serial.Instance.send(Serial.Commands.motorX(anglee, Direction.CLOCK_WISE, 80));
                            xArrow.startExecuting((int)anglee, Direction.CLOCK_WISE, 80);
                        }
                        else if (i == 1)
                        {
                            Serial.Instance.send(Serial.Commands.motorX(anglee, Direction.COUNTER_CLOCK_WISE, 80));
                            xArrow.startExecuting((int)anglee, Direction.COUNTER_CLOCK_WISE, 80);
                        }

                        Thread.Sleep(80 * deltaangle + 500);
                        Serial.Instance.send(Serial.Commands.focusAndShot());
                        Thread.Sleep(1500);
                        if (i == 0 || i == 2)
                        {
                            if (anglee >= angle)
                            {
                                break;
                            }
                        }
                        if (i == 1)
                        {
                            if (anglee <= 0)
                            {
                                break;
                            }
                        }
                    }
                }
            });
            a.Start(); */
            
            Thread b = new Thread(() =>
            {
                var yas = new int[] { 60, 90, 120 };
                double anglee = 0;
                for (int i = 0; i < 3; i++)
                {

                    y.angle = yas[i];
                    Thread.Sleep(50 * 35);
                    var deltaangle = 30;
                    sendComm.Execute("Y");
                    FocusShotComm.Execute("FS");
                    Thread.Sleep(1500);

                    var commands = new Commands(); ;
                    while (true)
                    {
                        if (i == 0 || i == 2)
                            anglee += deltaangle;
                        else if (i == 1)
                            anglee -= deltaangle;

                        anglee %= 360;
                        if (anglee < 0)
                        {
                            anglee = 360 + anglee;
                        }

                        x.angle = (int)anglee;
                        if (i == 0 || i == 2)
                        {
                            x.direction = Direction.CLOCK_WISE;
                        }
                        else if (i == 1)
                        {
                            x.direction = Direction.COUNTER_CLOCK_WISE;
                        }

                        sendComm.Execute("X");

                        Thread.Sleep(80 * deltaangle + 500);
                        FocusShotComm.Execute("FS");
                        Thread.Sleep(1500);
                        if (i == 0 || i == 2)
                        {
                            if (anglee >= angle)
                            {
                                break;
                            }
                        }
                        if (i == 1)
                        {
                            if (anglee <= 0)
                            {
                                break;
                            }
                        }
                    }
                }
            });
            b.Start();
        }

        private void close(object obj)
        {
            EventAggregator.getInstance().PublishEvent<ECloseWindow>(new ECloseWindow() { hashCode = _windowHashCode });
        }

        private void FocusShot(object obj)
        {
            if ((string)obj == "FS")
            {
                Serial.Instance.send(Serial.Commands.focusAndShot());
            }
            else if ((string)obj == "F")
            {
                Serial.Instance.send(Serial.Commands.focus());
            }
            else if ((string)obj == "S")
            {
                Serial.Instance.send(Serial.Commands.shot());
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
            if (_debugOpend)
            {
                WindowFactory<DebugWindow>.getInstance().CreateNewWindow();
            }
            else
            {
                //EventAggregator.getInstance().PublishEvent<ESetFocus>(new ESetFocus() { targetWindow = "DebugWindow" }); 
                //debugOpend = true;
                EventAggregator.getInstance().PublishEvent<ECloseWindow>(new ECloseWindow() { targetWindow = "DebugWindow" });
            }
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

                Serial.Instance.send(Serial.Commands.motor(angles, directions, delays, names));

                xArrow.startExecuting(x.angle, x.direction, x.delay);

                yArrow.startExecuting(y.angle, y.direction, y.delay);
            }

            if ((string)obj == "X")
            {
                var commands = new Commands();

                x.angle %= 360;

                Serial.Instance.send(Serial.Commands.motorX(x.angle, x.direction, x.delay));

                xArrow.startExecuting(x.angle, x.direction, x.delay);
            }

            if ((string)obj == "Y")
            {
                var commands = new Commands();

                y.angle %= 360;

                Serial.Instance.send(Serial.Commands.motorY(y.angle, y.direction, y.delay));

                yArrow.startExecuting(y.angle, y.direction, y.delay);
            }

            //ENDDEBUG
        }

        private void MoveXArrow(object obj)
        {
            while (true)
            {
                xArrow.arrow.Update();
                Thread.Yield();
                //Thread.Sleep(1);
            }
        }

        private void MoveYArrow(object obj)
        {
            while (true)
            {
                yArrow.arrow.Update();
                Thread.Yield();
                //Thread.Sleep(1);
            }
        }

        public void OnEventHandler(ECancelDebug e)
        {
            debugOpend = false;
        }
    }
}
