using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino.Selfienator.Core.Events.Debug
{
    public class EDebugMessage
    {
        public bool isIncoming { get; set; }
        public string message { get; set; }
    }
}
