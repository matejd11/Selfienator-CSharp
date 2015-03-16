using Arduino.Selfienator.Core;
using System;

namespace Arduino.Selfienator.Models
{
    public class ArrowHelper : ViewModel
    {
        private double _angle;
        private double _widthView;
        private double _heightView;

        private double _goalAngle;
        private int _goalDirection;
        private DateTime _LastTime;
        private TimeSpan _deltaTime;
        private double _goalDelay;

        private bool _isExecuting;

        public ArrowHelper()
        {
            widthView = 150;
            heightView = 150;
            angle = 0;
            _isExecuting = false;
        }

        public double angle
        {
            get { return _angle; }
            set
            {
                _angle = value;
                if (_angle < 0)
                {
                    _angle = 360 + _angle;
                }
                _angle %= 360;
                NotifyPropertyChanged();
            }
        }
        public double widthView
        {
            get { return _widthView; }
            set
            {
                _widthView = value;
                NotifyPropertyChanged();
            }
        }
        public double heightView
        {
            get { return _heightView; }
            set
            {
                _heightView = value;
                NotifyPropertyChanged();
            }
        }
        public double goalAngle
        {
            get { return _goalAngle; }
            set
            {
                _goalAngle = value;
                NotifyPropertyChanged();
            }
        }
        public double goalDelay
        {
            get { return _goalDelay; }
            set
            {
                if (value > 10)
                {
                    _goalDelay = value;
                }
                else
                {
                    _goalDelay = 10;
                }
                 NotifyPropertyChanged();
            }
        }
        public int goalDirection
        {
            get { return _goalDirection; }
            set
            {
                _goalDirection = value;
                NotifyPropertyChanged();
            }
        }

        public void startExecuting(double angle, int direction, double delay)
        {
            this.goalAngle = angle;
            this.goalDirection = direction;
            this.goalDelay = delay;
            _isExecuting = true;
        }
        public void stopExecuting()
        {
            _isExecuting = false;
        }
        public double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        internal void Update()
        {
            if (_isExecuting)
            {
                if (_goalAngle != angle)
                {
                    _deltaTime = (DateTime.Now - _LastTime);
                    if (_deltaTime.TotalMilliseconds > _goalDelay)
                    {
                        if (_goalDirection == Direction.CLOCK_WISE)
                        {
                            angle += 1;
                            //Console.WriteLine(_goalAngle - _deltaTime.Milliseconds);
                        }
                        else if (_goalDirection == Direction.COUNTER_CLOCK_WISE)
                        {
                            angle -= 1;
                            //Console.WriteLine(_goalAngle - _deltaTime.Milliseconds);
                        }
                        _LastTime = DateTime.Now;
                    }
                }
                else if (_goalAngle == angle)
                {
                    _isExecuting = false;
                }

            }
        }
    }
}
