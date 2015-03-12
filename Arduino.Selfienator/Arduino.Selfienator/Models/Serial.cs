using Arduino.Selfienator.Core;
using Arduino.Selfienator.Core.Events.Debug;
using System;
using System.IO.Ports;
using System.Threading;

namespace Arduino.Selfienator.Models
{
    public class Serial : IDisposable
    {
        private static Serial _instance;
        private static ICommands _commands;
        private string inData;
        public static Serial GetInstance(string port, int bitRate)
        {

            if (_instance == null || _commands == null)
            {
                _instance = new Serial(port, bitRate);
                _commands = new Commands();
            }
            return _instance;
        }

        public static Serial GetInstance()
        {
            if (_instance == null || _commands == null)
            {
                return GetInstance("COM3", 9600);
            }
            return _instance;
        }

        public static ICommands getCommands()
        {
            return _commands;
        }

        private SerialPort serial;
        public Serial(string port, int bitRate)
        {
            serial = new SerialPort(port, bitRate);
            serial.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            serial.Open();
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            inData += serial.ReadExisting();
            if (inData.Contains(";"))
            {
                EventAggregator.getInstance().PublishEvent<EDebugMessage>(new EDebugMessage() { isIncoming = true, message = inData});
                inData = "";
            }
        }

        public void send(string message)
        {
            serial.Write(message);
            EventAggregator.getInstance().PublishEvent<EDebugMessage>(new EDebugMessage() { isIncoming = false, message = message });
        }

        public void Dispose()
        {
            if (serial != null)
            {
                serial.Dispose();
            }
        }
    }
}