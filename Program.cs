using System.Drawing;

namespace Monsterkampf_Simulator
{
    internal class Program
    {
        // Declaration of two (empty) monster objects to be further set up via the game onboarding flow.
        public static Monster monster01;
        public static Monster monster02;

        // Keeps track of the number of attacks happening throughout the game.
        public static int currentRound = 1;

        // Thread declaration.
        private static Thread monster01FightThread = new Thread(Monster01FightThread);
        private static Thread monster02FightThread = new Thread(Monster02FightThread);
        private static Thread fightThreadHandler = new Thread(ThreadHandler);
        private static Thread guiUpdateThreadHandler = new Thread(GUIUpdateThread);

        /// <summary>
        /// The thread which keeps the GUI updating.
        /// </summary>
        static void GUIUpdateThread()
        {
            while (fightThreadHandler.IsAlive)
            {
                GUIHandler.PrintAllMonsterStats(monster01, monster02);
                Thread.Sleep(250);
            }
            return;
        }

        /// <summary>
        /// The fight thread of the first monster.
        /// </summary>
        static void Monster01FightThread()
        {
            while (monster01.IsAlive() && monster02.IsAlive())
            {
                monster01.Attack(monster02);
                currentRound++;
            }
            return;
        }

        /// <summary>
        /// The fight thread of the second monster.
        /// </summary>
        static void Monster02FightThread()
        {
            while (monster01.IsAlive() && monster02.IsAlive())
            {
                monster02.Attack(monster01);
                currentRound++;
            }
            return;
        }

        /// <summary>
        /// Manages the main threads responsible for the asynchronous fights to happen and the GUI to update.
        /// </summary>
        static void ThreadHandler()
        {

            monster01FightThread.Start();
            monster02FightThread.Start();
            guiUpdateThreadHandler.Start();

            monster01FightThread.Join();
            monster02FightThread.Join();
        }

        /// <summary>
        /// Initiates the end game flow. 
        /// Used to determine which monster won the fight. 
        /// Also cleans up the console a bit.
        /// </summary>
        static void EndGameFlow()
        {

            Console.WriteLine("\n");

            // Used to prevent the issue of final stats not showing up properly.
            InfoBoard.Clear(); 
            GUIHandler.ClearConsole(true); 


            if (monster01.IsAlive())
            {
                InfoBoard.AddEntry(
                    new InfoBoardAction
                    {
                        content = $"{monster01.GetType().Name} has won the fight. Round counter: {currentRound}.",
                        fgColor = ConsoleColor.DarkMagenta,
                    }
                );
            }
            else if (monster02.IsAlive())
            {
                InfoBoard.AddEntry(
                    new InfoBoardAction
                    {
                        content = $"{monster02.GetType().Name} has won the fight. Round counter: {currentRound}.",
                        fgColor = ConsoleColor.DarkMagenta,
                    }
                );
            }
            else
            {
                DebugPrinter.Print(
                    level: DebugPrinter.DebugLevel.Error,
                    message: $"None of the two monsters won the fight. Please check for errors."
                );
            }

            // Used to prevent the issue of final stats not showing up properly.
            GUIHandler.PrintAllMonsterStats(monster01, monster02);

            // Plain keep-alive method.
            Console.ReadKey();

        }

        static void Main(string[] args)
        {
            GameSetupFlow.PlayerOnboardingFlow();

            Console.CursorVisible = false;

            fightThreadHandler.Start();
            fightThreadHandler.Join();

            EndGameFlow();
        }
    }
}
