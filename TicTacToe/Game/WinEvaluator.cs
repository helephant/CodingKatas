using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Players;

namespace TicTacToe.Game
{
    internal class WinEvaluator
    {
        public bool HasPlayerWon(ITicTacToePlayer currentPlayer, TicTacToeBoard ticTacToeBoard)
        {
            var winningLines = new WinningLinesGenerator(ticTacToeBoard.Boundries, ticTacToeBoard).PossibleWins();
            foreach (var winningLine in winningLines)
            {
                if (PlayerHasWon(currentPlayer, winningLine))
                    return true;
            }

            return false;
        }

        private bool PlayerHasWon(ITicTacToePlayer currentPlayer, IEnumerable<ITicTacToePlayer> winningLine)
        {
            return winningLine.All(player => player == currentPlayer);
        }
    }
}