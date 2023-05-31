using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BachelorProjectHydrogen.ManualCyclograms;
using static BachelorProjectHydrogen.ComPort;
using static BachelorProjectHydrogen.ComCommunication;


namespace BachelorProjectHydrogen
{
    public static class StringTransformations
    {

        public static int rows;
        public static int cols;
        public static double targetPressure;
        public static string pressureMeasurement;
        public static int cycleAmount;
        public static double pressMeasurement;
        public static string fromClientSocket = "00001000";
        public static string fromController = "00001001";

        public static string BinaryStringToDecString(string from)
        {
            Convert.ToByte(from);
            return from.ToString();
        }

        public static string GetPressureMeasurement()
        {

            if (response.Length == comInputExample.Length)
            {
                pressureMeasurement = response.Substring(13, 6);
            }

            return pressureMeasurement;
        }

        public static List<List<T>> separation<T>(this List<T> values, int sizeToDivide)
        {
            return values.Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / sizeToDivide)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        public static byte[] ConvertBinaryString(string together)
        {
            byte[] send = new byte[] { };
            var bytesAsStrings = together.Select((c, i) => new { Char = c, Index = i })
                .GroupBy(x => x.Index / 8)
                .Select(g => new string(g.Select(x => x.Char).ToArray()));
            send = bytesAsStrings.Select(s => Convert.ToByte(s, 2)).ToArray();

            return send;
        }
     

        public static List<string> SeperateString(string commandString)
        {

            string sendSte = "00000111";

            List<string> commands = new List<string>();

            for (int i = 0; i < commandString.Length; i += cols)
            {
                commands.Add(sendSte + commandString.Substring(i, cols));

            }

            return commands;
        }


    }
}
