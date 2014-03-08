using TicTacToe.Players;

namespace TicTacToe.Game
{
    public class TicTacToeGame
    {
        private ITicTacToePlayer _currentPlayer;
        private TicTacToeBoard _board;

        public TicTacToeGame(ITicTacToePlayer naughts, ITicTacToePlayer crosses)
        {
            Naughts = naughts;
            Crosses = crosses;
            _board = new TicTacToeBoard();

            _currentPlayer = Naughts;
        }

        public void Play()
        {
            while(!IsFinished)
                PlayTurn();
        }

        public void PlayTurn()
        {
            if(IsFinished)
                throw new GameOverException();

            // this design isn't perfect because it doesn't feed back to the
            // player if they have made an invalid move
            var position = _currentPlayer.PlayTurn(_board);
            if(_board[position] == null)
            {
                _board = _board.UpdateBoard(position, _currentPlayer);
                if (HasMadeWinningMove(_currentPlayer))
                    Winner = _currentPlayer;

                _currentPlayer = _currentPlayer == Naughts ? Crosses : Naughts;
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
            get { return Winner != null || _board.IsComplete; }
        }

        public ITicTacToePlayer Naughts { get; private set; }

        public ITicTacToePlayer Crosses { get; set; }

        public ITicTacToePlayer Winner { get; private set; }
    }
}