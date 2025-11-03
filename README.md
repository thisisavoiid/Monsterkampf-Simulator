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
git clone https://github.com/thisisavoiid/Monsterkampf-Simulator.git
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

# Escape Room

A console-based **Escape Room game** written in C#. Players navigate through rooms, find keys, and unlock doors to escape. The game features dynamic map generation, level-based gameplay, typewriter-style text display, and interactive sound effects.

---

## Table of Contents

- [Features](#features)
- [Gameplay](#gameplay)
- [Installation](#installation)
- [Usage](#usage)
- [Mechanics & Design](#mechanics--design)
- [Future Improvements](#future-improvements)
- [Author](#author)

---

## Features

- **Customizable room size** or pre-defined level progression.
- Interactive player movement using **WASD keys**.
- Collect keys to unlock doors and escape each room.
- Dynamic map generation with walls, floors, doors, and keys.
- Real-time console updates with typewriter-style messages.
- Console sound effects for movement, picking up items, and success.
- Easy to extend with new levels or mechanics.

---

## Gameplay

1. Run the program.
2. Choose a game mode:
   - **Custom Mode**: Define your own room size and play in a single generated room.
   - **Level Mode**: Progress through pre-defined levels with increasing complexity.
3. Navigate the map using **WASD** keys.
4. Collect keys (`K`) to unlock doors (`D`).
5. Escape each room to progress to the next level or finish the game.
6. A move counter tracks the number of player actions.

> The player’s position, keys, doors, walls, and ground tiles are represented with colored console blocks for a visually engaging experience.

---

## Installation

1. Clone the repository:
```bash
git clone https://github.com/thisisavoiid/Escape_Room.git
````

2. Open the solution in **Visual Studio 2022** or later.
3. Build the solution (`Ctrl+Shift+B`) and run (`F5`) in **Console mode**.

---

## Usage

* Input numbers to set room size (10–20 blocks) in Custom Mode.
* Move with **WASD keys**.
* The console shows:

  * Player in **Blue**
  * Ground in **Gray**
  * Walls in **DarkGray**
  * Keys in **Yellow**
  * Doors in **Magenta**
* Sounds provide feedback for actions like movement, picking up keys, and unlocking doors.
* Typewriter-style messages display dialogues and success notifications.

---

## Mechanics & Design

* **Map System**: Rooms are 2D arrays with characters representing walls, floor, keys, doors, and player.
* **Game Modes**:

  * Custom Mode: Random map generation based on user-defined size.
  * Level Mode: Predefined levels with increasing difficulty.
* **Movement Logic**:

  * Checks for collisions with walls or locked doors.
  * Updates the console visually and plays sound feedback.
* **Win Condition**:

  * Player collects key and reaches the door.
  * Progresses to next level in Level Mode or finishes in Custom Mode.
* **Typewriter Effect**:

  * Displays dialogue and messages character-by-character.
  * Optional sound effects for immersive gameplay.

---

## Future Improvements

* Add more interactive elements (traps, puzzles, enemies).
* Implement a scoring system based on moves or time.
* Enhance console visuals with richer ASCII graphics.
* Add save/load functionality for custom rooms or level progress.

---

## Author

**Jonathan Huber** – Developed as part of a console-based C# game project.

---

> Made with ❤️ in C#
