namespace Monsterkampf_Simulator
{
    public class LogicLib
    {
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

        public static bool IsInRange(int value, int min, int max)
        {
            return min <= value && value <= max;
        }
    }
}
