using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using static BachelorProjectHydrogen.ClientSocket;

namespace BachelorProjectHydrogen
{
    public static class ComPort
    {
        public static SerialPort _serialPort = new SerialPort();
       
        public static string response;
        // 29.132 pressure
        public static string comInputExample = "17:54:23.359,29.132,-44.644,0.000,0,0";
        
        public static bool connectedToComPort = false;


        public static void ConnectToComPort(SerialPort _serialPort, string portName)
        {
            _serialPort.PortName = portName;
            _serialPort.Handshake = Handshake.None;
            _serialPort.Open();
            if(_serialPort.IsOpen)
            {
                connectedToComPort = true;
            }            
        }

  

    }
}
