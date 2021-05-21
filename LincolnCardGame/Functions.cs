using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LincolnCardGame
{
    interface IFunctions
    {
        int IntInput(string message, List<int> allowedValues);
        int OutputSlots(int player, int wins, int opponentWins, List<Card> hand);
        (List<Card>, Card) GetSlot(int wins, int opponentWins, List<Card> hand);
        (List<Card>, List<Card>) GetSlot(int wins, int opponentWins, List<Card> hand, int count);
        (List<Card>, Card, Card) GetBestMove(List<Card> hand);
        (int, List<string>) CompareWinners(Player user, Player bot, List<string> logs, int increment,
            int roundNumber, int userScore, int botScore, string userPlayedCards, string botPlayedCards);
    }

    class Functions : IFunctions
    {
        private Data data;

        public int IntInput(string message, List<int> allowedValues)
        {
            // output given message
            Console.WriteLine(message);

            // keep repeating until valid input given
            while (true) {
                Console.Write("> ");

                string value = Console.ReadLine();
                int number;

                // if value can be converted to number
                if (int.TryParse(value, out number)) {
                    
                    // if value is within list of allowed values
                    if (allowedValues.Contains(number))
                    {
                        // return number given through input
                        return number;
                    }
                }

                // invalid input given, keep requesting until valid input given
                Console.WriteLine("Invalid input! Please try again...");
            }
        }

        public int OutputSlots(int player, int wins, int opponentWins, List<Card> hand)
        {
            // output .playerTurn or .botTurn based on player type
            string title = player == 1 ? data.playerTurn : data.botTurn;
            Console.WriteLine(String.Format(title, wins, opponentWins));
            int counter = -1;

            // for every card in player's hand, display with slot numbers for input
            foreach (Card card in hand)
            {
                counter++;
                Console.WriteLine($"[Slot {counter}] {card.displayValue}");
            }

            // return total cards in player's hand
            return counter;
        }

        public (List<Card>, Card) GetSlot(int wins, int opponentWins, List<Card> hand)
        {
            // clear console, get total slots, get allowed input values
            // and request int input from user
            Console.Clear();
            int slots = OutputSlots(1, wins, opponentWins, hand);
            List<int> values = Enumerable.Range(0, slots + 1).ToList();
            int slot = IntInput($"\nChoose a slot number (0-{slots})", values);

            // get card at inputted slot value and remove
            Card card = hand[slot];
            hand.RemoveAt(slot);

            // return new hand and selected card
            return (hand, card);
        }

        public (List<Card>, List<Card>) GetSlot(int wins, int opponentWins, List<Card> hand, int count)
        {
            List<Card> selectedCards = new List<Card>();

            // get new hands and card info from 2 player turns in a round of selecting cards
            for (int i = 0; i < 2; i++)
            {
                (List<Card> newHand, Card card) = GetSlot(wins, opponentWins, hand);
                selectedCards.Add(card);
                hand = newHand;
            }

            // return new hand and both selected cards
            return (hand, selectedCards);
        }

        public (List<Card>, Card, Card) GetBestMove(List<Card> hand)
        {
            // select cards at start of hand list for best bot move
            // hand is in descending order, so will always contain highest cards in hand
            Card card1 = hand[0];
            Card card2 = hand[1];
            hand = hand.GetRange(2, hand.Count() - 2);

            // return new hand and selected cards
            return (hand, card1, card2);
        }

        public (int, List<string>) CompareWinners(Player user, Player bot, List<string> logs, int increment,
            int roundNumber, int userScore, int botScore, string userPlayedCards, string botPlayedCards)

        {
            // if user wins, add to win count and reset increment
            // increment of 0 means score of 1 next round rather than 2+ for winning
            if (userScore > botScore)
            {
                user.wins += increment;
                increment = 0;
            }

            // if bot wins, add to win count and reset increment
            else if (botScore > userScore)
            {
                bot.wins += increment;
                increment = 0;
            }

            // add round information to game logs to be shown at summary
            logs.Add(
                String.Format(
                    data.logFormat,
                    roundNumber,
                    userScore,
                    userPlayedCards,
                    botScore,
                    botPlayedCards,
                    user.wins,
                    bot.wins
                )
            );

            // return new increment and new game logs
            return (increment, logs);
        }

        public Functions()
        {
            data = new Data();
        }
    }
}
