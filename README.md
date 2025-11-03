# Monsterkampf Simulator

A console-based **monster battle simulator** written in C#. Players select two monsters, set their stats, and watch them fight in real-time with a live-updating console interface. The game features multithreading, ASCII-based UI, and custom monster abilities.

---

## Table of Contents

- [Features](#features)
- [Monster Classes](#monster-classes)
- [Gameplay](#gameplay)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Multithreading & Mechanics](#multithreading--mechanics)
- [Future Improvements](#future-improvements)
- [Author](#author)

---

## Features

- Interactive monster selection and stat customization.
- Real-time combat simulation with **parallel threads** for each monster.
- Live-updating console interface with health, attack, defense, and speed bars.
- Action board displaying the latest combat events.
- Colored console output for better visual experience.
- Extensible monster system via inheritance.

---

## Monster Classes

The base abstract class is `Monster`. Each monster has:

- **HP (_hp)** – Health points
- **AP (_ap)** – Attack power
- **DP (_dp)** – Defense points
- **S (_s)** – Speed (affects attack timing)
- **Description (_description)** – Monster's lore and abilities

Current monster subclasses:

- **Ork** – Has a thorn effect that reflects a portion of damage back to the attacker.
- **Troll** – Simple high-defense monster.
- **Goblin** – Simple agile monster.

> Additional monsters can be added easily by creating new classes that inherit from `Monster`.

---

## Gameplay

1. Run the program.
2. Select two monsters from the available list.
3. Customize their stats:
   - **HP**: Total health points.
   - **AP**: Attack power (must be higher than opponent's DP for Monster2).
   - **DP**: Defense points (must be lower than opponent's AP for Monster2).
   - **Speed**: Determines attack frequency.
4. Watch the monsters fight in real-time.
5. The action board displays all damage dealt and special effects.
6. The winner is announced once one monster's HP reaches 0.

---

## Installation

1. Clone the repository:
```bash
git clone https://github.com/yourusername/Monsterkampf-Simulator.git
````

2. Open the solution in **Visual Studio 2022** or later.
3. Build the solution (`Ctrl+Shift+B`) and run (`F5`) in **Console mode**.

---

## Usage

* Input numeric values during onboarding. Non-numeric or out-of-range inputs are rejected.
* Stats must respect constraints (Monster2’s AP > Monster1’s DP, Monster2’s DP < Monster1’s AP).
* Console displays live stats:

  * **HP** in red
  * **AP** in green
  * **DP** in cyan
  * **Speed** in yellow
* Info board shows the last 3 combat actions.

---

## Project Structure

```
Monsterkampf_Simulator/
│
├─ Program.cs                 # Main entry point, manages threads and combat
├─ GameSetupFlow.cs           # Handles player onboarding & monster selection
├─ GUIHandler.cs              # Console UI, stats, ASCII header, and action board
├─ LogicLib.cs                # Helper functions: number validation and range checks
├─ DebugPrinter.cs            # Colored console logs (Info, Warning, Error)
├─ Monster.cs                 # Abstract base class and concrete monsters (Ork, Troll, Goblin)
└─ README.md                  # Project documentation
```

---

## Multithreading & Mechanics

* **Monster threads**: Each monster attacks in a separate thread.
* **GUI thread**: Continuously updates console with live stats.
* **Thread synchronization**: Main thread waits for monster threads to finish before displaying results.
* **Combat loop**: Attack → Damage calculation → InfoBoard update → Sleep based on speed.

---

## Future Improvements

* Add more monster types with unique abilities.
* Implement turn-based combat option.
* Include a save/load system for monster stats.
* Enhance console graphics or create a simple GUI version.

---

## Author

**Jonathan Huber** – Created as part of a game programming project.

---

> Made with ❤️ in C#
