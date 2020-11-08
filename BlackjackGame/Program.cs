using BlackjackLibrary.Logic;
using BlackjackLibrary.Models;
using System;
using System.Dynamic;

namespace BlackjackGame
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var gameLogic = new BlackJackGameLogic();
                Console.WriteLine($"Play Mini Blackjack :)");
                gameLogic.ShuffleDeck();
                while (gameLogic.GameStillInPlay)
                {
                    Console.WriteLine("1. Hit");
                    Console.WriteLine("2. Stay");
                    Console.WriteLine("3. Exit Game");
                    var validSelection = int.TryParse(Console.ReadLine(), out int selection);
                    if (!validSelection)
                    {
                        Console.WriteLine("Invalid selection. Try again");
                    }
                    else
                    {
                        switch (selection)
                        {
                            case 1:
                                gameLogic.PlayerPlays();
                                break;
                            case 2:
                                gameLogic.DealerPlays();
                                break;
                            case 3:
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Invalid selection. Try again");
                                break;
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Play Again? y/n");
                if (Console.ReadLine().ToLower() == "y")
                {
                    Main(args);
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured. Please try again");
                Main(args);
            }

        }
    }
}
