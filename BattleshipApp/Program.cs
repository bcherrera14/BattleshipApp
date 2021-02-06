using System;

namespace BattleshipApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string appName = "Battleship Console App";
            string author = "Bryan Herrera";
            //Change text color
            Console.ForegroundColor = ConsoleColor.Cyan;
            //App title
            Console.WriteLine("{0} by {1}", appName, author);
            //Reset text color
            Console.ResetColor();

            //Create grid
            string[,] grid = new string[10, 10] { { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" }, { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" }, { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" }, { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" }, { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" }, { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" }, { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" }, { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" }, { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" }, { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" } };

            //Create a new random object
            Random random = new Random();
           
            //Init random boat coordinates
            int originX = random.Next(0, 9);

            int originY = random.Next(0, 9);

            Console.WriteLine("Boat Origin: X:{0} Y:{1}", originX, originY);

            //Random boat orientation
            string[] orientation = { "horizontal", "vertical" };
            string randomBoatOrientation = orientation[random.Next(orientation.Length)];
            Console.WriteLine("Orientation: {0}", randomBoatOrientation);

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
            for (int i = 0; i < boatCoordinates.GetLength(0); i++)
            {
                grid[boatCoordinates[i, 0], boatCoordinates[i, 1]] = "X";
            }
            
            //Print grid
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                string row = "";
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    row += grid[i, j] + " ";
                }
                Console.WriteLine(row);
            }

            // GAME LOGIC //
            int userGuesses = 0;
            int directHit = 0;
            while (userGuesses < 8) {
                //Get user X guess coordinate
                Console.WriteLine("Enter X Coordinate: ");
                int guessX = 0;
                string inputX = Console.ReadLine();

                //Validate X input
                if(!int.TryParse(inputX, out guessX) || Int32.Parse(inputX) < 0 || Int32.Parse(inputX) > 9 || inputX == "")
                {
                    //Print error message
                    Console.WriteLine("Please enter a number between 0 and 9.");
                    continue;
                }
                guessX = Int32.Parse(inputX);

                //Get user Y guess coordinate
                Console.WriteLine("Enter Y Coordinate: ");
                int guessY = 0;
                string inputY = Console.ReadLine();
                //Validate Y input
                if (!int.TryParse(inputY, out guessY) || Int32.Parse(inputY) < 0 || Int32.Parse(inputY) > 9 || inputY == "")
                {
                    //Print error message
                    Console.WriteLine("Please enter a number between 0 and 9.");
                    continue;
                }
                guessY = Int32.Parse(inputY);
                //Print user guess coordiante
                Console.WriteLine("You guessed: " + inputX + " " + inputY);

                //Reply hit or miss
                for (int i = 0; i < boatCoordinates.GetLength(0); i++)
                {
                    if (boatCoordinates[i, 1] == guessX && boatCoordinates[i, 0] == guessY)
                    {
                        Console.WriteLine("Direct Hit!");
                        directHit++;
                        break;
                    }
                    if (i == 4)
                    {
                        Console.WriteLine("Miss!");
                    }
                }
                //If user hits ship 5 times
                if (directHit == 5)
                {
                    Console.WriteLine("You Sunk My Battleship!");
                    break;
                }

                //Increment user guesses
                userGuesses++;
            }
        }
    }
}
