using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf_Simulator
{
    internal class DebugPrinter
    {
        // List of available debug levels
        internal enum DebugLevel
        {
            Info,
            Warning,
            Error
        }

        // Struct that contains color and prefix
        struct ConsoleFormat
        {
            public ConsoleColor fgColor;
            public ConsoleColor bgColor;
            public string prefix;
        }

        // Contains all the ConsoleFormat structs in order to format the console output properly
        static readonly Dictionary<DebugLevel, ConsoleFormat> DebugFormat = new Dictionary<DebugLevel, ConsoleFormat>()
        {
            {DebugLevel.Info, new ConsoleFormat{fgColor=ConsoleColor.White, bgColor=ConsoleColor.Black, prefix="INFO" }},
            {DebugLevel.Warning, new ConsoleFormat{fgColor=ConsoleColor.Yellow, bgColor=ConsoleColor.Black, prefix="WARNING" }},
            {DebugLevel.Error, new ConsoleFormat{fgColor=ConsoleColor.Red, bgColor=ConsoleColor.Black, prefix="ERROR" }}
        };

        // Manages the console output
        public static void Print(string message, DebugLevel level)
        {
            Console.ForegroundColor = DebugFormat[level].fgColor;
            Console.BackgroundColor = DebugFormat[level].bgColor;
            Console.WriteLine($"[{DebugFormat[level].prefix}] {message}");
            Console.ResetColor();
        }

    }
}
