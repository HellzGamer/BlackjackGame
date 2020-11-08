using System;
using System.Collections.Generic;
using System.Text;

namespace BlackjackLibrary.Models
{
    public class Card
    {
        public Suit Suit { get; set; }
        public RankAndValue Rank { get; set; }
        public int Value { get; set; }


        public Card(Suit suit, RankAndValue rank, int value)
        {
            Suit = suit;
            Rank = rank;
            Value = value;
        }
    }

    public enum Suit
    {
        Hearts,
        Diamonds,
        Spades,
        Clubs
    }

    public enum RankAndValue
    {
        Ace = 1,
        Deuce,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
}
