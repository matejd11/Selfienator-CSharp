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
        public double angleX { get; set; }
        public double widthX { get; set; }
        public double heightX { get; set; }
        public double widthXView { get; set; }
        public double heightXView { get; set; }
        public double marginX { get; set; }

        public MainWindowViewModel()
            : this(0)
        {

        }

        public MainWindowViewModel(int hashCode)
        {
            _windowHashCode = hashCode;
            directions = new int[] { 0, 1 };
            heightX = 100;
            widthX = 100;
            widthXView = 150;
            heightXView = 150;
            marginX = (widthXView - heightX) / 2;
            angleX = 0;

        }
        public int angle { get; set; }
        public int direction { get; set; }
        public int delay { get; set; }
        public int[] directions { get; set; }


        public ICommand zapComm { get { return new ActionCommand(zapni); } }
        public ICommand vypComm { get { return new ActionCommand(vypni); } }
        public ICommand sendComm { get { return new ActionCommand(send); } }
        public ICommand debugOnComm { get { return new ActionCommand(startDebug); } }
        public ICommand leftComm { get { return new ActionCommand(left); } }
        public ICommand rightComm { get { return new ActionCommand(right); } }

        private void right(object obj)
        {
            angleX += 5;
            widthX = Math.Abs(Math.Cos(ConvertToRadians(angleX)) * 100) + Math.Abs(Math.Cos(ConvertToRadians(90 - (angleX))) * 100);
            heightX = Math.Abs(Math.Cos(ConvertToRadians(angleX)) * 100) + Math.Abs(Math.Cos(ConvertToRadians(90 - (angleX))) * 100); 
            widthXView = 150;
            heightXView = 150;
            marginX = (widthXView - heightX) / 2;
            NotifyPropertyChanged("angleX");
            NotifyPropertyChanged("widthX");
            NotifyPropertyChanged("heightX");
            NotifyPropertyChanged("widthXView");
            NotifyPropertyChanged("heightXView");
            NotifyPropertyChanged("marginX");
        }
        private void left(object obj)
        {
            angleX -= 5;
            widthX = Math.Abs(Math.Cos(ConvertToRadians(angleX)) * 100) + Math.Abs(Math.Cos(ConvertToRadians(90 - (angleX))) * 100);
            heightX = Math.Abs(Math.Cos(ConvertToRadians(angleX)) * 100) + Math.Abs(Math.Cos(ConvertToRadians(90 - (angleX))) * 100); 
            widthXView = 150;
            heightXView = 150;
            marginX = (widthXView - heightX) / 2;
            NotifyPropertyChanged("angleX");
            NotifyPropertyChanged("widthX");
            NotifyPropertyChanged("heightX");
            NotifyPropertyChanged("widthXView");
            NotifyPropertyChanged("heightXView");
            NotifyPropertyChanged("marginX");
        }
        public double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

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
