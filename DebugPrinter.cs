namespace Monsterkampf_Simulator
{
    internal class DebugPrinter
    {
        struct ConsoleFormat
        {
            public ConsoleColor fgColor;
            public ConsoleColor bgColor;
            public string prefix;
        }

        internal enum DebugLevel
        {
            Info,
            Warning,
            Error
        }


        static readonly Dictionary<DebugLevel, ConsoleFormat> DebugFormat = new Dictionary<DebugLevel, ConsoleFormat>()
        {
            {DebugLevel.Info, new ConsoleFormat{fgColor=ConsoleColor.White, bgColor=ConsoleColor.Black, prefix="INFO" }},
            {DebugLevel.Warning, new ConsoleFormat{fgColor=ConsoleColor.Yellow, bgColor=ConsoleColor.Black, prefix="WARNING" }},
            {DebugLevel.Error, new ConsoleFormat{fgColor=ConsoleColor.Red, bgColor=ConsoleColor.Black, prefix="ERROR" }}
        };

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
