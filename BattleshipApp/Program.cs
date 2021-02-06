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
        }
    }
}
