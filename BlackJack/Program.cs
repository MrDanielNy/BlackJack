using System;
using System.Collections.Generic;

//***Built by "The Green Group" for a class in TUC-Sweden school***
//In Alphabetical order of the last name, Abraham Eishow, Alexander Holm, Daniel Ny, Mikael Sundqvist, Jonas Tärnemark.

namespace BlackJack
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            List<Player> players = new List<Player>();
            List<Card> deckOfCards = new List<Card>();

            //Display welcomescreen
            Display.DisplayWelcomeScreen();

            //Get nr of players
            int nrOfPlayers = Player.PickNumberOfPlayers();

            // Assign name to players
            players = Player.NamePlayers(nrOfPlayers);

            bool isPlayingGame = true;
            while (isPlayingGame)
            {
                //Create a new deck of cards
                deckOfCards = Card.CreateCardDeck();

                //Create a Dealer object
                Dealer dealer = new Dealer();

                int deal = 0;

                //Pick cards
                NewDeal(players, deckOfCards, dealer, deal);

                //Let players do their rounds.
                Player.HitOrStand(deckOfCards, players, dealer);

                //Let dealer play the round
                dealer.HitOrStand(deckOfCards, players);

                //Check and display winners
                DisplayWinners(players, nrOfPlayers, dealer);

                //Check if player wants to Quit or Continue
                while (true)
                {
                    Console.WriteLine("Press Y to play another round or N to exit game.");
                    string playerChoice = Console.ReadLine();
                    if (playerChoice.ToLower() == "y") //Play another round.
                    {
                        Player.ResetPlayer(players, nrOfPlayers);

                        //Display welcomescreen
                        Display.DisplayWelcomeScreen();
                        break;
                    }
                    else if (playerChoice.ToLower() == "n") //Time to quit game
                    {
                        Console.WriteLine("Thank you for playing Black Jack!");
                        isPlayingGame = false;
                        System.Threading.Thread.Sleep(500);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Check to see who has won
        /// </summary>
        /// <param name="players">List of players in game</param>
        /// <param name="dealer">The Dealer object</param>
        private static void DisplayWinners(List<Player> players, int nrOfPlayers, Dealer dealer)
        {
            Display.DisplayDealer(dealer, true);
            Display.DisplayPlayers(nrOfPlayers, players);

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].CurrentValue[0] >21 )
                {
                    Console.WriteLine($"{players[i].PlayerName} got over 21!");
                } else if ((players[i].CurrentValue[0] <= 21 && players[i].CurrentValue[0] > dealer.DealerValue[0]) || (players[i].CurrentValue[0] <= 21 && dealer.DealerValue[0] >21)  )
                {
                    Console.WriteLine($"{players[i].PlayerName} Wins!");
                } else if ((players[i].CurrentValue[0] < 21 && players[i].CurrentValue[0] < dealer.DealerValue[0]) || players[i].CurrentValue[0] > 21)
                {
                    Console.WriteLine($"{players[i].PlayerName} Lose!");
                } else if (players[i].CurrentValue[0] == dealer.DealerValue[0])
                {
                    Console.WriteLine($"{players[i].PlayerName} Draw!");
                }
            }
            

        }

        /// <summary>
        /// Give all the players and the dealer one card each.
        /// </summary>
        /// <param name="playersList">List of players in game</param>
        /// <param name="deckOfCards">The current list of cards</param>
        /// <param name="dealer">The Dealer object</param>
        /// <param name="deal">What deal we are on</param>
        private static void NewDeal(List<Player> playersList, List<Card> deckOfCards, Dealer dealer, int deal)
        {
            Display.DisplayDealer(dealer, false);
    
            Display.DisplayPlayers(playersList.Count, playersList);

            for (deal = 0; deal < 2; deal++)
            {
                DrawPlayersCards(playersList.Count, playersList, deckOfCards, deal);
    
                DrawDealersCard(dealer, deal, deckOfCards);
    
                Display.DisplayPlayers(playersList.Count, playersList);
            }
        }
        
        /// <summary>
        /// Draw a card from the carddeck to the dealer
        /// </summary>
        /// <param name="dealer">DealerObject</param>
        /// <param name="deal">What deal we are on</param>
        /// <param name="deckOfCards">List of cards available</param>
        private static void DrawDealersCard(Dealer dealer, int deal, List<Card> deckOfCards)
        {
            Console.Clear();
            int randCard = Card.RandomCardFromDeck(deckOfCards.Count);
            Card card = deckOfCards[randCard];
            deckOfCards.RemoveAt(randCard);
            dealer.DealerHand[deal] = card.fullCardName;
            if (card.value == 1)
            {
                dealer.DealerValue[0] += 1;
                dealer.DealerValue[1] += 11;
            }
            else
            {
                dealer.DealerValue[0] += card.value;
                dealer.DealerValue[1] += card.value;
            }
    
            Display.DisplayDealer(dealer, false);
    
        }

        /// <summary>
        /// Draw a card from the carddeck to the dealer
        /// </summary>
        /// <param name="numberOfPlayers">How many players there are</param>
        /// <param name="playersList">List of players</param>
        /// <param name="deckOfCards">List of cards available</param>
        /// <param name="deal">What deal we are on</param>
        private static void DrawPlayersCards(int numberOfPlayers, List<Player> playersList, List<Card> deckOfCards, int deal)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                int randCard = Card.RandomCardFromDeck(deckOfCards.Count);
                Card card = deckOfCards[randCard];
                deckOfCards.RemoveAt(randCard);
                //string tempCardString = card.cardName + " of " + card.suit;
                string tempCardString = card.fullCardName;
                playersList[i].CurrentHand[deal] = tempCardString;
                Card.ShowDrawnCard(card);
                if (card.value == 1)
                {
                    playersList[i].CurrentValue[0] += 1;
                    playersList[i].CurrentValue[1] += 11;
                }
                else
                {
                    playersList[i].CurrentValue[0] += card.value;
                    playersList[i].CurrentValue[1] += card.value;
                }
                if (i < numberOfPlayers - 1)
                {
                    Console.Write("\t\t");
                }
    
            }
        }
    }
}
