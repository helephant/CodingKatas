using System.Collections;
using System.Collections.Generic;

namespace TicTacToe
{
    internal class TicTacToeBoard : IEnumerable<ITicTacToePlayer>
    {
        private readonly BoardPosition _topLeft = new BoardPosition(1, 1);
        private readonly BoardPosition _bottomRight = new BoardPosition(3, 3);

        private readonly Dictionary<BoardPosition, ITicTacToePlayer> _board = new Dictionary<BoardPosition, ITicTacToePlayer>();
        private ITicTacToePlayer _winner;

        public void PlaySquare(BoardPosition position, ITicTacToePlayer player)
        {
            _board.Add(position, player);

            if (HasMadeWinningMove(player))
                _winner = player;
        }

        private bool HasMadeWinningMove(ITicTacToePlayer player)
        {
            if (_board.Count < 5) return false;

            var winEvaluator = new BinaryMaskWinEvaluator();
            return winEvaluator.HasPlayerWon(GetPositionsForPlayer(player));
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
       

        private IEnumerable<bool> GetPositionsForPlayer(ITicTacToePlayer currentPlayer)
        {
            foreach (var player in this)
            {
                yield return player == currentPlayer;
            }
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