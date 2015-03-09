using Arduino.Selfienator.Core;
using Arduino.Selfienator.Core.Events.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arduino.Selfienator.Views
{
    public class DebugViewModel : ViewModel, ISubscriber<EDebugMessage>
    {

        private string _incomingMessage;
        private string _outgoingMessage;

        public DebugViewModel()
            : this(0)
        {

        }
        public DebugViewModel(int windowsHashCode)
        {
            EventAggregator.getInstance().SubsribeEvent(this);
            _windowHashCode = windowsHashCode;
        }

        public string incomingMessage
        {
            get { return _incomingMessage; }
            set
            {
                _incomingMessage = value;
                NotifyPropertyChanged();
            }
        }

        public string outgoingMessage
        {
            get { return _outgoingMessage; }
            set
            {
                _outgoingMessage = value;
                NotifyPropertyChanged();
            }
        }

        public void OnEventHandler(EDebugMessage e)
        {
            if (e.isIncoming == true)
            {
                incomingMessage += "\n" +e.message;
                outgoingMessage += "\n";
            }
            if (e.isIncoming == false)
            {
                outgoingMessage += "\n" + e.message;
                incomingMessage += "\n";
            }
        }
    }
}
