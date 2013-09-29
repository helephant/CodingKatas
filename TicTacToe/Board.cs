using System.Collections;
using System.Collections.Generic;

namespace TicTacToe
{
    internal class Board : IEnumerable<ITicTacToePlayer>
    {
        private readonly BoardPosition _topLeftBound = new BoardPosition(1, 1);
        private readonly BoardPosition _bottomRightBound = new BoardPosition(3, 3);
        public const int NumberOfSpacesOnBoard = 9;

        private readonly Dictionary<BoardPosition, ITicTacToePlayer> _board = new Dictionary<BoardPosition, ITicTacToePlayer>();

        public ITicTacToePlayer this[BoardPosition position]
        {
            get
            {
                return _board.ContainsKey(position) ? _board[position] : null;
            }
            set
            {
                if (IsInsideBounds(position))
                    throw new InvalidMoveException(position);

                _board.Add(position, value);
            }
        }

        private bool IsInsideBounds(BoardPosition position)
        {
            return position.Column < _topLeftBound.Column || position.Column > _bottomRightBound.Column ||
                   position.Row < _topLeftBound.Row || position.Column > _bottomRightBound.Column;
        }

        public IEnumerator<ITicTacToePlayer> GetEnumerator()
        {
            for (var row = _topLeftBound.Row; row <= _bottomRightBound.Row; row++)
            {
                for (var column = _topLeftBound.Column; column <= _bottomRightBound.Column; column++)
                {
                    yield return this[new BoardPosition(row, column)];
                }
            }
        }

        #region enumerator boiler plate
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        public bool IsComplete
        {
            get { return _board.Count >= NumberOfSpacesOnBoard; } 
        }
    }
}