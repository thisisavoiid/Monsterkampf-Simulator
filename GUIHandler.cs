using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf_Simulator
{
    public class GUIHandler
    {
        // This method prints the game header with a rainbow color effect
        static public void PrintHeaderIcon()
        {
            // The ASCII art for the header including the "Made by" line
            string headerIcon = "______  ___                   _____                 ______________        ______ _____             \r\n" +
                                "___   |/  /_____________________  /_____________    ___  ____/__(_)______ ___  /___  /_____________\r\n" +
                                "__  /|_/ /_  __ \\_  __ \\_  ___/  __/  _ \\_  ___/    __  /_   __  /__  __ `/_  __ \\  __/  _ \\_  ___/\r\n" +
                                "_  /  / / / /_/ /  / / /(__  )/ /_ /  __/  /        _  __/   _  / _  /_/ /_  / / / /_ /  __/  /    \r\n" +
                                "/_/  /_/  \\____//_/ /_//____/ \\__/ \\___//_/         /_/      /_/  _\\__, / /_/ /_/\\__/ \\___//_/     \r\n" +
                                "                                                                  /____/                           \r\n" +
                                "(Made by Jonathan Huber)\n";

            int i = 1; // Counter to cycle through colors for the rainbow effect

            // Loop through each character of the header string
            foreach (char c in headerIcon)
            {
                // Change console color based on the current counter value to create rainbow effect
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

                Console.Write(c); // Print the current character in the chosen color

                // Increment color counter, reset to 1 after 6 to loop the rainbow
                if (i < 6) i++; else i = 1;
            }

            Console.ResetColor(); // Reset the console color back to default after printing
        }

        // Builds a single horizontal progress bar for stats like HP, AP, DP, Speed
        static public string BuildSingleBar(float curr_value, float max_value, int len, bool isInverted)
        {
            char filledChunkChar = '█'; // Character representing filled portion
            char emptyChunkChar = '░';  // Character representing empty portion

            // Calculate the number of characters to fill proportionally
            // Clamp at 0 to prevent negative values
            int stringLen = Math.Max(0, (int)Math.Round(len * (curr_value / max_value)));

            if (isInverted)
            {
                // For inverted bars (higher value = more empty space)
                return new string(filledChunkChar, len - stringLen) + new string(emptyChunkChar, stringLen);
            }
            else
            {
                // Normal bars (higher value = more filled space)
                return new string(filledChunkChar, stringLen) + new string(emptyChunkChar, len - stringLen);
            }
        }

        // Clears the console while optionally keeping the header at the top
        static public void ClearConsole(bool keepHeader = true)
        {
            Console.CursorVisible = false; // Hide the cursor during clearing

            if (keepHeader)
            {
                // Clear only the lines below the header
                for (int i = 7; i < Console.WindowHeight; i++)
                {
                    Console.SetCursorPosition(0, i);              // Move cursor to the start of the line
                    Console.Write(new string(' ', Console.WindowWidth)); // Fill line with spaces
                }
                Console.SetCursorPosition(0, 7); // Reset cursor position below header
            }
            else
            {
                Console.Clear(); // Completely clear the console
            }

            Console.CursorVisible = true; // Make cursor visible again
        }

        // Prints the stat bars for both monsters
        static public void PrintAllMonsterStats(Monster monster01, Monster monster02)
        {
            ClearConsole(true); // Clear console but keep header
            Console.Write("\n"); // Add a blank line for spacing

            PrintSingleMonsterStatBars(monster01); // Print first monster stats
            Console.WriteLine("\n");                 // Spacing between monsters
            PrintSingleMonsterStatBars(monster02); // Print second monster stats
        }

        // Prints the stat bars for a single monster
        static public void PrintSingleMonsterStatBars(Monster monster)
        {
            // Build the progress bars for each stat
            string hpBar = BuildSingleBar(monster.HP, monster.maxHP, 20, false) + $" {Math.Max(0, monster.HP)} HP";
            string apBar = BuildSingleBar(monster.AP, GameSetupFlow.maxValue, 20, false) + $" {monster.AP} AP";
            string dpBar = BuildSingleBar(monster.DP, GameSetupFlow.maxValue, 20, false) + $" {monster.DP} DP";
            string speedBar = BuildSingleBar(monster.S, GameSetupFlow.maxValue, 20, true) + $" {monster.S} S";

            Console.ResetColor(); // Reset color before printing titles

            // Print the class name of the monster in blue
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine((monster.Class.ToString().ToUpper()));

            // Print each stat bar in its respective color
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(hpBar);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(apBar);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(dpBar);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(speedBar);

            Console.ResetColor(); // Reset console color after printing
        }
    }
}
