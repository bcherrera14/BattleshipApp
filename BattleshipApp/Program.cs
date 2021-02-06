using System;

namespace BattleshipApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintColorMessage(ConsoleColor.Cyan, "Battleship by Bryan Herrera");

            //Create grid
            string[,] grid = new string[10, 10] { { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" }, { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" }, { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" }, { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" }, { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" }, { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" }, { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" }, { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" }, { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" }, { "O", "O", "O", "O", "O", "O", "O", "O", "O", "O" } };

            int[,] boatCoordinates = RandomizeShipLocation(grid);
            
            

            // GAME LOGIC //
            int userGuesses = 0;
            int directHit = 0;
            while (userGuesses < 8) {
                Console.WriteLine("");
                //Print grid
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    string row = "";
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        //row += grid[i, j] + " ";
                        if(grid[i,j] == "/")
                        {
                            //Change text color
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            //Tell user its not a number
                            Console.Write("/ ");
                            //Reset text color
                            Console.ResetColor();
                        }else if (grid[i, j] == "X")
                        {
                            //Change text color
                            Console.ForegroundColor = ConsoleColor.Red;
                            //Tell user its not a number
                            Console.Write("X ");
                            //Reset text color
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(grid[i, j] + " ");
                        }
                    }
                    Console.WriteLine(row);
                }
                Console.WriteLine("");


                //Get user X guess coordinate
                Console.WriteLine("Enter X Coordinate: ");
                string inputX = Console.ReadLine();
                
                int guessX = 0;
                //Validate X input
                if (!int.TryParse(inputX, out guessX) || Int32.Parse(inputX) < 0 || Int32.Parse(inputX) > 9 || inputX == "")
                {
                    //Print error message
                    //Console.WriteLine("Please enter a number between 0 and 9.");
                    PrintColorMessage(ConsoleColor.Red, "Please enter a number between 0 and 9.");
                    continue;
                }

                Console.WriteLine("");
                //Get user Y guess coordinate
                Console.WriteLine("Enter Y Coordinate: ");
                string inputY = Console.ReadLine();

                int guessY = 0;
                //Validate Y input
                if (!int.TryParse(inputY, out guessY) || Int32.Parse(inputY) < 0 || Int32.Parse(inputY) > 9 || inputY == "")
                {
                    //Print error message
                    //Console.WriteLine("Please enter a number between 0 and 9.");
                    PrintColorMessage(ConsoleColor.Red, "Please enter a number between 0 and 9.");
                    continue;
                }

                //Print user guess coordiante
                //Console.WriteLine("You guessed: " + inputX + " " + inputY);

                Console.WriteLine("");
                //Reply hit or miss
                for (int i = 0; i < boatCoordinates.GetLength(0); i++)
                {
                    if (boatCoordinates[i, 1] == guessX && boatCoordinates[i, 0] == guessY)
                    {
                        //Console.WriteLine("Direct Hit!");
                        PrintColorMessage(ConsoleColor.Green, "Direct Hit!");
                        grid[boatCoordinates[i, 0], boatCoordinates[i, 1]] = "X";
                        directHit++;
                        break;
                    }
                    if (i == 4)
                    {
                        //Console.WriteLine("Miss!");
                        PrintColorMessage(ConsoleColor.Red, "Missed!");
                        grid[guessY, guessX] = "/";
                    }
                }
                //If user hits ship 5 times
                if (directHit == 5)
                {
                    //Console.WriteLine("You Sunk My Battleship!");
                    Console.WriteLine("");
                    //Print grid
                    for (int i = 0; i < grid.GetLength(0); i++)
                    {
                        string row = "";
                        for (int j = 0; j < grid.GetLength(1); j++)
                        {
                            //row += grid[i, j] + " ";
                            if (grid[i, j] == "/")
                            {
                                //Change text color
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                //Tell user its not a number
                                Console.Write("/ ");
                                //Reset text color
                                Console.ResetColor();
                            }
                            else if (grid[i, j] == "X")
                            {
                                //Change text color
                                Console.ForegroundColor = ConsoleColor.Red;
                                //Tell user its not a number
                                Console.Write("X ");
                                //Reset text color
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.Write(grid[i, j] + " ");
                            }
                        }
                        Console.WriteLine(row);
                    }
                    Console.WriteLine("");
                    PrintColorMessage(ConsoleColor.Yellow, "You sunk my battleship!");
                    break;
                }

                //Increment user guesses
                userGuesses++;
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

            //Console.WriteLine("Boat Origin: X:{0} Y:{1}", originX, originY);

            //Random boat orientation
            string[] orientation = { "horizontal", "vertical" };
            string randomBoatOrientation = orientation[random.Next(orientation.Length)];
            //Console.WriteLine("Orientation: {0}", randomBoatOrientation);

            //Build boat
            int[,] boatCoordinates = new int[5, 2];
            if (randomBoatOrientation == "horizontal")
            {
                if (originX > 5)
                {
                    originX = originX - 4;
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
                    originY = originY - 4;
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
            //for (int i = 0; i < boatCoordinates.GetLength(0); i++)
            //{
            //    grid[boatCoordinates[i, 0], boatCoordinates[i, 1]] = "X";
            //}
            return boatCoordinates;
        }

        static int ValidateInput(string input)
        {
            int number;
            //Validate X input
            if (!int.TryParse(input, out number) || Int32.Parse(input) < 0 || Int32.Parse(input) > 9 || input == "")
            {
                //Print error message
                //Console.WriteLine("Please enter a number between 0 and 9.");
                PrintColorMessage(ConsoleColor.Red, "Please enter a number between 0 and 9.");
                //continue;
            }
            return number;
        }

    }
}
