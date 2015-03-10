using Arduino.Selfienator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino.Selfienator.Models
{
    public class ArrowHelper : ViewModel
    {
        private double _angle;
        private double _width;
        private double _height;
        private double _widthView;
        private double _heightView;
        private double _margin;

        private double _goalAngle;
        private int _goalDirection;
        private DateTime _LastTime;
        private TimeSpan _deltaTime;
        private double _goalDelay;

        public ArrowHelper()
        {
            height = 100;
            width = 100;
            widthView = 150;
            heightView = 150;
            margin = (widthView - height) / 2;
            angle = 0;
        }

        public double angle
        {
            get { return _angle; }
            set
            {
                _angle = value;
                _angle %= 360;
                width = Math.Abs(Math.Cos(ConvertToRadians(_angle)) * 100) + Math.Abs(Math.Cos(ConvertToRadians(90 - (_angle))) * 100);
                height = Math.Abs(Math.Cos(ConvertToRadians(_angle)) * 100) + Math.Abs(Math.Cos(ConvertToRadians(90 - (_angle))) * 100);
                widthView = 150;
                heightView = 150;
                margin = (widthView - height) / 2;
                NotifyPropertyChanged();
            }
        }
        public double width
        {
            get { return _width; }
            set
            {
                _width = value;
                NotifyPropertyChanged();
            }
        }
        public double height
        {
            get { return _height; }
            set
            {
                _height = value;
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
        public double margin
        {
            get { return _margin; }
            set
            {
                _margin = value;
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
                _goalDelay = value;
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
        public double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        internal void Update()
        {
            if (_goalAngle != _angle)
            {
                if (_deltaTime.TotalMilliseconds > _goalDelay)
                {
                    if (_goalDirection == Direction.CLOCK_WISE)
                    {
                        angle += 1;
                    }
                    else if (_goalDirection == Direction.COUNTER_CLOCK_WISE)
                    {
                        angle -= 1;
                    }

                    _deltaTime = new TimeSpan();
                    _LastTime = DateTime.Now;
                    double a = _deltaTime.TotalMilliseconds; 
                }
                _deltaTime = (DateTime.Now - _LastTime);
            }
        }
    }
}
