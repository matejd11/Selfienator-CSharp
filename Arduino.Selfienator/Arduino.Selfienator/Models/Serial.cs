using Arduino.Selfienator.Core;
using System;
using System.IO.Ports;
using System.Threading;

namespace Arduino.Selfienator.Models
{
    public class Serial
    {
        private static Serial _instance;
        private static ICommands _commands;
        public static Serial GetInstance(string port)
        {

            if (_instance == null || _commands == null)
            {
                _instance = new Serial(port);
                _commands = new Commands();
            }
            return _instance;
        }

        public static Serial GetInstance()
        {
            if (_instance == null || _commands == null)
            {
                GetInstance("COM3");
            }
            return _instance;
        }

        public static ICommands getCommands()
        {
            return _commands;
        }

        private SerialPort serial;
        public Serial(string port)
        {
            serial = new SerialPort(port, 9600);
            serial.Open();
            Thread scanThread = new Thread(p =>
            {
                while (true)
                {
                    //TODO: create reading from COM port
                }
            });
            scanThread.IsBackground = true;
            scanThread.Start();
        }

        public void send(String message)
        {
            serial.Write(message);
        }
    }
}