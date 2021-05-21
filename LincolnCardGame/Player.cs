using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

namespace LincolnCardGame
{
    interface IPlayer
    {
        public abstract void Play(Player opponent);
    }

    internal abstract class Player : IPlayer
    {
        protected int _wins = 0;
        protected int _score;
        protected string _playedCards;
        protected Hand hand;
        protected Data data = new Data();
        protected Functions functions = new Functions();
        protected Random random = new Random();
        protected List<Card> _playerHand;

        public List<Card> playerHand
        {
            get { return _playerHand; }
            set { _playerHand = value; }
        }

        public int wins
        {
            get { return _wins; }
            set { _wins = value; }
        }

        public int score
        {
            get { return _score; }
        }

        public string playedCards
        {
            get { return _playedCards; }
        }

        public abstract void Play(Player opponent);
    }

    class User : Player
    {
        public override void Play(Player opponent)
        {
            // request selected cards via .GetSlots and get new hand + 2 selected cards
            (List<Card> newHand, List<Card> cards) =
                functions.GetSlot(_wins, opponent.wins, _playerHand, 2);

            // update hand, score and played cards for game summary
            _playerHand = newHand;
            _score = cards[0].numValue + cards[1].numValue;
            _playedCards = $"{cards[0].displayValue}, {cards[1].displayValue}";
        }

        public User(Deck cardDeck)
        {
            // create new hand and update _playerHand variable
            hand = new Hand(cardDeck);
            _playerHand = hand.Get();
        }
    }

    class Bot : Player
    {
        public override void Play(Player opponent)
        {
            // set score of 0 and get new list of cards
            _score = 0;
            List<string> _cards = new List<string>();

            // use for loop from 0-2 to select two cards before making move
            for (int i = 0; i < 2; i++)
            {
                // clear console and output bot's current hand selection
                Console.Clear();
                functions.OutputSlots(2, opponent.wins, wins, playerHand);

                // wait 1.5 seconds to make bot feel more human-like
                Console.Write("\nWaiting for opponent's move...\n> ");
                Thread.Sleep(1500);

                // select a card from hand at random, update score
                // and add to selected card list for game logging
                int selection = random.Next(playerHand.Count());
                _score += _playerHand[selection].numValue;
                _cards.Add(_playerHand[selection].displayValue);

                // output bot's selected slot number, wait 0.5 seconds
                Console.Write(selection);
                Thread.Sleep(500);

                // remove selected card from bot's hand
                _playerHand.RemoveAt(selection);
            }

            // after selecting both cards, set played cards string for summary
            _playedCards = String.Join(", ", _cards);
        }

        public Bot(Deck cardDeck)
        {
            // create new hand and update _playerHand variable
            hand = new Hand(cardDeck);
            _playerHand = hand.Get();
        }
    }
}