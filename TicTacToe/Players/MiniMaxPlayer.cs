using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Game;
using TicTacToe.Game.WinEvaluators;

namespace TicTacToe.Players
{
    /// <summary>
    /// Plays tictactoe using the MiniMax algorithm which brute-force searches all of the
    /// possible moves to find the best move. 
    /// </summary>
    public class MiniMaxPlayer : ITicTacToePlayer
    {
        private const int Win = 1;
        private const int Draw = 0;
        private const int Loss = -1;
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

                var score = IsFinalMove(nextBoard, this, _opponent)
                    ? EvaluateMove(nextBoard, this, _opponent)
                    : MinimizeScore(nextBoard).Item2;

                if (ShouldAlphaBetaPrune(score, Win))
                    return new Tuple<BoardPosition, int>(boardPosition, score);

                scores.Add(new Tuple<BoardPosition, int>(boardPosition, score));
            }
            return scores.OrderByDescending(x => x.Item2).First();
        }

        private Tuple<BoardPosition, int> MinimizeScore(TicTacToeBoard board)
        {
            var scores = new List<Tuple<BoardPosition, int>>();
            foreach (var boardPosition in board.VacantSquares)
            {
                var nextBoard = board.UpdateBoard(boardPosition, _opponent);

                var score = IsFinalMove(nextBoard, _opponent, this) ? 
                    EvaluateMove(nextBoard, _opponent, this) :
                    MaximizeScore(nextBoard).Item2;
                var scoreForPosition = new Tuple<BoardPosition, int>(boardPosition, score);

                if (ShouldAlphaBetaPrune(score, Loss))
                    return scoreForPosition;

                scores.Add(scoreForPosition);
            }
            return scores.OrderBy(x => x.Item2).First();
        }

        private bool IsFinalMove(TicTacToeBoard board, ITicTacToePlayer currentPlayer, ITicTacToePlayer otherPlayer)
        {
            return board.IsComplete || (new EquationWinEvaluator()).HasPlayerWon(currentPlayer, board);
        }

        private bool ShouldAlphaBetaPrune(int score, int idealScore)
        {
            // Alpha-beta pruning means if we have found the maximum or minimum score already
            // we don't need to keep looking through the board for a better score
            return score == idealScore;
        }

        private int EvaluateMove(TicTacToeBoard board, ITicTacToePlayer currentPlayer, ITicTacToePlayer otherPlayer)
        {
            if ((new EquationWinEvaluator()).HasPlayerWon(currentPlayer, board))
                return currentPlayer == this ? Win : Loss; 

            return Draw; 
        }

    }
}
