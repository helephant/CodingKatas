﻿using System;

namespace TicTacToe
{
    internal class WinEvaluator
    {
        private readonly int[] _wins = new[]
            {
                // Did this because there is no binary literal 
                // and I wanted it to be easy to read
                Convert.ToInt32("111000000",2),
                Convert.ToInt32("000111000",2),
                Convert.ToInt32("000000111",2),
                Convert.ToInt32("100100100",2),
                Convert.ToInt32("010010010",2),
                Convert.ToInt32("001001001",2),
                Convert.ToInt32("100010001",2),
                Convert.ToInt32("001010100",2)
            };

        public bool HasPlayerWon(ITicTacToePlayer currentPlayer, Board board)
        {
            var playerMask = GetPositionMaskForPlayer(currentPlayer, board);
            foreach (var win in _wins)
            {
                if ((win & playerMask) == win)
                    return true;
            }
            return false;
        }

        private int GetPositionMaskForPlayer(ITicTacToePlayer currentPlayer, Board board)
        {
            var positionMask = 0;
            var gridIndex = 1;
            foreach (var playerOnPosition in board)
            {
                if (playerOnPosition == currentPlayer)
                    positionMask |= 1 << (9 - gridIndex);
                gridIndex++;
            }
            return positionMask;
        }
    }
}