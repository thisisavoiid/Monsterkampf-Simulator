using System.Drawing;

namespace Monsterkampf_Simulator
{
    internal class Program
    {
        
        public static Monster monster01;
        public static Monster monster02;

        public static int currentRound = 1;

        private static Thread monster01FightThread = new Thread(Monster01FightThread);
        private static Thread monster02FightThread = new Thread(Monster02FightThread);
        private static Thread fightThreadHandler = new Thread(ThreadHandler);
        private static Thread guiUpdateThreadHandler = new Thread(GUIUpdateThread);

        static void GUIUpdateThread()
        {
            while (fightThreadHandler.IsAlive)
            {
                GUIHandler.PrintAllMonsterStats(monster01, monster02);
                Thread.Sleep(250);
            }
            return;
        }

        static void Monster01FightThread()
        {
            while (monster01.IsAlive() && monster02.IsAlive())
            {
                monster01.Attack(monster02);
                currentRound++;
            }
            return;
        }

        static void Monster02FightThread()
        {
            while (monster01.IsAlive() && monster02.IsAlive())
            {
                monster02.Attack(monster01);
                currentRound++;
            }
            return;
        }

        static void ThreadHandler()
        {

            monster01FightThread.Start();
            monster02FightThread.Start();
            guiUpdateThreadHandler.Start();

            monster01FightThread.Join();
            monster02FightThread.Join();


        }

        static void EndGameFlow()
        {

            Console.WriteLine("\n");

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

            GUIHandler.PrintAllMonsterStats(monster01, monster02);

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
