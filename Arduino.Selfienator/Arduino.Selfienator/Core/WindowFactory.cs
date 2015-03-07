using Arduino.Selfienator.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Arduino.Selfienator.Core
{
    public class WindowFactory<T> : IWindowFactory, ISubscriber<EOpenWindow<T>>
        where T : Window, new()
    {
        private static WindowFactory<T> _instance;
        public void CreateNewWindow()
        {
            T window = new T();
            window.Show();
        }

        public void CreateNewDialogWindow()
        {
            T window = new T();
            window.ShowDialog(); 
        }

        public static WindowFactory<T> getInstance()
        {
            if (_instance == null)
            {
                _instance = new WindowFactory<T>();
            }
            return _instance;
        }

        public void OnEventHandler(EOpenWindow<T> e)
        {
            throw new NotImplementedException();
        }
    }
}
