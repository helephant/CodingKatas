namespace TicTacToe
{
    public class TicTacToeGame
    {
        private readonly ITicTacToePlayer _naughts;
        private readonly ITicTacToePlayer _crosses;
        private ITicTacToePlayer _winner;
        private ITicTacToePlayer _currentPlayer;
        private readonly Board _board;

        public TicTacToeGame(ITicTacToePlayer naughts, ITicTacToePlayer crosses)
        {
            _naughts = naughts;
            _crosses = crosses;
            _board = new Board();

            _currentPlayer = _naughts;
        }

        public void PlayTurn()
        {
            if(IsFinished)
                throw new GameOverException();

            // this design isn't perfect because it doesn't feed back to the
            // player if they have made an invalid move
            var position = _currentPlayer.PlayTurn();
            if(_board[position] == null)
            {
                _board[position] = _currentPlayer;
                if (HasMadeWinningMove(_currentPlayer))
                    _winner = _currentPlayer;

                _currentPlayer = _currentPlayer == _naughts ? _crosses : _naughts;
            }
        }

        public ITicTacToePlayer PlayerOnSquare(int row, int column)
        {
            return _board[new BoardPosition(row, column)];
        }

        private bool HasMadeWinningMove(ITicTacToePlayer player)
        {
            var winEvaluator = new WinEvaluator();
            return winEvaluator.HasPlayerWon(player, _board);
        }

        public bool IsFinished
        {
            get { return _winner != null || _board.IsComplete; }
        }

        public ITicTacToePlayer Winner
        {
            get { return _winner; }
        }
    }
}