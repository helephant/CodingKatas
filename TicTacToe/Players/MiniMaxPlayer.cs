using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Game;
using TicTacToe.Game.WinEvaluators;

namespace TicTacToe.Players
{
    /// <summary>
    /// This minimax-player doesn't not give preference faster wins.
    /// </summary>
    public class MiniMaxPlayer : ITicTacToePlayer
    {
        private readonly ITicTacToePlayer _opponent;

        public MiniMaxPlayer(ITicTacToePlayer opponent)
        {
            _opponent = opponent;
        }

        public BoardPosition PlayTurn(TicTacToeBoard board)
        {
            return MaximizeScore(board).Item1;
        }

        private Tuple<BoardPosition, int> MaximizeScore(TicTacToeBoard board)
        {
            var scores = new List<Tuple<BoardPosition, int>>();
            foreach (var boardPosition in board.VacantSquares)
            {
                var nextBoard = board.UpdateBoard(boardPosition, this);

                if (IsFinalMove(nextBoard, this, _opponent))
                {
                    var score = EvaluateMove(nextBoard, this, _opponent);
                    scores.Add(new Tuple<BoardPosition, int>(boardPosition, score));
                }
                else
                {
                    scores.Add(new Tuple<BoardPosition, int>(boardPosition, MinimizeScore(nextBoard).Item2));
                }
            }
            return scores.OrderByDescending(x => x.Item2).First();
        }

        private Tuple<BoardPosition, int> MinimizeScore(TicTacToeBoard board)
        {
            var scores = new List<Tuple<BoardPosition, int>>();
            foreach (var boardPosition in board.VacantSquares)
            {
                var nextBoard = board.UpdateBoard(boardPosition, _opponent);

                if (IsFinalMove(nextBoard, _opponent, this))
                {
                    var score = EvaluateMove(nextBoard, _opponent, this);
                    scores.Add(new Tuple<BoardPosition, int>(boardPosition, score));
                }
                else
                {
                    scores.Add(new Tuple<BoardPosition, int>(boardPosition, MaximizeScore(nextBoard).Item2));
                }
            }
            return scores.OrderBy(x => x.Item2).First();
        }

        private bool IsFinalMove(TicTacToeBoard board, ITicTacToePlayer currentPlayer, ITicTacToePlayer otherPlayer)
        {
            return board.IsComplete || (new EquationWinEvaluator()).HasPlayerWon(currentPlayer, board);
        }

        private int EvaluateMove(TicTacToeBoard board, ITicTacToePlayer currentPlayer, ITicTacToePlayer otherPlayer)
        {
            const int win = 1;
            const int draw = 0;
            const int loss = -1;

            if ((new EquationWinEvaluator()).HasPlayerWon(currentPlayer, board))
                return currentPlayer == this ? win : loss; 

            return draw; 
        }

    }
}
