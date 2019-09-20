using System;
using System.Collections.Generic;

namespace BlackJack
{
    public class Player
    {
        public string PlayerName { get; set; }
        public string[] CurrentHand = { "", "", "", "", ""};
        public int[] CurrentValue =  { 0, 0 };

        public Player(string playerName)
        {
            PlayerName = playerName;

            //Add twenty letters to each hand so the placing of the cards isn't off.
            for (int currentHandIndex=0;currentHandIndex<5;currentHandIndex++)
            {
                for (int i = 0; i < 20; i++)
                {
                    CurrentHand[currentHandIndex] += " ";
                }
            }
            
        }

        /// <summary>
        /// Name each player.
        /// </summary>
        /// <param name="nrOfPlayers">How many players to add</param>
        /// <returns></returns>
        public static List<Player> NamePlayers(int nrOfPlayers)
        {
            List<Player> tempPlayer = new List<Player>();
            string playerName = "";

            //Add Player objects with names to list of players
            for (int i = 0; i < nrOfPlayers; i++)
            {
                bool hasWrittenName = false;

                //Check to make sure we have at least one character in the name.
                while (hasWrittenName == false)
                {
                    Console.Write("Enter player name, at least one letter: ");
                    playerName = Console.ReadLine();
                    playerName = playerName.Trim();
                    if (playerName.Length >= 1)
                        hasWrittenName = true;
                    else
                        Console.WriteLine("You may not leave the player name empty");
                }

                Player player = new Player(playerName);
                tempPlayer.Add(player);
            }

            return tempPlayer;
        }

        /// <summary>
        /// Decide how many players that will join the game.
        /// </summary>
        /// <returns>Number of players chosen.</returns>
        public static int PickNumberOfPlayers()
        {
            int nrOfPlayers;
            while (true)
            {
                Console.Write("Please enter number of players between 1-6: ");
                string inputNrOfPlayers = Console.ReadLine();
                nrOfPlayers = int.Parse(inputNrOfPlayers);
                if(nrOfPlayers < 1 || nrOfPlayers > 6)
                {
                    Console.WriteLine("You can only choose between 1 and 6 players");
                } else
                {
                    break;
                }
            }
            return nrOfPlayers;
        }

        /// <summary>
        /// Choose between Hit (Taking a new card) or Stand.
        /// </summary>
        /// <param name="deckOfCards">Currently used carddeck</param>
        /// <param name="playersList">List of players in game</param>
        /// <param name="dealer">The Dealer object</param>
        public static void HitOrStand(List<Card> deckOfCards, List<Player> playersList, Dealer dealer)
        {
            for (int i = 0; i < playersList.Count; i++)
            {
                int deal = 2;
                while (true)
                {
                    if(playersList[i].CurrentValue[0] == 21 || playersList[i].CurrentValue[1] == 21)
                    {
                        Console.Write($"{playersList[i].PlayerName}: You got Black Jack! Press enter for next player");
                        Console.ReadLine();
                        break;
                    }

                    if(playersList[i].CurrentValue[0] == playersList[i].CurrentValue[1])
                    {
                        Console.Write($"{playersList[i].PlayerName}: You got {playersList[i].CurrentValue[0]}, do you want to (H)it or (S)tand? ");
                    } else
                    {
                        Console.Write($"{playersList[i].PlayerName}: You got {playersList[i].CurrentValue[0]} / {playersList[i].CurrentValue[1]}, do you want to (H)it or (S)tand? ");
                    }
                    
                    
                    string playerChoise = Console.ReadLine();
                    if (playerChoise.ToLower() == "h")
                    {
                        Console.WriteLine();
                        int randCard = Card.RandomCardFromDeck(deckOfCards.Count);
                        Card card = deckOfCards[randCard];
                        deckOfCards.RemoveAt(randCard);
                        Card.ShowDrawnCard(card);
                        string tempCardString = card.fullCardName;
                        playersList[i].CurrentHand[deal] = tempCardString;
                        deal++;
                        if (card.value == 1) //If card is ace
                        {
                            playersList[i].CurrentValue[0] += 1;
                            playersList[i].CurrentValue[1] += 11;
                        }
                        else
                        {
                            playersList[i].CurrentValue[0] += card.value;
                            playersList[i].CurrentValue[1] += card.value;
                        }
                        if(deal>=5 && playersList[i].CurrentValue[0] < 22 && playersList[i].CurrentValue[1] < 22) //Got Charlie Black Jack, set score to 21
                        {
                            playersList[i].CurrentValue[0] = 21;
                            playersList[i].CurrentValue[1] = 21;
                            Console.WriteLine();
                            Console.Write("You got Charlie Black Jack!! Press enter for next player");
                            Console.ReadLine();
                            break;
                        }
                        if (playersList[i].CurrentValue[0] > 21 && playersList[i].CurrentValue[1] > 21)
                        {
                            //We got over 21
                            Console.WriteLine();
                            Console.WriteLine("You are busted!!");
                            break;
                        } else if(playersList[i].CurrentValue[0] == 21 || playersList[i].CurrentValue[1] == 21)
                        {
                            //We got Black Jack
                            Console.WriteLine();
                            Console.WriteLine("You got Black Jack!! Press enter for next player");
                            Console.ReadLine();
                            break;
                        }
                    }
                    else if (playerChoise.ToLower() == "s")
                    {
                        //Assign the best value to currentvalue[0] for compareson
                        //when we check winners
                        if(playersList[i].CurrentValue[1] <= 21)
                        {
                            playersList[i].CurrentValue[0] = playersList[i].CurrentValue[1];
                        }
                        break;
                    }

                    Display.DisplayBoard(dealer, playersList.Count, playersList, deal);
                }
            }
        }

        /// <summary>
        /// Reset player hand och value if we will be playing another round.
        /// </summary>
        /// <param name="playersList">Current list of players</param>
        public static void ResetPlayer(List<Player> playersList, int numberOfPlayers)
        {
            for(int i=0;i<numberOfPlayers;i++)
            {
                for (int currentHand = 0; currentHand < 5; currentHand++)
                {
                    playersList[i].CurrentHand[currentHand] = "";
                    for (int nrOfSpaces=0; nrOfSpaces < 20; nrOfSpaces++)
                    {
                        playersList[i].CurrentHand[currentHand] += " ";
                    }
                }
                playersList[i].CurrentValue[0] = 0;
                playersList[i].CurrentValue[1] = 0;
            }
        }
    }
}
