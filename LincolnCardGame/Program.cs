using System;
using System.Collections.Generic;

namespace LincolnCardGame
{
    class Program
    {
        static void Game(Deck cardDeck)
        {
            Functions functions = new Functions();
            Data data = new Data();

            User user = new User(cardDeck);
            Bot bot = new Bot(cardDeck);

            List<string> logs = new List<string>();

            int increment = 0;
            int roundNumber = 0;


            // while player's hand has cards, do the following
            while (user.playerHand.Count > 0)
            {
                // call .Play for each player, then increment variables
                user.Play(bot);
                bot.Play(user);
                increment++;
                roundNumber++;

                // update increment and logs vars based on winner of the round
                (increment, logs) = functions.CompareWinners(
                    user, bot, logs, increment, roundNumber, user.score,
                    bot.score, user.playedCards, bot.playedCards
                );
            }

            // after game ended, if increment still exists, there are still points
            // that need to be given out - keep doing the following until resolved
            while (increment > 0)
            {
                // if card deck is empty, recreate and shuffle to ensure winner is found
                if (cardDeck.IsEmpty())
                {
                    cardDeck = new Deck();
                    cardDeck.Shuffle();
                    continue;
                }

                // deal card for both user and bots, increase round number
                Card userCard = cardDeck.Deal();
                Card botCard = cardDeck.Deal();
                roundNumber++;

                // update variables based on winner of round
                (increment, logs) = functions.CompareWinners(
                    user, bot, logs, increment, roundNumber, userCard.numValue,
                    botCard.numValue, userCard.displayValue, botCard.displayValue
                );
            }

            // get .playerWin or .botWin string based on winner
            string winnerInfo = user.wins > bot.wins ? data.playerWin : data.botWin;
            
            // clear output, show final scores and wait for input
            Console.Clear();
            Console.Write(String.Format(winnerInfo, user.wins, bot.wins));
            Console.ReadLine();

            // clear output, show game summary and wait for input before reset
            Console.Clear();
            Console.Write(String.Format(data.returnToMenu, String.Join("\n", logs)));
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            Data data = new Data();

            // main menu loop, doesn't end until user quits program
            while (true)
            {
                // clear console, write main menu info and request input
                Console.Clear();
                Console.Write(data.mainMenu + "\n> ");
                char input = Console.ReadLine().ToUpper()[0];

                // if play selected, create deck, shuffle and start game
                if (input == 'P')
                {
                    try
                    {
                        Deck cardDeck = new Deck();
                        cardDeck.Shuffle();
                        Game(cardDeck);
                    }

                    // if error caught during game, return to main menu
                    catch
                    {
                        Console.WriteLine("Unknown error, restarting...");
                    }
                }

                // if quit selected, exit program gracefully
                else if (input == 'Q')
                {
                    System.Environment.Exit(0);
                }
            }
        }
    }
}
