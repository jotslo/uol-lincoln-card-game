using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LincolnCardGame
{
    interface IHand
    {
        List<Card> Get();
    }

    class Hand : IHand
    {
        private List<Card> _newHand = new List<Card>();

        public List<Card> Get()
        {
            // return hand in order of descending numerical value
            return _newHand.OrderByDescending(card => card.numValue).ToList();
        }

        public Hand(Deck cardDeck)
        {
            // add 10 cards from deck to player's hand
            for (int i = 0; i < 10; i++)
            {
                if (!cardDeck.IsEmpty())
                {
                    _newHand.Add(cardDeck.Deal());
                }

                // if deck is empty, end for loop as no more available
                else
                {
                    break;
                }
            }
        }
    }
}
