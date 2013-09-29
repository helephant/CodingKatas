using System.Collections;
using System.Collections.Generic;

namespace TicTacToe
{
    internal class Board : IEnumerable<ITicTacToePlayer>
    {
        private readonly BoardPosition _topLeftBound = new BoardPosition(1, 1);
        private readonly BoardPosition _bottomRightBound = new BoardPosition(3, 3);

        private readonly Dictionary<BoardPosition, ITicTacToePlayer> _board = new Dictionary<BoardPosition, ITicTacToePlayer>();

        public void PlaySquare(BoardPosition position, ITicTacToePlayer player)
        {
            if(IsInsideBounds(position))
                throw new InvalidMoveException(position);

            _board.Add(position, player);
        }

        public ITicTacToePlayer GetPlayerAtPosition(int row, int column)
        {
            return GetPlayerAtPosition(new BoardPosition(row, column));
        }

        public ITicTacToePlayer GetPlayerAtPosition(BoardPosition position)
        {
            return _board.ContainsKey(position) ? _board[position] : null;
        }

        private bool IsInsideBounds(BoardPosition position)
        {
            return position.Column < _topLeftBound.Column || position.Column > _bottomRightBound.Column ||
                   position.Row < _topLeftBound.Row || position.Column > _bottomRightBound.Column;
        }

        public IEnumerator<ITicTacToePlayer> GetEnumerator()
        {
            for (var y = _topLeftBound.Row; y <= _bottomRightBound.Row; y++)
            {
                for (var x = _topLeftBound.Column; x <= _bottomRightBound.Column; x++)
                {
                    yield return GetPlayerAtPosition(x, y);
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
            get { return _board.Count >= 9; }
        }
    }
}