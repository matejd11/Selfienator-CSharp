using Arduino.Selfienator.Core;
using Arduino.Selfienator.Core.Events.Debug;
using System.Windows.Input;

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

        public ICommand clearComm
        {
            get
            {
                return new ActionCommand(p =>
                {
                    incomingMessage = "";
                    outgoingMessage = "";
                });
            }
        }

        public DebugViewModel(int windowsHashCode)
        {
            EventAggregator.Instance.Subsribe(this);
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
                incomingMessage += e.message.Trim() + "\n";
                outgoingMessage += "<---\n";
            }
            if (e.isIncoming == false)
            {
                outgoingMessage += e.message.Trim() + "\n";
                incomingMessage += "--->\n";
            }
        }
    }
}
