using BlackjackLibrary.Interfaces;
using BlackjackLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackjackLibrary.Logic
{
    public class BlackJackGameLogic : IGame
    {
        public List<Card> Deck;
        static Random random = new Random();
        public int PlayerScore;
        public bool GameStillInPlay = true;
        public BlackJackGameLogic()
        {
            GetDeck();
        }

        public void ShuffleDeck()
        {
            for (int i = Deck.Count - 1; i > 0; i--)
            {
                int x = random.Next(i + 1);
                var t = Deck[i];
                Deck[i] = Deck[x];
                Deck[x] = t;
            }
        }

        public void PlayerPlays()
        {
            var currentCard = Deck[0];
            Deck.RemoveAt(0);
            AddTotalScore(currentCard);
        }

        public void DealerPlays()
        {
            if (PlayerScore > 0)
            {
                Console.WriteLine($"Dealer playing...");
                var playerStillInGame = PlayerScore <= 21 ? true : false;
                var totalScore = 0;
                for (int i = 0; i < Deck.Count; i++)
                {
                    totalScore += Deck[i].Value;
                    Console.WriteLine($"Drew: {Deck[i].Rank} {Deck[i].Suit}. Dealer scored: {totalScore}");
                    if (totalScore >= 17 && totalScore <= 21)
                    {
                        if (!playerStillInGame)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Player Bust. You Lose! Dealer scored: {totalScore}. Player scored: {PlayerScore}");
                        }
                        else if (totalScore > PlayerScore)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"You Lose! Dealer scored: {totalScore}. Player scored: {PlayerScore}");
                        }
                        else if (PlayerScore > totalScore)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Player Wins!!! Dealer scored: {totalScore}. Player scored: {PlayerScore}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"Draw Game! Dealer scored: {totalScore}. Player Scored: {PlayerScore}");
                        }
                        break;
                    }
                    else if (totalScore > 21)
                    {
                        if (playerStillInGame)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Dealer Bust! Dealer scored: {totalScore}. Player scored: {PlayerScore}. Player wins!");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"Player and Dealer Bust! Dealer scored: {totalScore}. Player scored: {PlayerScore}. Draw Game!");
                        }
                        break;
                    }
                }
                GameStillInPlay = false;
            }
            else
            {
                Console.WriteLine("Game not started. Player to go first!");
            }
        }

        private void GetDeck()
        {
            IEnumerable<Suit> suits = Enum.GetValues(typeof(Suit)).Cast<Suit>();
            IEnumerable<RankAndValue> rankAndValues = Enum.GetValues(typeof(RankAndValue)).Cast<RankAndValue>();
            Deck = (from rv in rankAndValues from s in suits select new Card(s, rv, (int)rv)).ToList();
            for (int i = 0; i < Deck.ToList().Count; i++)
            {
                if (Deck[i].Rank == RankAndValue.Jack || Deck[i].Rank == RankAndValue.Queen || Deck[i].Rank == RankAndValue.King)
                {
                    Deck[i].Value = 10;
                }
            }
        }

        private void AddTotalScore(Card card)
        {
            if (card != null)
            {
                PlayerScore += card.Value;
                Console.WriteLine($"Drew: {card.Rank} {card.Suit}. Player scored: {PlayerScore}");
            }
            if (PlayerScore > 21)
            {
                DealerPlays();
            }
        }
    }
}