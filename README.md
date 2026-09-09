# Kitchen Chaos

Kitchen Chaos is a 3D Unity cooking game where you prepare meals under pressure, combine ingredients, complete recipes, and deliver orders before time runs out.

## Languages & Tools

<p align="left">
  <img src="https://img.shields.io/badge/Unity-6000.3.18f1-000000?style=for-the-badge&logo=unity&logoColor=white" alt="Unity 6000.3.18f1" height="36" />
  <img src="https://raw.githubusercontent.com/dotnet/vscode-csharp/main/images/csharpIcon.png" alt="C# logo" height="36" />
  <img src="https://upload.wikimedia.org/wikipedia/commons/6/6e/JetBrains_Rider_Icon.svg" alt="JetBrains Rider logo" height="36" />
</p>

## Features

- Fast-paced kitchen gameplay with interactive counters and appliances
- Ingredient pickup, chopping, cooking, plating, and trash mechanics
- Recipe system with order delivery and score tracking
- Countdown, game-start, pause, and game-over screens
- Progress bars and visual feedback for cutting and frying
- Sound effects for interactions, cooking, delivery, and UI buttons
- Single-player and local multiplayer support
- URP rendering and 3D Unity setup

## Controls

- `W`, `A`, `S`, `D`/ `Arrow Keys` - Move player
- `E` - Interact / pick up / use counter
- `F` - Interact Alternate / cut the food on the counter
- `Escape` - Pause / resume the game
- `Play` button in the menu - Start the game
- `Resume` button - Continue a paused game
- `Main Menu` button - Return to the main menu
- `Quit` button - Exit the game

## How To Play

1. Launch the game from the main menu.
2. Pick up ingredients from the kitchen counters.
3. Chop and cook ingredients as required by the active recipes.
4. Place the finished ingredients on a plate in the correct order.
5. Deliver completed meals before the timer runs out.
6. Work quickly to earn the highest score possible.

## Getting Started

### Requirements

- Unity 6000.3.18f1
- JetBrains Rider or another C# editor

### Open The Project

1. Clone the repository.
2. Open the folder in Unity Hub.
3. Select Unity version `6000.3.18f1`.
4. Open the project and let Unity import the assets.
5. Open the main scene in `Assets/Scenes` to start the game.

## Project Structure

- `Assets/Scenes` - Main menu and gameplay scenes
- `Assets/Scripts` - Core game logic, kitchen interactions, UI, recipes, and loading
- `Assets/Prefabs` - Player, counter, ingredient, and kitchen prefabs
- `Assets/Materials` - Materials and visual assets
- `Assets/Sounds` - Music and sound effects
- `Assets/Animations` - Player animation controllers and clips
- `ProjectSettings` - Unity project configuration

## Credits

- Tutorial / inspiration source: [Code Monkey Kitchen Chaos tutorial](https://www.youtube.com/watch?v=AmGSEH7QcDg)
- TextMesh Pro font assets and other Unity package content are included in the project
- Try my game for free: [Itch.io](https://laksondev.itch.io/kitchen-chaos), [Unity Play](https://play.unity.com/en/games/c25cfe71-54b7-42b1-9eb7-9d1b4682d8e7/kitchen-chaos)

## Notes

- This project uses the Unity Input System package.
- Game state is coordinated through player, kitchen counter, recipe, delivery, and UI scripts.
