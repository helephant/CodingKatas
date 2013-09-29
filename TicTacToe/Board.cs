using System.Collections;
using System.Collections.Generic;

namespace TicTacToe
{
    internal class Board : IEnumerable<ITicTacToePlayer>
    {
        private readonly BoardPosition _topLeft = new BoardPosition(1, 1);
        private readonly BoardPosition _bottomRight = new BoardPosition(3, 3);

        private readonly Dictionary<BoardPosition, ITicTacToePlayer> _board = new Dictionary<BoardPosition, ITicTacToePlayer>();
        private ITicTacToePlayer _winner;

        public void PlaySquare(BoardPosition position, ITicTacToePlayer player)
        {
            if(IsInsideBounds(position))
                throw new InvalidMoveException(position);

            _board.Add(position, player);
            if (HasMadeWinningMove(player))
                _winner = player;
        }

        public ITicTacToePlayer GetPlayerAtPosition(int row, int column)
        {
            return GetPlayerAtPosition(new BoardPosition(row, column));
        }

        public ITicTacToePlayer GetPlayerAtPosition(BoardPosition position)
        {
            return _board.ContainsKey(position) ? _board[position] : null;
        }

        public ITicTacToePlayer Winner
        {
            get { return _winner; }
        }

        public bool IsComplete()
        {
            return _winner != null || _board.Count >= 9;
        }

        private bool HasMadeWinningMove(ITicTacToePlayer player)
        {
            if (_board.Count < 5) return false;

            var winEvaluator = new WinEvaluator();
            return winEvaluator.HasPlayerWon(GetPositionsForPlayer(player));
        }

        private IEnumerable<bool> GetPositionsForPlayer(ITicTacToePlayer currentPlayer)
        {
            foreach (var player in this)
            {
                yield return player == currentPlayer;
            }
        }

        private bool IsInsideBounds(BoardPosition position)
        {
            return position.Column < _topLeft.Column || position.Column > _bottomRight.Column ||
                   position.Row < _topLeft.Row || position.Column > _bottomRight.Column;
        }

        public IEnumerator<ITicTacToePlayer> GetEnumerator()
        {
            for (var y = _topLeft.Row; y <= _bottomRight.Row; y++)
            {
                for (var x = _topLeft.Column; x <= _bottomRight.Column; x++)
                {
                    yield return GetPlayerAtPosition(x, y);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}