using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BachelorProjectHydrogen.Steps;
using static BachelorProjectHydrogen.ComPort;
using static BachelorProjectHydrogen.ClientSocket;
using static BachelorProjectHydrogen.ComCommunication;
using static BachelorProjectHydrogen.OtherTransformations;
using static BachelorProjectHydrogen.StringTransformations;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections;

namespace BachelorProjectHydrogen
{
    public static class ComCommands
    {
        public static TimeSpan TimeOfDay { get; }
        static string hours = DateTime.Now.ToString("HH");
        static string minutes = DateTime.Now.ToString("mm");
        static string seconds = DateTime.Now.ToString("ss");
        public static int timeInterval;
        public static bool commandSent = false;
        




        // laika sinhronizacijas komanda

        public static byte[] TimeSyncComCommand()
        {
            byte hoursByte = TimeNowToByte(hours);
            byte minutesByte = TimeNowToByte(minutes);
            byte secondsByte = TimeNowToByte(seconds);
            string timeSyncCommand = "00010000";
            byte timeSy = Convert.ToByte(timeSyncCommand, 2);
            string timeSync = timeSy.ToString() + hoursByte + minutesByte + secondsByte;
            Console.WriteLine("Output to COM: ");
            Console.WriteLine(timeSync);
            byte[] timeArr = new byte[] { timeSy, hoursByte, minutesByte,
            secondsByte};
            
            return timeArr;

        }

        // inicializacijas komanda

        public static void InitializationCommand()
        {
            int komponentaNr = 0;
            string add = "00000101";
            byte addInit = Convert.ToByte(add, 2);
            char[] ports = { 'A', 'B', 'C', 'D', 'E', 'F' };
            int pins = 0;
            
            List<byte> nineTen = new List<byte>() { addInit, Convert.ToByte(9),
                Convert.ToByte(ports[0]), Convert.ToByte(14),
            addInit, Convert.ToByte(10), Convert.ToByte(ports[0]),
                Convert.ToByte(15)};

            List<byte> pump = new List<byte>() { addInit, Convert.ToByte(0),
                Convert.ToByte(ports[5]), Convert.ToByte(0) };
           

            // varstiem

            List<byte> bytesFirst = new List<byte>();

            for (int i = 0; i < 8; i++)
            {
                komponentaNr = i;
                pins = i;
               
                bytesFirst.Add(addInit);
                bytesFirst.Add(Convert.ToByte(komponentaNr));
                bytesFirst.Add(Convert.ToByte(ports[0]));
                bytesFirst.Add(Convert.ToByte(pins));
            }

            List<byte> bytesSecond = new List<byte>();

            for (int i = 11; i < 18; i++)
            {
                komponentaNr = i;
                pins = (11 - i) * (-1);
                bytesSecond.Add(addInit);
                bytesSecond.Add(Convert.ToByte(komponentaNr));
                bytesSecond.Add(Convert.ToByte(ports[1]));
                bytesSecond.Add(Convert.ToByte(pins));

            }
            // manometriem

            List<byte> bytesThird = new List<byte>();

            for (int i = 0; i < 3; i++)
            {

                komponentaNr = i;
                pins = i;
               
                bytesThird.Add(addInit);
                bytesThird.Add(Convert.ToByte(komponentaNr));
                bytesThird.Add(Convert.ToByte(ports[2]));
                bytesThird.Add(Convert.ToByte(pins));
            }

            List<byte> bytesFourth = new List<byte>();

            for (int i = 0; i < 3; i++)
            {

                komponentaNr = i;
                pins = i;
                
                bytesFourth.Add(addInit);
                bytesFourth.Add(Convert.ToByte(komponentaNr));
                bytesFourth.Add(Convert.ToByte(ports[3]));
                bytesFourth.Add(Convert.ToByte(pins));
            }

            List<byte> bytesFifth = new List<byte>();

            for (int i = 0; i < 3; i++)
            {

                komponentaNr = i;
                pins = i;

                bytesFifth.Add(addInit);
                bytesFifth.Add(Convert.ToByte(komponentaNr));
                bytesFifth.Add(Convert.ToByte(ports[4]));
                bytesFifth.Add(Convert.ToByte(pins));

            }

            List<byte> allComponents = new List<byte>();
            allComponents.AddRange(bytesFirst);
            allComponents.AddRange(nineTen);
            allComponents.AddRange(bytesSecond);
            allComponents.AddRange(bytesThird);
            allComponents.AddRange(bytesFourth);
            allComponents.AddRange(bytesFifth);
            allComponents.AddRange(pump);


            byte[] allComponentsArray = allComponents.ToArray();

            List<List<byte>> partitions = allComponents.separation(4);

            foreach (List<byte> partition in partitions)
            {
                WriteToComPort(partition.ToArray());
                
            }
        }


        // uzsaksanas komanda

        public static byte[] InitializationFinishedCom()
        {
            string InitFinishString = "00001010";
            byte InitFinished = Convert.ToByte(InitFinishString, 2);
            string finished = InitFinished.ToString();
            Console.WriteLine("Output to COM: ");
            Console.WriteLine(finished);
            byte[] finishedByteArray = new byte[] { InitFinished };
            return finishedByteArray;
        }


        // visu reseto 

        public static byte[] ResetAll()
        {
            string resetAll = "00001111";
            byte resetA = Convert.ToByte(resetAll, 2);
            string resetB = resetA.ToString();
            Console.WriteLine("Output to COM: ");
            Console.WriteLine(resetB);
            byte[] resetAllByteArray = new byte[] { resetA }; 
            return resetAllByteArray;
        }

        // reseto plusmas sensoru uz 0

        public static byte[] ResetFlowMeter(byte flowM)
        {

            string resetFlowMeter = "00000011";
            byte resetFM = Convert.ToByte(resetFlowMeter, 2);
            string resetFlowM = resetFM.ToString() + flowM.ToString();
            Console.WriteLine("Output to COM: ");
            Console.WriteLine(resetFlowM);
            byte[] resetFlM = new byte[] {resetFM, flowM};
            return resetFlM;
        }

        
       // command to send compression steps to com port and socket server

        public static async void SendAllCommandsToComPortAndServer(int timeInterval)
        {
            List<string> allSteps = new List<string>();
            allSteps = AllStepsListWithCycleAmount(cycleAmount);
            foreach(string s in allSteps)
            {
                WriteToComPort(ConvertBinaryString(s));
                //SendDataToSocketServer(binaryStringToDecString(fromClientSocket) +s);
                await Task.Delay(timeInterval);
            }
        }

        public static async void SendAllCommandsToComPortAndServerWithTargetPres(int timeInterval)
        {
            List<string> allSteps = new List<string>();
            allSteps = AllStepsListWithTargetPressure(targetPressure);
            foreach (string s in allSteps)
            {
                WriteToComPort(ConvertBinaryString(s));
                //SendDataToSocketServer(binaryStringToDecString(fromClientSocket)+s);
                await Task.Delay(timeInterval);
            }
        }

    }
}

