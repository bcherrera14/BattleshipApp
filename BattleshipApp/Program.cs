using System;

namespace BattleshipApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Greet the user
            PrintColorMessage(ConsoleColor.Cyan, "Welcome to Battleship by Bryan Herrera");
            Console.WriteLine("");
            Console.WriteLine("");

            //Rules of the game
            Console.WriteLine("- You have 8 tries to sink my single battleship.");
            Console.WriteLine("- You will choose a (x,y) coordinate between 0 and 9 to fire at.");
            Console.WriteLine("- 5 successful hits will sink my battleship.");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write("Ready to play? [Y or N]: ");
            string answer = Console.ReadLine().ToUpper();

            if (answer != "Y")
            {
                return;
            }

            //Game loop
            while (true)
            {
                Console.Clear();
                //Create grid
                string[,] grid = new string[10, 10];
                //Randomize boat location
                int[,] boatCoordinates = RandomizeShipLocation(grid);
                

                //Clear colsole before game starts
                Console.Clear();
                PrintColorMessage(ConsoleColor.Magenta, "Fire Away!");
                Console.WriteLine("");

                //Game Logic
                int userGuesses = 8;
                int directHit = 0;

                while (userGuesses > 0)
                {
                    //ShowShipLocation(grid, boatCoordinates);
                    PrintGrid(grid);
                    Console.WriteLine("");

                    //Show how many attempts left
                    PrintColorMessage(ConsoleColor.Cyan, "Remaining guesses: " + userGuesses);
                    Console.WriteLine("");

                    //Get user X
                    Console.Write("Enter X Coordinate: ");
                    string inputX = Console.ReadLine();

                    int guessX = 0;
                    //Validate X input
                    if (!int.TryParse(inputX, out guessX) || Int32.Parse(inputX) < 0 || Int32.Parse(inputX) > 9 || inputX == "")
                    {
                        Console.Clear();
                        PrintColorMessage(ConsoleColor.Red, "Please enter a number between 0 and 9.");
                        Console.WriteLine("");
                        continue;
                    }

                    Console.WriteLine("");

                    //Get user Y
                    Console.Write("Enter Y Coordinate: ");
                    string inputY = Console.ReadLine();

                    int guessY = 0;
                    //Validate Y input
                    if (!int.TryParse(inputY, out guessY) || Int32.Parse(inputY) < 0 || Int32.Parse(inputY) > 9 || inputY == "")
                    {
                        Console.Clear();
                        PrintColorMessage(ConsoleColor.Red, "Please enter a number between 0 and 9.");
                        Console.WriteLine("");
                        continue;
                    }

                    Console.Clear();

                    //Reply hit or miss
                    for (int i = 0; i < boatCoordinates.GetLength(0); i++)
                    {
                        if (boatCoordinates[i, 1] == guessX && boatCoordinates[i, 0] == guessY)
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
                            grid[guessY, guessX] = "/";
                        }
                    }

                    //If user hits ship 5 times
                    if (directHit == 5)
                    {
                        PrintColorMessage(ConsoleColor.Magenta, "You sunk my battleship!");
                        break;
                    }

                    //Decrement user guesses
                    userGuesses--;

                }

                //Show Game Result
                //PrintGrid(grid);
                ShowShipLocation(grid, boatCoordinates);
                Console.WriteLine("");

                //Ask to replay game
                PrintColorMessage(ConsoleColor.Magenta, "Gameover! Would you like to play again? [Y or N]");
                //Get the answer
                string replay = Console.ReadLine().ToUpper();

                if (replay == "Y")
                {
                    continue;
                }
                else if (replay == "N")
                {
                    return;
                }
                else
                {
                    return;
                }

            }

        }
        //Pring color message

        static void PrintColorMessage(ConsoleColor color, string message)
        {
            //Change text color
            Console.ForegroundColor = color;
            //Tell user its not a number
            Console.WriteLine(message);
            //Reset text color
            Console.ResetColor();
        }

        static int[,] RandomizeShipLocation(string[,] grid)
        {
            //Create a new random object
            Random random = new Random();

            //Init random boat coordinates
            int originX = random.Next(0, 9);
            int originY = random.Next(0, 9);

            //Random boat orientation
            string[] orientation = { "horizontal", "vertical" };
            string randomBoatOrientation = orientation[random.Next(orientation.Length)];

            //Build boat
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

            //Place boat in grid
            for(int i = 0; i < boatCoordinates.GetLength(0); i++)
            {
                grid[boatCoordinates[i, 0], boatCoordinates[i, 1]] = "0";
            }

            return boatCoordinates;
        }

        static void PrintGrid(string[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                string row = "";
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == "/")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" / ");
                        Console.ResetColor();
                    }
                    else if (grid[i, j] == "X")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" X ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(" O ");
                    }
                }
                Console.WriteLine(row);
            }
        }

        static void ShowShipLocation(string[,] grid, int[,] boatCoordinates)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                string row = "";
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == "/")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" / ");
                        Console.ResetColor();
                    }
                    else if (grid[i, j] == "X")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" X ");
                        Console.ResetColor();
                    }
                    else if (grid[i, j] == "0")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" O ");
                        Console.ResetColor();
                    }
                    else
                    {
                      Console.Write(" O ");
                    }

                    
                }
                Console.WriteLine(row);
            }
        }

    }
}
