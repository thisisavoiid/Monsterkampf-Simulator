using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf_Simulator
{
    public class GUIHandler
    {
        // This method prints the header of the game with a rainbow color effect
        static public void PrintHeaderIcon()
        {
            // The ASCII art for the header
            string headerIcon = "______  ___                   _____                 ______________        ______ _____             \r\n" +
                                "___   |/  /_____________________  /_____________    ___  ____/__(_)______ ___  /___  /_____________\r\n" +
                                "__  /|_/ /_  __ \\_  __ \\_  ___/  __/  _ \\_  ___/    __  /_   __  /__  __ `/_  __ \\  __/  _ \\_  ___/\r\n" +
                                "_  /  / / / /_/ /  / / /(__  )/ /_ /  __/  /        _  __/   _  / _  /_/ /_  / / / /_ /  __/  /    \r\n" +
                                "/_/  /_/  \\____//_/ /_//____/ \\__/ \\___//_/         /_/      /_/  _\\__, / /_/ /_/\\__/ \\___//_/     \r\n" +
                                "                                                                  /____/                           \r\n" +
                                "(Made by Jonathan Huber)\n";

            int i = 1; // Counter to cycle through colors for the rainbow effect

            // Loop through each character in the header string
            foreach (char c in headerIcon)
            {
                // Change the console text color based on the counter
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

                Console.Write(c); // Print the character with the selected color

                // Increment color counter, reset after 6 to loop the rainbow
                if (i < 6) i++; else i = 1;
            }

            Console.ResetColor(); // Reset the console color back to default after printing
        }


        static public string BuildSingleBar(float curr_value, float max_value, int len, bool isInverted)
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

        static public void ClearConsole(bool keepHeader = true)
        {
            Console.CursorVisible = false;
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
            Console.CursorVisible= true;
        }
        static public void PrintAllMonsterStats(Monster monster01, Monster monster02)
        {
            ClearConsole(true);
            Console.Write("\n");
            PrintSingleMonsterStatBars(monster01);
            Console.WriteLine("\n");
            PrintSingleMonsterStatBars(monster02);
        }

        static public void PrintSingleMonsterStatBars(Monster monster)
        {

            string hpBar = BuildSingleBar(monster.HP, monster.maxHP, 20, false) + $" {Math.Max(0,monster.HP)} HP";
            string apBar = BuildSingleBar(monster.AP, GameSetupFlow.maxValue, 20, false) + $" {monster.AP} AP";
            string dpBar = BuildSingleBar(monster.DP, GameSetupFlow.maxValue, 20, false) + $" {monster.DP} DP";
            string speedBar = BuildSingleBar(monster.S, GameSetupFlow.maxValue, 20, true) + $" {monster.S} S";

            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine((monster.Class.ToString().ToUpper()));

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
