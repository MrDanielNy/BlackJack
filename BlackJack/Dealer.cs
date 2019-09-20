using System;
using System.Collections.Generic;

namespace BlackJack
{
    public class Dealer
    {
        public int[] DealerValue = { 0, 0 };
        public string[] DealerHand = { "", "", "", "", "" };

        public Dealer()
        {
        }

        /// <summary>
        /// The 'AI' of the dealer. He will play until he reaches 17 or more.
        /// </summary>
        /// <param name="deckOfCards">Current list of cards in carddeck</param>
        /// <param name="playersList">Current list of players in game</param>
        public void HitOrStand(List<Card> deckOfCards, List<Player> playersList)
        {
            int deal = 2;
            while (true)
            {
                deal++;
                Display.DisplayDealer(this, true);
                Display.DisplayPlayers(playersList.Count, playersList);
                //Check if winner or should stop
                if (DealerValue[0] >= 17 || (DealerValue[1] >= 17 && DealerValue[1] <22))
                {
                    //To shorten the need for comparison later, let DealerValue[0] be the same as DealerValue[1] as
                    //long as that value is not bigger than 21.
                    if(DealerValue[1] >= 17 && DealerValue[1] < 22)
                    {
                        DealerValue[0] = DealerValue[1];
                    }
                    break;
                }
                else
                {
                    //Get a new card
                    int randCard = Card.RandomCardFromDeck(deckOfCards.Count);
                    Card card = deckOfCards[randCard];
                    deckOfCards.RemoveAt(randCard);
                    Card.ShowDrawnCard(card);
                    string tempCardString = card.fullCardName;
                    DealerHand[deal] = tempCardString;

                    if (card.value == 1) //If we got an ace
                    {
                        DealerValue[0] += 1;
                        DealerValue[1] += 11;
                    } else
                    {
                        DealerValue[0] += card.value;
                        DealerValue[1] += card.value;
                    }

                    //If we have over 21.
                    if(DealerValue[0] > 21)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Dealer busted!!");
                        break;
                    }

                    //If we got 'Charlie Black Jack', meaning we have five cards on our hand, we automaticly have 21.
                    if (deal >= 5 && DealerValue[0] < 22 && DealerValue[1] < 22) 
                    {
                        DealerValue[0] = 21;
                        DealerValue[1] = 21;
                        Console.WriteLine();
                        Console.Write("Dealer got Charlie Black Jack!! Press enter to continue");
                        Console.ReadLine();
                        break;
                    }
                }

            }

        }
    }
}
