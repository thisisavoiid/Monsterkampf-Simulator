namespace Monsterkampf_Simulator
{
    internal class DebugPrinter
    {

        /// <summary>
        /// Holds the values about how to format a debug message.
        /// </summary>
        struct ConsoleFormat
        {
            public ConsoleColor fgColor;
            public ConsoleColor bgColor;
            public string prefix;
        }

        /// <summary>
        /// Contains the different debug levels.
        /// </summary>
        internal enum DebugLevel
        {
            Info,
            Warning,
            Error
        }

        /// <summary>
        /// Contains the combined debug message formats including prefixes.
        /// </summary>
        static readonly Dictionary<DebugLevel, ConsoleFormat> DebugFormat = new Dictionary<DebugLevel, ConsoleFormat>()
        {
            {DebugLevel.Info, new ConsoleFormat{fgColor=ConsoleColor.White, bgColor=ConsoleColor.Black, prefix="INFO" }},
            {DebugLevel.Warning, new ConsoleFormat{fgColor=ConsoleColor.Yellow, bgColor=ConsoleColor.Black, prefix="WARNING" }},
            {DebugLevel.Error, new ConsoleFormat{fgColor=ConsoleColor.Red, bgColor=ConsoleColor.Black, prefix="ERROR" }}
        };

        /// <summary>
        /// Prints a debug message.
        /// </summary>
        /// <param name="message">
        /// Message to print.
        /// </param>
        /// <param name="level">
        /// Debug level the message should be printed as.
        /// </param>
        /// <param name="deleteAfter">
        /// Optional amount of time after which the debug message is being deleted.
        /// </param>
        public static void Print(string message, DebugLevel level, int deleteAfter=0)
        {
            Console.ForegroundColor = DebugFormat[level].fgColor;
            Console.BackgroundColor = DebugFormat[level].bgColor;
            Console.WriteLine($"[{DebugFormat[level].prefix}] {message}");
            Console.ResetColor();
            
            if (deleteAfter > 0)
            {
                Thread.Sleep(deleteAfter);
                GUIHandler.ClearConsole(true);
            }
        }

    }
}
