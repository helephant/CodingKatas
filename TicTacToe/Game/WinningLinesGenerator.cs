using System;
using System.Collections.Generic;
using TicTacToe.Players;

namespace TicTacToe.Game
{
    public class WinningLinesGenerator
    {
        private readonly BoardBoundaries _boundaries;
        private readonly IEnumerable<ITicTacToePlayer> _boardStatus;

        private readonly List<Predicate<BoardPosition>> _lineEquation = new List<Predicate<BoardPosition>> 
            {
                x => x.Row == 1,
                x => x.Row == 2,
                x => x.Row == 3,
                x => x.Column == 1,
                x => x.Column == 2,
                x => x.Column == 3,
                x => x.Row == x.Column, // left diagonal
                x => x.Row == 4 - x.Column // right diagonal
            };

        public WinningLinesGenerator(BoardBoundaries boundaries, IEnumerable<ITicTacToePlayer> boardStatus)
        {
            _boundaries = boundaries;
            _boardStatus = boardStatus;
        }

        public IEnumerable<IEnumerable<ITicTacToePlayer>> PossibleWins()
        {
            foreach (var equation in _lineEquation)
            {
                yield return GenerateLine(equation);
            }
        }

        private IEnumerable<ITicTacToePlayer> GenerateLine(Predicate<BoardPosition> equation)
        {
            var e = _boardStatus.GetEnumerator();
            for (var row = _boundaries.TopLeft.Row; row <= _boundaries.BottomRight.Row; row++)
            {
                for (var column = _boundaries.TopLeft.Column; column <= _boundaries.BottomRight.Column; column++)
                {
                    var position = new BoardPosition(row, column);
                    e.MoveNext();

                    if(equation(position))
                        yield return e.Current;
                }
            }
        }
    }
}
