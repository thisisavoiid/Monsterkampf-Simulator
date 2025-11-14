namespace Monsterkampf_Simulator
{
    public class LogicLib
    {

        /// <summary>
        /// Checks whether a value is an integer.
        /// </summary>
        /// <param name="value">
        /// Value to check.
        /// </param>
        /// <returns>
        /// True if the value is numeral, 
        /// False if it isn't.
        /// </returns>
        public static bool IsNumeral(string value)
        {
            try
            {
                int parsedNumber = int.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks whether a value is inside a certain range.
        /// </summary>
        /// <param name="value">
        /// Value to check.
        /// </param>
        /// <param name="min">
        /// First index of the range.
        /// </param>
        /// <param name="max">
        /// Last index of the range.
        /// </param>
        /// <returns>
        /// True if the value is inside the specified range, 
        /// False if it isn't.
        /// </returns>
        public static bool IsInRange(int value, int min, int max)
        {
            return min <= value && value <= max;
        }
    }
}
