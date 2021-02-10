using System;

namespace BattleshipApp
{
    class BattleShip
    {
        
    }
    class Program
    {
        static void Main()
        {
            GameInformation();

            bool isGameOver = StartGame("Ready to play? [Y or N]: ");

            while (!isGameOver)
            {
                Console.Clear();
                string[,] grid = new string[10, 10];
                int[,] boatCoordinates = RandomizeShipLocation(grid);
                
                Console.WriteLine("");
                PrintColorMessage(ConsoleColor.Magenta, "Fire Away!");
                Console.WriteLine("");

                int userGuesses = 8;
                int directHit = 0;

                while (userGuesses > 0 && directHit < 5)
                {
                    PrintGrid(grid, false);
                    Console.WriteLine("");

                    PrintColorMessage(ConsoleColor.Cyan, "Remaining guesses: " + userGuesses);
                    Console.WriteLine("");

                    int userGuessX = ValidateInput("Enter X Coordinate: ");
                    int userGuessY = ValidateInput("Enter Y Coordinate: ");

                    Console.Clear();
                    Console.WriteLine("");


                    for (int i = 0; i < boatCoordinates.GetLength(0); i++)
                    {
                        if (boatCoordinates[i, 1] == userGuessX && boatCoordinates[i, 0] == userGuessY)
                        {
                            PrintColorMessage(ConsoleColor.Green, "Direct Hit!");
                            Console.WriteLine("");
                            grid[boatCoordinates[i, 0], boatCoordinates[i, 1]] = "X";
                            directHit++;
                            break;
                        }
                        if (i == 4)
                        {
                            PrintColorMessage(ConsoleColor.Red, "Missed!");
                            Console.WriteLine("");
                            grid[userGuessY, userGuessX] = "/";
                        }
                    }

                    if (directHit == 5)
                    {
                        Console.Clear();
                        Console.WriteLine("");
                        PrintColorMessage(ConsoleColor.Green, "You sunk my battleship!");
                        Console.WriteLine("");
                    }

                    userGuesses--;

                }

                PrintGrid(grid, true);
                Console.WriteLine("");

                isGameOver = StartGame("Gameover! Would you like to play again? [Y or N]: ");

            }

        }

        static void GameInformation()
        {
            PrintColorMessage(ConsoleColor.Cyan, "Welcome to Battleship by Bryan Herrera");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("- You have 8 tries to sink my single battleship.");
            Console.WriteLine("- You will choose a (x,y) coordinate between 0 and 9 to fire at.");
            Console.WriteLine("- 5 successful hits will sink my battleship.");
            Console.WriteLine("");
            Console.WriteLine("");
        }

        static bool StartGame(string message)
        {
            Console.Write(message);
            string answer = Console.ReadLine().ToUpper();
            bool isGameOver = false;
            if (answer != "Y")
            {
                isGameOver = true;
            }

            return isGameOver;
        }

        static void PrintColorMessage(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static int[,] RandomizeShipLocation(string[,] grid)
        {
            Random random = new Random();

            int originX = random.Next(0, 9);
            int originY = random.Next(0, 9);

            string[] orientation = { "horizontal", "vertical" };
            string randomBoatOrientation = orientation[random.Next(orientation.Length)];

            int[,] boatCoordinates = new int[5, 2];
            if (randomBoatOrientation == "horizontal")
            {
                if (originX > 5)
                {
                    originX -= 4;
                }
                boatCoordinates[0, 0] = originY;
                boatCoordinates[0, 1] = originX;
                for (int i = 1; i < boatCoordinates.GetLength(0); i++)
                {
                    boatCoordinates[i, 0] = originY;
                    boatCoordinates[i, 1] = originX + i;
                }
            }
            else if (randomBoatOrientation == "vertical")
            {
                if (originY > 5)
                {
                    originY -= 4;
                }
                boatCoordinates[0, 0] = originY;
                boatCoordinates[0, 1] = originX;
                for (int i = 1; i < boatCoordinates.GetLength(0); i++)
                {
                    boatCoordinates[i, 0] = originY + i;
                    boatCoordinates[i, 1] = originX;
                }
            }

            for(int i = 0; i < boatCoordinates.GetLength(0); i++)
            {
                grid[boatCoordinates[i, 0], boatCoordinates[i, 1]] = "0";
            }

            return boatCoordinates;
        }

        static void PrintGrid(string[,] grid, bool showShipLocation)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                string row = "";
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == "/")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" /");
                        Console.ResetColor();
                    }
                    else if (grid[i, j] == "X")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" X");
                        Console.ResetColor();
                    }
                    else if (grid[i, j] == "0" && showShipLocation)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" O");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(" O");
                    }
                }
                Console.WriteLine(row);
            }
        }

        static int ValidateInput(string message)
        {
            bool isValid = false;
            string input;
            int userGuess = 0;
            while (!isValid)
            {
                Console.Write(message);
                input = Console.ReadLine();

                if (!int.TryParse(input, out userGuess) || Int32.Parse(input) < 0 || Int32.Parse(input) > 9 || input == "")
                {
                    Console.WriteLine("");
                    PrintColorMessage(ConsoleColor.Red, "Please enter a number between 0 and 9.");
                    Console.WriteLine("");
                    continue;
                }

                isValid = true;
            }
           
            Console.WriteLine("");
            return userGuess;
        }
    }
}
