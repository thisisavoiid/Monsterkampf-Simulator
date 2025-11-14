using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Monsterkampf_Simulator
{
    /// <summary>
    /// Handles all console-based GUI output for the fight simulation,
    /// including headers, monster stats, info board printing, and visual bars.
    /// </summary>
    public class GUIHandler
    {
        /// <summary>
        /// Prints the colored ASCII header logo at the top of the console.
        /// Cycles through multiple colors for a rainbow-like effect.
        /// </summary>
        public static void PrintHeaderIcon()
        {
            string headerIcon = "______  ___                   _____                 ______________        ______ _____             \r\n" +
                                "___   |/  /_____________________  /_____________    ___  ____/__(_)______ ___  /___  /_____________\r\n" +
                                "__  /|_/ /_  __ \\_  __ \\_  ___/  __/  _ \\_  ___/    __  /_   __  /__  __ `/_  __ \\  __/  _ \\_  ___/\r\n" +
                                "_  /  / / / /_/ /  / / /(__  )/ /_ /  __/  /        _  __/   _  / _  /_/ /_  / / / /_ /  __/  /    \r\n" +
                                "/_/  /_/  \\____//_/ /_//____/ \\__/ \\___//_/         /_/      /_/  _\\__, / /_/ /_/\\__/ \\___//_/     \r\n" +
                                "                                                                  /____/                           \r\n" +
                                "(Made by Jonathan Huber)\n";

            int i = 1;

            foreach (char c in headerIcon)
            {
                switch (i)
                {
                    case 1: Console.ForegroundColor = ConsoleColor.Red; break;
                    case 2: Console.ForegroundColor = ConsoleColor.Yellow; break;
                    case 3: Console.ForegroundColor = ConsoleColor.Green; break;
                    case 4: Console.ForegroundColor = ConsoleColor.Cyan; break;
                    case 5: Console.ForegroundColor = ConsoleColor.Blue; break;
                    case 6: Console.ForegroundColor = ConsoleColor.Magenta; break;
                    default: Console.ForegroundColor = ConsoleColor.White; break;
                }

                Console.Write(c);

                if (i < 6) i++; else i = 1;
            }

            Console.ResetColor();
        }

        /// <summary>
        /// Prints the InfoBoard including all recent InfoBoardAction entries.
        /// If no entries exist, prints a fallback message.
        /// </summary>
        public static void PrintInfoBoard()
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(new string('=', 15) + " ACTION BOARD " + new string('=', 15));

            if (InfoBoard.GetInfoBoardContent().Length == 0)
            {
                Console.WriteLine("- No Infoboard Data available!");
                return;
            }

            foreach (InfoBoardAction action in InfoBoard.GetInfoBoardContent())
            {
                Console.ForegroundColor = action.fgColor;
                Console.WriteLine("- " + action.content);
            }

            Console.ResetColor();
        }

        /// <summary>
        /// Builds a visual bar using filled and empty characters.
        /// </summary>
        /// <param name="curr_value">
        /// Current value.
        /// </param>
        /// <param name="max_value">
        /// Maximum possible value.
        /// </param>
        /// <param name="len">
        /// Total length of the bar in characters.
        /// </param>
        /// <param name="isInverted">
        /// If true, swaps filled/empty positions.
        /// </param>
        /// <returns>
        /// A visual bar string.
        /// </returns>
        public static string BuildSingleBar(float curr_value, float max_value, int len, bool isInverted)
        {
            char filledChunkChar = '█';
            char emptyChunkChar = '░';

            int stringLen = Math.Max(0, (int)Math.Round(len * (curr_value / max_value)));

            if (isInverted)
            {
                return new string(filledChunkChar, len - stringLen) + new string(emptyChunkChar, stringLen);
            }
            else
            {
                return new string(filledChunkChar, stringLen) + new string(emptyChunkChar, len - stringLen);
            }
        }

        /// <summary>
        /// Clears the console window while optionally keeping the header visible.
        /// </summary>
        /// <param name="keepHeader">
        /// If true, clears only the content below the header.
        /// </param>
        public static void ClearConsole(bool keepHeader = true)
        {
            if (keepHeader)
            {
                for (int i = 7; i < Console.WindowHeight; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write(new string(' ', Console.WindowWidth));
                }
                Console.SetCursorPosition(0, 7);
            }
            else
            {
                Console.Clear();
            }
        }

        /// <summary>
        /// Prints the stats of two monsters, the header, and the info board in a single combined layout.
        /// </summary>
        public static void PrintAllMonsterStats(Monster monster01, Monster monster02)
        {
            Console.SetCursorPosition(0, 0);
            ClearConsole(true);
            Console.Write("\n");

            PrintSingleMonsterStats(monster01);
            Console.WriteLine("\n");
            PrintSingleMonsterStats(monster02);
            Console.WriteLine("\n");

            PrintInfoBoard();
        }

        /// <summary>
        /// Prints a single monster's HP, AP, DP, and Speed bars including their labels.
        /// </summary>
        public static void PrintSingleMonsterStats(Monster monster)
        {
            string hpBar = BuildSingleBar(monster._hp, monster._maxHp, 20, false) + $" {Math.Max(0, monster._hp)} HP";
            string apBar = BuildSingleBar(monster._ap, GameSetupFlow.maxValue, 20, false) + $" {monster._ap} AP";
            string dpBar = BuildSingleBar(monster._dp, GameSetupFlow.maxValue, 20, false) + $" {monster._dp} DP";
            string speedBar = BuildSingleBar(monster._s, GameSetupFlow.maxValue, 20, true) + $" {monster._s} S";

            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(monster.GetType().Name.ToUpper());

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(hpBar);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(apBar);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(dpBar);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(speedBar);

            Console.ResetColor();
        }
    }
}
