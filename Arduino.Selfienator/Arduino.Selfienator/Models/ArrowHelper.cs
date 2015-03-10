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
        public double _angle;
        public double _width;
        public double _height;
        public double _widthView;
        public double _heightView;
        public double _margin;

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
        public double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
