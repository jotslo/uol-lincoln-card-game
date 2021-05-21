using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LincolnCardGame
{
    class Data
    {
        private string _mainMenu =
@"Lincoln - Main Menu

- Enter 'P' to PLAY
- Enter 'Q' to QUIT
";
        private string _playerTurn =
@"Lincoln - Your Turn

 You | Opponent
 {0}   | {1}

Your cards:";

        private string _botTurn =
@"Lincoln - Opponent's Turn

 You | Opponent
 {0}   | {1}

Opponent's cards:";

        private string _playerWin =
@"Lincoln - You Win!

 You | Opponent
 {0}   | {1}

Press enter to view game summary...
> ";


        private string _botWin =
@"Lincoln - You Lose!

 You | Opponent
 {0}   | {1}

Press enter to view game summary...
> ";

        private string _logFormat = 
@"[Round {0}]
Your score: {1} ({2})
Opponent's score: {3} ({4})
You: {5} | Opponent: {6}
";

        private string _returnToMenu =
@"Lincoln - Game Summary

{0}

Press enter to return to main menu...
> ";

        private Dictionary<string, int> _numValues =
            new Dictionary<string, int>();

        public string mainMenu
        {
            get { return _mainMenu; }
        }

        public string playerTurn
        {
            get { return _playerTurn; }
        }

        public string botTurn
        {
            get { return _botTurn; }
        }

        public string playerWin
        {
            get { return _playerWin; }
        }

        public string botWin
        {
            get { return _botWin; }
        }

        public string logFormat
        {
            get { return _logFormat; }
        }

        public string returnToMenu
        {
            get { return _returnToMenu; }
        }

        public Dictionary<string, int> numValues
        {
            get { return _numValues; }
        }

        public Data()
        {
            // for every number from 1-10, add to _numValues mapping dictionary
            foreach (int number in Enumerable.Range(1, 11))
            {
                _numValues.Add(number.ToString(), number);
            }

            // add remaining mapped numerical values to dictionary
            _numValues.Add("J", 11);
            _numValues.Add("Q", 12);
            _numValues.Add("K", 13);
            _numValues.Add("A", 14);
        }
    }
}
