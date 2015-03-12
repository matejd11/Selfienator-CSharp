using Arduino.Selfienator.Core;
using Arduino.Selfienator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino.Selfienator.ViewModels
{
    public class ArrowUserControlVM : ViewModel
    {
        private ArrowHelper _arrow;

        public ArrowUserControlVM()
        {
            arrow = new ArrowHelper();
        }

        public ArrowHelper arrow
        {
            get { return _arrow; }
            set
            {
                _arrow = value;
                NotifyPropertyChanged();
            }
        }

        internal void Update()
        {
            arrow.Update();
        }

        internal void startExecuting(int angle, int direction, int delay)
        {
            arrow.startExecuting(angle, direction, delay);
        }

        internal void AddAngle(double angle)
        {
            arrow.angle += angle;
        }

        internal void SubAngle(int angle)
        {
            arrow.angle -= angle;
        }
    }
}
