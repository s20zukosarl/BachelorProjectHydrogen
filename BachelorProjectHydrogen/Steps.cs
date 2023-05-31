using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BachelorProjectHydrogen.StringTransformations;
using static BachelorProjectHydrogen.ComCommunication;
using static BachelorProjectHydrogen.ComPort;
using System.IO.Ports;

namespace BachelorProjectHydrogen
{
    public class Steps
    {

        public static byte[][] cyclogramOne = new byte[2][]
        {
          new byte [32]{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
              0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
          new byte [32]{ 1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,
              0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
        };

        public static byte[][] cyclogramTwo = new byte[7][]
            {
            new byte [32]{0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,
                          0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            new byte [32]{0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,
                          0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            new byte[32]{1,0,1,0,0,0,0,0,0,0,0,0,1,1,0,1,0,
                          0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            new byte[32]{0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,
                          0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            new byte[32]{1,1,0,0,0,0,0,0,0,0,1,0,0,1,0,1,0,
                          0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            new byte[32]{0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,
                         0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            new byte[32]{1,0,1,0,0,0,0,0,0,0,0,0,1,1,0,1,0,
                        0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 }
            };

        public static byte[][] cyclogramThree = new byte[2][]
            {
              new byte [32]{0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,
                  0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
              new byte [32]{0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,
                  0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 }
            };

        public static byte[][] cyclogramFour = new byte[5][]
            {
            new byte [32]{0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            new byte [32]{0,0,0,0,1,0,0,1,0,0,0,1,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            new byte [32]{0,0,1,0,0,0,0,0,0,0,0,0,1,1,0,0,1,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            new byte [32]{0,0,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            new byte [32]{0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,1,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 }
            };

        public static byte[][] cyclogramFive = new byte[2][]
        {
            new byte[32]{0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            new byte[32]{0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }

        };


        public static byte[][] cyclogramSix = new byte[1][]
        {
            new byte [32]{ 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }
        };


        public static List<string> CreateStepList(byte[][] gram)
        {
            string sendSte = "00000111";
            byte sendStep = Convert.ToByte(sendSte, 2);
            int whichRow = 0;
            var row = gram.GetLength(0);
            string sendSt = "";
            byte[] sendS = new byte[] { };
            List<string> allStep = new List<string>();

            for (whichRow = 0; whichRow < row; whichRow++)
            {
                byte[] cyclo = gram[whichRow];

                sendSt = sendSte + String.Join("", cyclo);
                allStep.Add(sendSt);
            }

            return allStep;

        }

        public static List<string> AllStepsListWithCycleAmount(int cycleAmount)
        {
            List<string> bigList = new List<string>() { };

            bigList.AddRange(CreateStepList(cyclogramOne));
            for (int i = 0; i < cycleAmount; i++)
            {
                bigList.AddRange(CreateStepList(cyclogramTwo));
            }
            bigList.AddRange(CreateStepList(cyclogramThree));
            for (int i = 0; i < cycleAmount; i++)
            {
                bigList.AddRange(CreateStepList(cyclogramFour));
            }
            bigList.AddRange(CreateStepList(cyclogramFive));
            bigList.AddRange(CreateStepList(cyclogramSix));

            return bigList;
        }
         
       
        public static List<string> AllStepsListWithTargetPressure(double targetPressure)
        {
            List<string> bigList = new List<string>() { };

            bigList.AddRange(CreateStepList(cyclogramOne));
            while (targetPressure / 2 > pressMeasurement / 2)
            {
                bigList.AddRange(CreateStepList(cyclogramTwo));
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(ReceiveData);
               // GetPressureMeasurement();
            }
            bigList.AddRange(CreateStepList(cyclogramThree));
            while (targetPressure > pressMeasurement)
            {
                bigList.AddRange(CreateStepList(cyclogramFour));
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(ReceiveData);
                //GetPressureMeasurement();
            }
            bigList.AddRange(CreateStepList(cyclogramFive));
            bigList.AddRange(CreateStepList(cyclogramSix));

            return bigList;
        }
    }
}
