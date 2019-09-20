using System;
using System.Collections.Generic;

namespace BlackJack
{
    public enum Suit { Hearts, Spades, Clubs, Diamonds }; //Heart, Spade, Clubb, Diamonds
    public enum CardName { Ace=1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King };

    public class Card
    {
        public string suit; 
        public int cardNumber; //1=Ace, 11=Jack, 12=Queen, 13=King
        public int value; //Ace = 1 or 11, Jack, Queen, King = 10
        public string cardName;
        public string fullCardName;

        public Card()
        {

        }

        /// <summary>
        /// Create a new carddeck with all 52 cards
        /// </summary>
        /// <returns>Returns a list of cards</returns>
        public static List<Card> CreateCardDeck()
        {
            List<Card> tempCardDeck = new List<Card>();

            for(int i=0;i<4;i++)
            {
                for(int cardNr=1;cardNr<=13;cardNr++)
                {
                    Card tempCard = new Card();

                    tempCard.suit = Enum.GetName(typeof(Suit), i);
                    tempCard.cardNumber = cardNr;

                    if (cardNr <= 10)
                        tempCard.value = cardNr;
                    else
                        tempCard.value = 10;

                    tempCard.cardName = Enum.GetName(typeof(CardName), cardNr);

                    tempCardDeck.Add(tempCard);
                    tempCard.fullCardName = $"{tempCard.cardName} of {tempCard.suit}";
                    if(tempCard.fullCardName.Length < 20)
                    {
                        for(int index=0;i<20-tempCard.fullCardName.Length;index++)
                        {
                            tempCard.fullCardName += " ";
                        }
                    }
                    Console.WriteLine($"{tempCard.fullCardName} with value {tempCard.value}");
                    int nrOfLetters = tempCard.fullCardName.Length;
                }
            }

            return tempCardDeck;
        }

        /// <summary>
        /// Shows the card
        /// </summary>
        /// <param name="card"></param>
        public static void ShowDrawnCard(Card card)
        {
            //Console.Write($"{card.cardName} of {card.suit}");
            Console.Write($"{card.fullCardName}");
            System.Threading.Thread.Sleep(250);
        }

        /// <summary>
        /// Picks a random card from whats left of the deck.
        /// </summary>
        /// <param name="cardsLeft"></param>
        /// <returns>The cards position in the deck.</returns>
        public static int RandomCardFromDeck(int cardsLeft)
        {
            Random rnd = new Random();
            int randomCard = rnd.Next(0, cardsLeft);
            return randomCard;
        }
    }
}

