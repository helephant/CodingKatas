namespace TicTacToe
{
    public class TicTacToeGame
    {
        private readonly ITicTacToePlayer _naughts;
        private readonly ITicTacToePlayer _crosses;
        private ITicTacToePlayer _currentPlayer;
        private readonly Board _board;

        public TicTacToeGame(ITicTacToePlayer naughts, ITicTacToePlayer crosses)
        {
            _naughts = naughts;
            _crosses = crosses;
            _board = new Board();

            _currentPlayer = _naughts;
        }

        public bool IsFinished()
        {
            return _board.IsComplete();
        }

        public void PlayTurn()
        {
            if(IsFinished())
                throw new TicTacToeGameOverException();

            BoardPosition position = _currentPlayer.PlayTurn();
            if(_board.GetPlayerAtPosition(position) == null)
            {
                _board.PlaySquare(position, _currentPlayer);
                _currentPlayer = _currentPlayer == _naughts ? _crosses : _naughts;
            }
        }

        public ITicTacToePlayer Winner
        {
            get { return _board.Winner; }
        }

        public ITicTacToePlayer PlayerOnSquare(int row, int column)
        {
            return _board.GetPlayerAtPosition(new BoardPosition(row, column));
        }
    }
}