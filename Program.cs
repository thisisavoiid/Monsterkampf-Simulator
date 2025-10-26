using System.ComponentModel.Design;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Transactions;
using System.Threading;

namespace Monsterkampf_Simulator
{
    internal class Program
    {
        // Enum representing the different types/classes of monsters
        public enum MonsterClass
        {
            Ork = 1,
            Troll = 2,
            Goblin = 3
        }

        // Global monster objects used in the game
        public static Monster monster01 = new Monster(); // First monster
        public static Monster monster02 = new Monster(); // Second monster

        // Tracks the current round number of the fight
        public static int currentRound = 1;

        // Threads for running each monster's attack loop and handling fight coordination
        static Thread monster01FightThread = new Thread(Monster01FightThread);
        static Thread monster02FightThread = new Thread(Monster02FightThread);
        static Thread fightThreadHandler = new Thread(ThreadHandler);
        static Thread guiUpdateThreadHandler = new Thread(GUIUpdateThread);

        static void GUIUpdateThread()
        {
            while (fightThreadHandler.IsAlive)
            {
                GUIHandler.PrintAllMonsterStats(monster01, monster02);
                Thread.Sleep(250);
            }
            return;
        }

        // Thread method for Monster01 attacks
        static void Monster01FightThread()
        {
            // Continue attacking while both monsters are alive
            while (monster01.HP > 0 && monster02.HP > 0)
            {
                monster01.Attack(monster02); // Monster01 attacks Monster02
                currentRound++; // Increment round counter after each attack
            }
            return;
        }

        // Thread method for Monster02 attacks
        static void Monster02FightThread()
        {
            // Continue attacking while both monsters are alive
            while (monster01.HP > 0 && monster02.HP > 0)
            {
                monster02.Attack(monster01); // Monster02 attacks Monster01
                currentRound++; // Increment round counter after each attack
            }
            return;
        }

        // Thread handler to start and synchronize fight threads
        static void ThreadHandler()
        {
            monster01FightThread.Start(); // Start Monster01 attack thread
            monster02FightThread.Start(); // Start Monster02 attack thread
            guiUpdateThreadHandler.Start();

            // Wait for both fight threads to finish before proceeding
            monster01FightThread.Join();
            monster02FightThread.Join();
        }

        // Determines the winner and prints the result
        static void EndGameFlow()
        {
            Console.WriteLine("\n");

            if (monster01.HP > 0) // Monster01 still alive
            {
                DebugPrinter.Print(
                    level: DebugPrinter.DebugLevel.Info,
                    message: $"{monster01.Class} has won the fight. Round counter: {currentRound}."
                );
            }
            else if (monster02.HP > 0) // Monster02 still alive
            {
                DebugPrinter.Print(
                    level: DebugPrinter.DebugLevel.Info,
                    message: $"{monster02.Class} has won the fight. Round counter: {currentRound}."
                );
            }
            else // Both monsters dead (edge case)
            {
                DebugPrinter.Print(
                    level: DebugPrinter.DebugLevel.Error,
                    message: $"None of the two monsters won the fight. Please check for errors."
                );
            }

            // Keep-Alive
            Console.ReadKey();

        }

        // Main method - entry point of the program
        static void Main(string[] args)
        {
            // Runs the player onboarding setup to configure monsters


             GameSetupFlow.PlayerOnboardingFlow();

            /* Print debug info about monsters after creation
            DebugPrinter.Print(
                level: DebugPrinter.DebugLevel.Info,
                message: $"Monster of Type {monster01.Class} created successfully. Values have been set to: AP: {monster01.HP} DP: {monster01.DP} HP: {monster01.HP} S: {monster01.S}"
            );
            DebugPrinter.Print(
                level: DebugPrinter.DebugLevel.Info,
                message: $"Monster of Type {monster02.Class} created successfully. Values have been set to: AP: {monster02.HP} DP: {monster02.DP} HP: {monster02.HP} S: {monster02.S}"
            );
            */

            GUIHandler.PrintAllMonsterStats(monster01, monster02);

            // Starts the fight threads (both monsters attack simultaneously)
            fightThreadHandler.Start();
            fightThreadHandler.Join(); // Wait for the fight to finish

            // Determines the winner and prints the end game result
            EndGameFlow();
        }
    }
}
