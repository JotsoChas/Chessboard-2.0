using System;
using System.Text;
using System.Collections.Generic;

namespace Chessboard
{
    internal class Program          // ◼︎  ◻ 
    {
        static void Main(string[] args)
        {
            // Makes sure the symbols ◻ ◼ are shown correctly
            Console.OutputEncoding = Encoding.Unicode;

            bool again = true; // Solves if user wants to play again

            while (again)           //While loop, Play again? true/false
            {
                ShowTitle(); // Show title box  headline

                int size = ReadSize();   // Ask for board size
                RenderBoard(size);       // Draw the board

                // Ask if user wants to make a new board
                Console.WriteLine("\nWould you like to make a new one? (y/n)\n");
                string answer = Console.ReadLine()?.ToLower() ?? "";        //conv Y YES YeS etc to small char

                // If user says yes, start again
                if (answer == "y" || answer == "yes" || answer == "ja")
                {
                    ShowTitle();
                    continue; // Go back to start of loop
                }
                else
                {
                    again = false; // End the loop if anything but yes is the input
                }
            }

            // Exit message
            Console.Clear(); // Clear screen
            Console.WriteLine("|============================|");
            Console.WriteLine("|== THANK YOU FOR PLAYING! ==|"); 
            Console.WriteLine("|==      SEE YOU SOON!     ==|");
            Console.WriteLine("|============================|");
        }

       
        static void ShowTitle()
        {
            Console.Clear(); // Clear screen
            Console.WriteLine("|==================================|");
            Console.WriteLine("|== LET'S DESIGN YOUR CHESSBOARD ==|");
            Console.WriteLine("|==================================|");
            Console.WriteLine(); // Empty line after title
        }

        
        static int ReadSize()
        {
            int number;

            while (true)
            {
                Console.WriteLine("Choose a number between 3 - 50\n");
                string input = Console.ReadLine();

                // Try to read text as number 
                int? parsed = ParseNumber(input);

                if (parsed != null)         //om texten kunde tolkas, använd värdet
                    number = parsed.Value;
                else if (!int.TryParse(input, out number))  //om inte det går att omvandla texten till en int
                {
                    Console.WriteLine("Only digits or number words allowed. Please try again.\n");
                    continue;       // Start over if invalid
                }

                // Check if number is in the correct range
                if (number < 3 || number > 50)
                {
                    Console.WriteLine("Please enter a number between 3 and 50.\n");
                    continue;       // Start over if invalid
                }

                // If valid, return number to Main
                return number;
            }
        }


        // Converts text like "thirtyfive" to number. Big help from AI!
        static int? ParseNumber(string input) //
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            // Normalize input,makes everything to lowercase
            input = input.ToLower().Replace("-", "").Replace(" ", "");

            // Try parsing direct digits, if input already is a string number
            if (int.TryParse(input, out int number))
                return number;

            // Base number words, Dictionary wich "ataches" 2 types, a string and a int.
            var ones = new Dictionary<string, int>()
    {
        { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 },
        { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 },
        { "ten", 10 }, { "eleven", 11 }, { "twelve", 12 }, { "thirteen", 13 },
        { "fourteen", 14 }, { "fifteen", 15 }, { "sixteen", 16 },
        { "seventeen", 17 }, { "eighteen", 18 }, { "nineteen", 19 }
    };

            var tens = new Dictionary<string, int>()
    {
        { "twenty", 20 }, { "thirty", 30 }, { "forty", 40 }, { "fifty", 50 }
    };

            // Exact match like "twelve" or "thirty"
            if (ones.TryGetValue(input, out int single))
                return single;

            if (tens.TryGetValue(input, out int ten)) //check if can be find in dict 10
                return ten;

            // Combined words like "twentyfive" or "fortyseven" are being merged 
            foreach (var t in tens)
            {
                if (input.StartsWith(t.Key))
                {
                    string rest = input.Substring(t.Key.Length);
                    if (string.IsNullOrEmpty(rest))
                        return t.Value; // e.g. "thirty"

                    if (ones.TryGetValue(rest, out int restValue))
                        return t.Value + restValue; // e.g. "thirtyfive" = 30 + 5
                }
            }

            // No match
            return null;
        }



        // Draws the board
        static void RenderBoard(int n)
        {
            Console.WriteLine();


            // Draw each row
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    // Change between white and black squares
                    string square = ((r + c) % 2 == 0) ? "◻" : "◼";

                    // Keep squares aligned
                    Console.Write($"{square,3}");
                }

                // Go to next line after each row
                Console.WriteLine();
            }
        }
    }
}