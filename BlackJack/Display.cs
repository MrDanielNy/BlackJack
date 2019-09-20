using System;
using System.Collections.Generic;

namespace BlackJack
{
    public class Display
    {
        public Display()
        {
        }

        /// <summary>
        /// Displays the dealers cards
        /// </summary>
        /// <param name="dealer">The Dealer object</param>
        /// <param name="isTimeToShowCards">If the second card should be a secret or not.</param>
        public static void DisplayDealer(Dealer dealer, bool isTimeToShowCards)
        {
            Console.Clear();
            Console.Write("\t\t\t\t\t\tDealer\n");
            if(isTimeToShowCards==false)
            {
                Console.WriteLine($"\t\t\t\t\t{dealer.DealerHand[0]}");
                Console.WriteLine($"\t\t\t\t\tCard turned upside down.");
            } else
            {
                for(int i=0;i<dealer.DealerHand.Length;i++)
                {
                    Console.WriteLine($"\t\t\t\t\t{dealer.DealerHand[i]}");
                }
                
            }
            //Console.Write("\t\t\t\t\t" + dealer.DealerHand [0]);
            Console.WriteLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        /// <param name="playersList"></param>
        public static void DisplayPlayers(int numberOfPlayers, List<Player> playersList)
        {
            //Show players names
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.Write("\t");
                Console.Write($"{playersList[i].PlayerName}");
                if (i < numberOfPlayers - 1)
                {
                    Console.Write("\t\t\t");   
                }
            }
            Console.WriteLine();

            //Show their cards
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < numberOfPlayers; j++)
                {
                    Console.Write($"{playersList[j].CurrentHand[i]}");
                    if (j < numberOfPlayers - 1)
                    {

                        Console.Write("\t\t");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Display the current gaming board/table
        /// </summary>
        /// <param name="dealer">The Dealer object</param>
        /// <param name="numberOfPlayers">Number of players playing</param>
        /// <param name="playersList">The list of players</param>
        /// <param name="deal">Current deal</param>
        public static void DisplayBoard(Dealer dealer, int numberOfPlayers, List<Player> playersList, int deal)
        {
            DisplayDealer(dealer, false);
            DisplayPlayers(numberOfPlayers, playersList);
        }

        /// <summary>
        /// Display the welcome screen to users
        /// </summary>
        public static void DisplayWelcomeScreen()
        {
            Console.WriteLine("=====================================================");
            Console.WriteLine("====================WELCOME!!!!======================");
            Console.WriteLine("=====================================================");
        }
    }
}
