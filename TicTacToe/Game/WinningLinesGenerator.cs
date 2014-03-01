using System;
using System.Collections.Generic;
using TicTacToe.Players;

namespace TicTacToe.Game
{
    public class WinningLinesGenerator
    {
        private readonly BoardPosition _topLeft;
        private readonly BoardPosition _bottomRight;
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

        public WinningLinesGenerator(BoardPosition topLeft, BoardPosition bottomRight, IEnumerable<ITicTacToePlayer> boardStatus)
        {
            _topLeft = topLeft;
            _bottomRight = bottomRight;
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
            for (var row = _topLeft.Row; row <= _bottomRight.Row; row++)
            {
                for (var column = _topLeft.Column; column <= _bottomRight.Column; column++)
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
