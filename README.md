# .NET-Puzzle-Game

A Sudoku-Style game in the form of a Windows Forms application.

## Notable Features
- Separate main menu and game-board scenes. The game board is dynamically generated based on the level selected, so there is no need to create more scenes per each level.
- A file system automatically detects and evaluates completed levels when the game runs, and will send players to the next unbeaten level when appropriate.
- The save data for completed levels are serialized using a JSON package to make them easy to read (though this does have the disadvantage of them being easy to edit, it would make sense to save them in a binary form for an actual game).
- Save data can be cleared within the program to allow the user to replay levels.
- Levels are scored based on the amount of time taken to complete them, which is saved into the completed level files. The average and best of these times can be viewed from the main menu per difficulty.
- A hint button can be used to show the user where they've inserted an incorrect value, with an accompanying flash animation.
- Using a hint on a level will invalidate its score, meaning it will not show up on the main menu views. Users are prompted for confirmation upon the first attempt to use a hint.
- The user can pause the game to freeze the timer, but doing so will hide the game board while it is paused to prevent making decisions while paused.
