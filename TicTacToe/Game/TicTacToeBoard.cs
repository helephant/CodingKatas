using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Players;

namespace TicTacToe.Game
{
    public class TicTacToeBoard : IEnumerable<ITicTacToePlayer>
    {
        private readonly BoardPosition _topLeftBound = new BoardPosition(1, 1);
        private readonly BoardPosition _bottomRightBound = new BoardPosition(3, 3);
        public const int NumberOfSpacesOnBoard = 9;

        private readonly Dictionary<BoardPosition, ITicTacToePlayer> _board = new Dictionary<BoardPosition, ITicTacToePlayer>();

        public TicTacToeBoard()
        {
        }

        public TicTacToeBoard(IEnumerable<ITicTacToePlayer> boardState)
        {
            var players = boardState.GetEnumerator();
            var positions = GetBoardPositions(_topLeftBound, _bottomRightBound).GetEnumerator();

            while (players.MoveNext() && positions.MoveNext())
            {
                this[positions.Current] = players.Current;
            }
        }

        public bool SquareIsFree(BoardPosition position)
        {
            return this[position] == null;
        }

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

        private IEnumerable<BoardPosition> GetBoardPositions(BoardPosition topRight, BoardPosition bottomLeft)
        {
            for (var row = topRight.Row; row <= bottomLeft.Row; row++)
            {
                for (var column = topRight.Column; column <= bottomLeft.Column; column++)
                {
                    yield return new BoardPosition(row, column);
                }
            }
        }

        public IEnumerator<ITicTacToePlayer> GetEnumerator()
        {
            foreach (var position in GetBoardPositions(_topLeftBound, _bottomRightBound))
            {
                yield return this[position];
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