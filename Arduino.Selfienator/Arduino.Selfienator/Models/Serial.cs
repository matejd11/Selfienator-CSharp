using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Arduino.Selfienator.Models
{
    public class Serial
    {
        private static Serial instance;

        public static Serial GetInstance(string port)
        {

            if (instance == null)
            {
                instance = new Serial(port);
            }
            return instance;
        }

        public static Serial GetInstance()
        {
            if (instance == null)
            {
                return GetInstance("COM3");
            }
            return instance;
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