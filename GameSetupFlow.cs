using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Monsterkampf_Simulator.Program;

namespace Monsterkampf_Simulator
{
    internal class GameSetupFlow
    {
        // This method handles the onboarding flow for the player to configure their monsters
        public static void PlayerOnboardingFlow()
        {
            int scopeIndex = 1; // Tracks which step of the setup flow we're in
            int monsterIndex = 1; // Tracks whether we're setting up the first or second monster
            string input; // Variable to store user input from console

            List<MonsterClass> availableMonsters = new List<MonsterClass>();

            // Populate the availableMonsters list with all possible MonsterClass enum values
            foreach (int monsterClassIndex in Enum.GetValues(typeof(MonsterClass)))
            {
                availableMonsters.Add((MonsterClass)monsterClassIndex);
            }

            // Main loop that runs the onboarding process
            while (true)
            {
                switch (scopeIndex)
                {
                    case 1:
                        {
                            // Display the header/logo for the UI
                            GUIHandler.PrintHeaderIcon();
                            Console.WriteLine("\nChoose your " + (monsterIndex == 1 ? "first" : "second") + " monster class:");

                            // List all available monsters with a number for selection
                            for (int i = 0; i < availableMonsters.Count; i++)
                            {
                                Console.WriteLine($"[{i + 1}] {availableMonsters[i]}");
                            }

                            input = Console.ReadLine();

                            // Check if input is a number
                            if (LogicLib.IsNumeral(input))
                            {
                                int choice = int.Parse(input);
                                // Validate that the number is within the valid range
                                if (choice > 0 && choice <= availableMonsters.Count)
                                {
                                    // Assign chosen monster class to the first or second monster
                                    if (monsterIndex == 1)
                                    {
                                        monster01.Class = availableMonsters[choice - 1];
                                        availableMonsters.Remove(availableMonsters[choice - 1]); // Remove selected monster so it can't be picked again
                                    }
                                    else if (monsterIndex == 2)
                                    {
                                        monster02.Class = availableMonsters[choice - 1];
                                    }
                                    scopeIndex++; // Move to the next step
                                    break;
                                }
                                else
                                {
                                    // Print error if number is out of range
                                    DebugPrinter.Print(
                                        level: DebugPrinter.DebugLevel.Error,
                                        message: $"The entered value is outside of the given scope."
                                    );
                                    break;
                                }
                            }
                            else
                            {
                                // Print error if input is not a number
                                DebugPrinter.Print(
                                    level: DebugPrinter.DebugLevel.Error,
                                    message: $"The entered value is not a number."
                                );
                                break;
                            }
                        }

                    case 2:
                        {
                            // Ask user to set the total HP (health points) for the monster
                            Console.Write($"\nSet the total HP (health points) of your {(monsterIndex == 1 ? monster01.Class : monster02.Class)}: ");
                            input = Console.ReadLine();

                            if (LogicLib.IsNumeral(input))
                            {
                                // Assign HP to the correct monster
                                if (monsterIndex == 1)
                                {
                                    monster01.HP = int.Parse(input);
                                }
                                else if (monsterIndex == 2)
                                {
                                    monster02.HP = int.Parse(input);
                                }
                                scopeIndex++; // Move to next step
                                break;
                            }
                            else
                            {
                                DebugPrinter.Print(
                                    level: DebugPrinter.DebugLevel.Error,
                                    message: $"The entered value is not a number."
                                );
                                break;
                            }
                        }

                    case 3:
                        {
                            // Ask user to set the attack power of the monster
                            Console.Write($"\nSet the attack power (damage) of your {(monsterIndex == 1 ? monster01.Class : monster02.Class)}: ");
                            input = Console.ReadLine();

                            if (LogicLib.IsNumeral(input))
                            {
                                if (monsterIndex == 1)
                                {
                                    monster01.AP = int.Parse(input);
                                }
                                else if (monsterIndex == 2)
                                {
                                    monster02.AP = int.Parse(input);
                                }
                                scopeIndex++; // Move to next step
                                break;
                            }
                            else
                            {
                                DebugPrinter.Print(
                                    level: DebugPrinter.DebugLevel.Error,
                                    message: $"The entered value is not a number."
                                );
                                break;
                            }
                        }

                    case 4:
                        {
                            // Ask user to set the defense points (damage reduction) of the monster
                            Console.Write($"\nSet the defense points (damage reduction) of your {(monsterIndex == 1 ? monster01.Class : monster02.Class)}: ");
                            input = Console.ReadLine();

                            if (LogicLib.IsNumeral(input))
                            {
                                if (monsterIndex == 1)
                                {
                                    monster01.DP = int.Parse(input);
                                }
                                else if (monsterIndex == 2)
                                {
                                    monster02.DP = int.Parse(input);
                                }
                                scopeIndex++; // Move to next step
                                break;
                            }
                            else
                            {
                                DebugPrinter.Print(
                                    level: DebugPrinter.DebugLevel.Error,
                                    message: $"The entered value is not a number."
                                );
                                break;
                            }
                        }

                    case 5:
                        {
                            // Ask user to set the attack speed of the monster
                            Console.Write($"\nSet the attack speed of your {(monsterIndex == 1 ? monster01.Class : monster02.Class)}: ");
                            input = Console.ReadLine();

                            if (LogicLib.IsNumeral(input))
                            {
                                if (monsterIndex == 1)
                                {
                                    monster01.S = int.Parse(input);
                                }
                                else if (monsterIndex == 2)
                                {
                                    monster02.S = int.Parse(input);
                                }

                                // If second monster is fully set, end the onboarding flow
                                if (monsterIndex == 2 && scopeIndex == 5)
                                {
                                    Console.Clear();
                                    GUIHandler.PrintHeaderIcon();
                                    return; // Exit the method
                                }

                                // Switch to the second monster setup
                                monsterIndex = 2;
                                scopeIndex = 1;
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                DebugPrinter.Print(
                                    level: DebugPrinter.DebugLevel.Error,
                                    message: $"The entered value is not a number."
                                );
                                break;
                            }
                        }
                }
            }
        }
    }
}
