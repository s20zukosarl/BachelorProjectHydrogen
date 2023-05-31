using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BachelorProjectHydrogen.ComPort;
using static BachelorProjectHydrogen.ClientSocket;
using static BachelorProjectHydrogen.StringTransformations;


namespace BachelorProjectHydrogen
{
    public static class ComCommunication
    {
        public static void WriteToComPort(byte[] command)
        {
            //int commandLength = Buffer.ByteLength(command);
            int commandLength = command.Length;
            _serialPort.Write(command, 0, commandLength);
        }


        public static void ReceiveData(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort _SerialPort = (SerialPort)sender;
            int _bytesToRead = _SerialPort.BytesToRead;
            byte[] recvData = new byte[_bytesToRead];
            _SerialPort.Read(recvData, 0, _bytesToRead);
            response = BitConverter.ToString(recvData, 0, _bytesToRead);
            //response = System.Text.Encoding.ASCII.GetString(recvData);
            Console.WriteLine("Input from COM: ");
            Console.WriteLine(response);
            SendDataToSocketServer(BinaryStringToDecString(fromController) + response);
            pressureMeasurement = GetPressureMeasurement();
            pressMeasurement = Convert.ToDouble(pressureMeasurement);
        }

       
    }
}
