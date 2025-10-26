using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf_Simulator
{
    public class LogicLib
    {
        // This method checks if a given string can be parsed as an integer
        public static bool IsNumeral(string value)
        {
            try
            {
                // Attempt to parse the string into an integer
                int parsedNumber = int.Parse(value);

                // If parsing succeeds, return true
                return true;
            }
            catch
            {
                // If parsing fails (exception is thrown), return false
                return false;
            }
        }

        public static bool IsInRange(int value, int min, int max)
        {
            return min <= value && value <= max;
        }
    }
}
