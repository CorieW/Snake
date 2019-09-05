using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    class Program
    {
        static int xSize = 15;
        static int ySize = 15;

        static void Main(string[] args)
        {
            while (true)
            {

                Console.SetWindowSize((xSize + 2) * 2, ySize + 5);

                Console.WriteLine("Press any key to play!");
                Console.ReadKey();
                Console.Clear();

                while (true)
                {
                    SnakeGame();

                    // Game over
                    Console.WriteLine("Wow you're bad at Snake lmao");
                    System.Threading.Thread.Sleep(350);
                    Console.SetWindowSize((xSize + 2) * 2, ySize + 6);
                    Console.WriteLine("Press any key to play again!");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void SnakeGame()
        {
            List<Vector2> snakePieces = new List<Vector2>()
                {
                    new Vector2(5, 4),
                    new Vector2(4, 4),
                    new Vector2(3, 4)
                };

            ConsoleKey keyActive = ConsoleKey.RightArrow;

            Vector2 food = new Vector2(-1, -1);
            while (true)
            {
                // Controlling
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (userInput.Key == ConsoleKey.RightArrow && keyActive != ConsoleKey.LeftArrow && keyActive != ConsoleKey.RightArrow)
                        keyActive = ConsoleKey.RightArrow;
                    else if (userInput.Key == ConsoleKey.LeftArrow && keyActive != ConsoleKey.LeftArrow && keyActive != ConsoleKey.RightArrow)
                        keyActive = ConsoleKey.LeftArrow;
                    else if (userInput.Key == ConsoleKey.UpArrow && keyActive != ConsoleKey.UpArrow && keyActive != ConsoleKey.DownArrow)
                        keyActive = ConsoleKey.UpArrow;
                    else if (userInput.Key == ConsoleKey.DownArrow && keyActive != ConsoleKey.UpArrow && keyActive != ConsoleKey.DownArrow)
                        keyActive = ConsoleKey.DownArrow;
                    else if (userInput.Key == ConsoleKey.Escape) // Pause game
                    {
                        while (true)
                        {
                            bool shouldContinue = PauseMenu();
                            if (shouldContinue) // Should continue game
                            {
                                break;
                            }
                        }
                    }
                    Console.Clear();
                }

                // Checking whether snake ate food and if so respawning food randomly
                if (snakePieces[0].Equals(food) || food.Equals(new Vector2(-1, -1)))
                {
                    if (snakePieces[0].Equals(food))
                    {
                        snakePieces.Add(new Vector2(snakePieces[snakePieces.Count - 1].x, snakePieces[snakePieces.Count - 1].y));
                    }

                    while (true)
                    {
                        int randX = new Random().Next(1, xSize + 1);
                        System.Threading.Thread.Sleep(10);
                        int randY = new Random().Next(1, ySize + 1);
                        food = new Vector2(randX, randY);

                        if (!snakePieces.Contains(food))
                        {
                            break;
                        }
                    }
                }

                // Points
                Console.WriteLine("Points: " + (snakePieces.Count - 3));

                // Displaying the game
                for (int y = 0; y < ySize + 2; y++)
                {
                    for (int x = 0; x < xSize + 2; x++)
                    {
                        if (x == 0 || y == 0 || x == xSize + 1 || y == ySize + 1)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("■ ");
                        }
                        else if (snakePieces[0].Equals(new Vector2(x, y)))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("O ");
                        }
                        else if (snakePieces.Contains(new Vector2(x, y)))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("o ");
                        }
                        else if (food.Equals(new Vector2(x, y)))
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("8 ");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("  ");
                        }
                    }
                    Console.WriteLine();
                }

                // Following of snake body
                for (int i = snakePieces.Count - 1; i > 0; i--)
                {
                    snakePieces[i] = snakePieces[i - 1];
                }

                // Movement of snake
                switch (keyActive)
                {
                    case ConsoleKey.RightArrow:
                        snakePieces[0] = new Vector2(snakePieces[0].x + 1, snakePieces[0].y);
                        break;
                    case ConsoleKey.LeftArrow:
                        snakePieces[0] = new Vector2(snakePieces[0].x - 1, snakePieces[0].y);
                        break;
                    case ConsoleKey.UpArrow:
                        snakePieces[0] = new Vector2(snakePieces[0].x, snakePieces[0].y - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        snakePieces[0] = new Vector2(snakePieces[0].x, snakePieces[0].y + 1);
                        break;
                }


                // Death
                // Out of boundaries
                if (snakePieces[0].x > xSize || snakePieces[0].x < 1 || snakePieces[0].y > ySize || snakePieces[0].y < 1)
                {
                    break;
                }
                // Bump into body
                bool lose = false;
                for (int i = 1; i < snakePieces.Count; i++)
                {
                    if (snakePieces[i].Equals(snakePieces[0]))
                    {
                        lose = true;
                        break;
                    }
                }
                if (lose)
                {
                    break;
                }

                // Delay
                System.Threading.Thread.Sleep(250);
                Console.Clear();

            }
        }

        /// <summary>
        /// Display the pause menu.
        /// </summary>
        /// <returns>The pause menu.</returns>
        static bool PauseMenu()
        { // DON'T MAKE CHANGES, THIS IS VERY SPECIFIC CODE FOR IT'S DESIGN AND PURPOSE
            Console.Clear();
            for(int y = 0; y < ySize + 4; y++)
            {
                string sentence = " Press 'ESCAPE' to continue! ";

                for (int x = 0; x < xSize + 2; x++)
                {
                    if (x == 0 || y == 0 || x == xSize+1 || y == ySize+3)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("■ ");
                    }
                    else if (y == 8 && x < 15)
                    {
                        Console.Write(sentence[(x)*2 -2].ToString() + sentence[(x)*2 - 1].ToString());
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }
            ConsoleKey userInput = Console.ReadKey().Key;
            if (userInput == ConsoleKey.Escape)
            {
                return true;
            }
            return false;
        }
    }
}
