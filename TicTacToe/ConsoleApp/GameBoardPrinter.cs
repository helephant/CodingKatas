using System.Text;
using TicTacToe.Game;
using TicTacToe.Players;

namespace TicTacToe.ConsoleApp
{
    public class GameBoardPrinter
    {
        private readonly TicTacToeGame _game;

        public GameBoardPrinter(TicTacToeGame game)
        {
            _game = game;
        }

        public string SymbolForPlayer(ITicTacToePlayer player)
        {
            if (player == _game.Naughts)
                return "O";
            if (player == _game.Crosses)
                return "X";
            return " ";
        }

        public string NameForPlayer(ITicTacToePlayer player)
        {
            if (player == _game.Naughts)
                return "Naughts";
            if (player == _game.Crosses)
                return "Crosses";
            return null;
        }

        public string WinnerMessage()
        {
            if (_game.Winner != null)
                return "The winner is " + NameForPlayer(_game.Winner) + ".";

            return "No winner, players have drawn.";
        }

        public string Board()
        {
            var board = new StringBuilder();
            for (var row = 1; row <= 3; row++)
            {
                for (var column = 1; column <= 3; column++)
                {
                    var ticTacToePlayer = _game.PlayerOnSquare(row, column);
                    board.Append(" | " + SymbolForPlayer(ticTacToePlayer));
                }
                board.AppendLine(" | ");
            }
            return board.ToString();
        }
    }
}