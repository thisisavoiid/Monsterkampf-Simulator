namespace Monsterkampf_Simulator
{
    public class GUIHandler
    {
        private static List<string> infoBoardContent = new List<string>();
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

        public static void ClearConsole(bool keepHeader = true)
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

            Console.CursorVisible = true;
        }

        public static void PrintInfoBoard() {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(new string('=', 10) + " ACTION BOARD " + new string('=', 10));
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(string.Join("\n", infoBoardContent));
            Console.ResetColor();
        }

        public static void ClearInfoBoard()
        {
            infoBoardContent.Clear();
        }

        public static void AddInfoBoardEntry(string entry)
        {
            if (infoBoardContent.Count>5)
            {
                infoBoardContent.Remove(infoBoardContent[0]);
            }
            infoBoardContent.Add("- " + entry);
        }

        public static void PrintAllMonsterStats(Monster monster01, Monster monster02)
        {
            Console.SetCursorPosition(0, 0);
            ClearConsole(true);
            Console.Write("\n");

            PrintSingleMonsterStatBars(monster01);
            Console.WriteLine("\n");
            PrintSingleMonsterStatBars(monster02);
            Console.WriteLine("\n");
            PrintInfoBoard();
        }

        public static void PrintSingleMonsterStatBars(Monster monster)
        {
            string hpBar = BuildSingleBar(monster._hp, monster._maxHp, 20, false) + $" {Math.Max(0, monster._hp)} HP";
            string apBar = BuildSingleBar(monster._ap, GameSetupFlow.maxValue, 20, false) + $" {monster._ap} AP";
            string dpBar = BuildSingleBar(monster._dp, GameSetupFlow.maxValue, 20, false) + $" {monster._dp} DP";
            string speedBar = BuildSingleBar(monster._s, GameSetupFlow.maxValue, 20, true) + $" {monster._s} S";

            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine((monster.GetType().Name.ToUpper()));

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
