using TicTacToe.Game;

namespace TicTacToe.Players
{
    public class MiniMaxPlayer : ITicTacToePlayer
    {
        public BoardPosition PlayTurn(TicTacToeBoard board)
        {
            // figure out the best turn for this player
            
            // iterate through the possible moves and see if any are a win
            foreach (var square in board.VacantSquares)
            {
                var nextBoard = board.UpdateBoard(square, this);
                if (new WinEvaluator().HasPlayerWon(this, nextBoard))
                    return square;

            }

            return null;
        }
    }
}
