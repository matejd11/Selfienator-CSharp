using Arduino.Selfienator.Core;

namespace Arduino.Selfienator.ViewModels
{
    public class CommandPropertyHelper : ViewModel
    {
        private int _angle;
        private int _direction;
        private int _delay;

        public int angle
        {
            get { return _angle; }
            set
            {
                _angle = value;
                _angle %= 360;
                NotifyPropertyChanged();
            }
        }
        public int direction
        {
            get { return _direction; }
            set
            {
                _direction = value;
                NotifyPropertyChanged();
            }
        }
        public int delay
        {
            get { return _delay; }
            set
            {
                if (value > 10)
                {
                    _delay = value;
                }
                else
                {
                    _delay = 10;
                }
                NotifyPropertyChanged();
            }
        }
    }
}
