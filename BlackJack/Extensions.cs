using System;
using System.Collections.Generic;

namespace BlackJack
{
    public static class Extensions
    {
        public static void Shuffle<Card>(this IList<Card> cardDeck)
        {
            Random rng = new Random();

            int n = cardDeck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = cardDeck[k];
                cardDeck[k] = cardDeck[n];
                cardDeck[n] = value;
            }
        }
    }
}
