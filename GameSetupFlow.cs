using System.Reflection;
using static Monsterkampf_Simulator.Program;

namespace Monsterkampf_Simulator
{
    internal class GameSetupFlow
    {

        static public int maxValue = 1000;
        static public int minValue = 10;

        private static Type[] GetAllMonsters()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            List<Type> classList = new List<Type>();

            foreach (Type classItem in executingAssembly.GetTypes().Where(type => type.IsClass).ToList())
            {
                if (classItem.IsSubclassOf(typeof(Monster)))
                {
                    classList.Add(classItem);
                }
            }
            return classList.ToArray();
        }

        public static void PlayerOnboardingFlow()
        {
            int scopeIndex = 1;
            int monsterIndex = 1;
            string input;

            GUIHandler.PrintHeaderIcon();

            List<Monster> availableMonsters = new List<Monster>();

            foreach (Type monsterClass in GetAllMonsters())
            {
                Monster monsterInstance = (Monster)Activator.CreateInstance(monsterClass);
                availableMonsters.Add(monsterInstance);
            }


            while (true)
            {

                Console.CursorVisible = true;
                Console.ForegroundColor = ConsoleColor.Yellow;

                switch (scopeIndex)
                {

                    case 1:
                        {

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n=> Choose your " + (monsterIndex == 1 ? "first" : "second") + " monster class:\n");

                            for (int i = 0; i < availableMonsters.Count; i++)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"[{i + 1}] {availableMonsters[i].GetType().Name}");
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.WriteLine($"- {availableMonsters[i]._description}\n");
                            }

                            Console.ResetColor();

                            input = Console.ReadLine();

                            if (LogicLib.IsNumeral(input))
                            {
                                int choice = int.Parse(input);
                                if (choice > 0 && choice <= availableMonsters.Count)
                                {
                                    if (monsterIndex == 1)
                                    {
                                        monster01 = availableMonsters[choice - 1];
                                    }
                                    else if (monsterIndex == 2)
                                    {
                                        monster02 = availableMonsters[choice - 1];
                                    }
                                    GUIHandler.ClearConsole(true);
                                    availableMonsters.Remove(availableMonsters[choice - 1]);
                                    scopeIndex++;

                                }
                                else
                                {
                                    DebugPrinter.Print(
                                        level: DebugPrinter.DebugLevel.Error,
                                        message: $"The entered value is outside of the given scope.",
                                        deleteAfter: 1000
                                    );
                                }
                                break;
                            }
                            else
                            {
                                DebugPrinter.Print(
                                    level: DebugPrinter.DebugLevel.Error,
                                    message: $"The entered value is not a number.",
                                    deleteAfter: 1000
                                );
                                break;
                            }
                        }

                    case 2:
                        {
                            Console.Write($"\n=> Set the total HP (health points) of your {(monsterIndex == 1 ? monster01.GetType().Name : monster02.GetType().Name)}: ");
                            input = Console.ReadLine();

                            if (LogicLib.IsNumeral(input))
                            {
                                if (LogicLib.IsInRange(int.Parse(input), minValue, maxValue))
                                {
                                    if (monsterIndex == 1)
                                    {
                                        monster01._hp = int.Parse(input);
                                        monster01._maxHp = int.Parse(input);
                                    }
                                    else if (monsterIndex == 2)
                                    {
                                        monster02._hp = int.Parse(input);
                                        monster02._maxHp = int.Parse(input);
                                    }
                                    GUIHandler.ClearConsole(true);
                                    scopeIndex++;
                                }
                                else
                                {
                                    DebugPrinter.Print(
                                        level: DebugPrinter.DebugLevel.Error,
                                        message: $"The entered value exceeds the allowed range {minValue}-{maxValue}.",
                                        deleteAfter: 1000
                                    );
                                }
                            }
                            else
                            {
                                DebugPrinter.Print(
                                    level: DebugPrinter.DebugLevel.Error,
                                    message: $"The entered value is not a number.",
                                    deleteAfter: 1000
                                );
                            }
                            break;
                        }

                    case 3:
                        {
                            Console.Write($"\n=> Set the attack power (damage) of your {(monsterIndex == 1 ? monster01.GetType().Name : monster02.GetType().Name)}: ");
                            input = Console.ReadLine();

                            if (LogicLib.IsNumeral(input))
                            {
                                if (LogicLib.IsInRange(int.Parse(input), minValue, maxValue))
                                {
                                    if (monsterIndex == 1)
                                    {
                                        monster01._ap = int.Parse(input);
                                        GUIHandler.ClearConsole(true);
                                        scopeIndex++;
                                    }
                                    else if (monsterIndex == 2)
                                    {
                                        if (int.Parse(input) > monster01._dp)
                                        {
                                            monster02._ap = int.Parse(input);
                                            GUIHandler.ClearConsole(true);
                                            scopeIndex++;
                                        }
                                        else
                                        {
                                            DebugPrinter.Print(
                                                level: DebugPrinter.DebugLevel.Error,
                                                message: $"Attack must be higher than the opponent's DP ({monster01._dp})!",
                                                deleteAfter: 1000
                                            );
                                        }
                                    }
                                }
                                else
                                {
                                    DebugPrinter.Print(
                                        level: DebugPrinter.DebugLevel.Error,
                                        message: $"The entered value exceeds the allowed range {minValue}-{maxValue}.",
                                        deleteAfter: 1000
                                    );
                                }
                            }
                            else
                            {
                                DebugPrinter.Print(
                                    level: DebugPrinter.DebugLevel.Error,
                                    message: $"The entered value is not a number.",
                                    deleteAfter: 1000
                                );

                            }
                            break;
                        }

                    case 4:
                        {
                            Console.Write($"\n=> Set the defense points (damage reduction) of your {(monsterIndex == 1 ? monster01.GetType().Name : monster02.GetType().Name)}: ");
                            input = Console.ReadLine();

                            if (LogicLib.IsNumeral(input))
                            {
                                if (LogicLib.IsInRange(int.Parse(input), minValue, maxValue))
                                {
                                    if (monsterIndex == 1)
                                    {
                                        monster01._dp = int.Parse(input);
                                        GUIHandler.ClearConsole(true);
                                        scopeIndex++;
                                    }
                                    else if (monsterIndex == 2)
                                    {
                                        if (int.Parse(input) < monster01._ap)
                                        {
                                            monster02._dp = int.Parse(input);
                                            GUIHandler.ClearConsole(true);
                                            scopeIndex++;
                                        }
                                        else
                                        {
                                            DebugPrinter.Print(
                                                level: DebugPrinter.DebugLevel.Error,
                                                message: $"Defense must be lower than the opponent's AP ({monster01._ap})!",
                                                deleteAfter: 1000
                                            );
                                        }
                                    }

                                }
                                else
                                {
                                    DebugPrinter.Print(
                                        level: DebugPrinter.DebugLevel.Error,
                                        message: $"The entered value exceeds the allowed range {minValue}-{maxValue}.",
                                        deleteAfter: 1000
                                    );
                                }
                            }
                            else
                            {
                                DebugPrinter.Print(
                                    level: DebugPrinter.DebugLevel.Error,
                                    message: $"The entered value is not a number.",
                                    deleteAfter: 1000
                                );
                            }
                            break;
                        }

                    case 5:
                        {
                            Console.Write($"\n=> Set the attack speed of your {(monsterIndex == 1 ? monster01.GetType().Name : monster02.GetType().Name)}: ");
                            input = Console.ReadLine();

                            if (LogicLib.IsNumeral(input))
                            {
                                if (LogicLib.IsInRange(int.Parse(input), minValue, maxValue))
                                {
                                    if (monsterIndex == 1)
                                    {
                                        monster01._s = int.Parse(input);
                                    }
                                    else if (monsterIndex == 2)
                                    {
                                        monster02._s = int.Parse(input);
                                    }
                                    if (monsterIndex == 2 && scopeIndex == 5)
                                    {
                                        GUIHandler.ClearConsole(true);
                                        return;
                                    }

                                    monsterIndex = 2;
                                    scopeIndex = 1;
                                    GUIHandler.ClearConsole(true);
                                }
                                else
                                {
                                    DebugPrinter.Print(
                                        level: DebugPrinter.DebugLevel.Error,
                                        message: $"The entered value exceeds the allowed range {minValue}-{maxValue}.",
                                        deleteAfter: 1000
                                    );
                                }
                            }
                            else
                            {
                                DebugPrinter.Print(
                                    level: DebugPrinter.DebugLevel.Error,
                                    message: $"The entered value is not a number.",
                                    deleteAfter: 1000
                                );
                            }


                            break;

                        }
                }
            }
        }
    }
}
