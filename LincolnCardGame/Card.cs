using System;
using System.Collections.Generic;
using System.Text;

namespace LincolnCardGame
{
    class Card
    {
        private string _suit;
        private string _cardValue;
        private string _displayValue;
        private int _numValue;

        public string suit
        {
            get { return _suit; }
        }

        public string cardValue
        {
            get { return _cardValue; }
        }

        public string displayValue
        {
            get { return _displayValue; }
        }

        public int numValue
        {
            get { return _numValue; }
        }

        public Card(string definedSuit, string definedValue)
        {
            // set local variables to relevant given arguments
            Data data = new Data();
            _suit = definedSuit;
            _cardValue = definedValue;
            _displayValue = definedValue + " of " + definedSuit;
            _numValue = data.numValues[definedValue];
        }
    }
}