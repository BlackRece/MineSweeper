# MineSweeper - Coding Challenge 
Idea came from The Coding Train's Coding Challenge #71 (video: https://www.youtube.com/watch?v=LFU5ZlrR21E)

# MineSweeper - Coding Challenge

![MineSweeper](minesweeper_demo.gif)

This repository contains my implementation of the classic game Minesweeper as part of a coding challenge. The idea for this project came from The Coding Train's Coding Challenge #71. You can watch the video [here](https://www.youtube.com/watch?v=LFU5ZlrR21E) to see the original challenge.

## Introduction

Minesweeper is a single-player puzzle game where the player's objective is to clear a rectangular board containing hidden "mines" or bombs, without detonating any of them. The board is divided into cells, and each cell may contain a mine or a number indicating how many mines are adjacent to it. The player must strategically reveal cells and use the numerical information to deduce the locations of mines. The game is won when all non-mine cells are revealed.

## How to Play

1. Clone the repository to your local machine.

2. Open the Unity project in Unity Editor.

3. Navigate to the main scene that contains the Minesweeper game.

4. Press the Play button in Unity Editor to start the game.

5. The game will display a grid representing the game board, initially filled with hidden cells.

6. Use the mouse to interact with the game:
   - Left-click on a cell: Reveal the cell.
   - Right-click on a cell: Flag/unflag the cell as a potential mine.

7. The game will show the number of adjacent mines for each revealed cell. Use this information to strategize and avoid mines.

8. Continue revealing cells and flagging potential mines until all non-mine cells are revealed or all mines are flagged.

9. If you accidentally reveal a mine, the game ends, and you lose. But don't worry; you can always start a new game!

## Requirements

To run the Minesweeper game, you need to have Unity installed on your system. Unity is available for free from the official website: [https://unity.com/](https://unity.com/)

## Folder Structure

The repository is structured as follows:

- `Assets`: Contains all the Unity assets for the Minesweeper project.
- `Scripts`: Contains the C# scripts used to implement the game logic.
- `Scenes`: Contains the Unity scenes, and the main scene where the Minesweeper game is implemented.
- `README.md`: This file you are currently reading.

## Acknowledgments

This implementation was inspired by The Coding Train's Coding Challenge #71 on Minesweeper. Special thanks to Daniel Shiffman for creating the challenge and providing the inspiration for this project.

## Contributing

If you find any issues or have ideas for improvements, feel free to open an issue or create a pull request. Contributions are always welcome!

## License

This project is licensed under the [MIT License](LICENSE.md).

---

Enjoy playing Minesweeper and happy coding! If you have any questions or need assistance, feel free to reach out.

**Author:** Your Name
**Contact:** your.email@example.com
**Project Link:** [https://github.com/your-username/minesweeper](https://github.com/your-username/minesweeper)
