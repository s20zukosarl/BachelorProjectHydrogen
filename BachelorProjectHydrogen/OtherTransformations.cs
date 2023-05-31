using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorProjectHydrogen
{
    public static class OtherTransformations
    {
        public static byte TimeNowToByte(string timeValue)
        {
            byte time = Convert.ToByte(timeValue);
            return time;
        }

        public static int fromSecondsToMs(int timeInterval)
        {
            timeInterval *= 1000;
            return timeInterval;
        }
    }
}
