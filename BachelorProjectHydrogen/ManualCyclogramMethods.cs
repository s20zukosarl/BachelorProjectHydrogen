using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BachelorProjectHydrogen.ComCommunication;
using static BachelorProjectHydrogen.StringTransformations;
using static BachelorProjectHydrogen.OtherTransformations;
using static BachelorProjectHydrogen.ClientSocket;
using static System.Windows.Forms.LinkLabel;

namespace BachelorProjectHydrogen
{
    public static class ManualCyclogramMethods
    {
        

        public static void SaveToLogFile(List<string> saveList, string iteration)
        {

            
            string fileName = "cyclogram";
            string extension = ".txt";

            string[] paths = { @"C:\Users", "john5", "Desktop", "LoggedCyclograms", fileName + iteration + extension };
            string fullPath = Path.Combine(paths);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(fullPath)))
            {
                foreach (string line in saveList)
                    outputFile.WriteLine(line.Trim());
            }

        }

        public static List<string> SeperateTimeString(string timeString)
        {
            List<string> times = new List<string>();

            for (int i = 0; i < timeString.Length; i++)
            {
                times.Add(timeString.Substring(i, 1));

            }
            return times;
        }

        public static async void TimeList(List<string> list, List<string> listy)
        {
            for (int i = 0; i < listy.Count; i++)
            {
                WriteToComPort(ConvertBinaryString(listy[i]));
                //SendDataToSocketServer(BinaryStringToDecString(fromClientSocket)+listy[i]);
                int time = Convert.ToInt32(list[i]);

                await Task.Delay(fromSecondsToMs(time));
            }

        }
    }
}
