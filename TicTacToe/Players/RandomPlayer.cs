using System;
using TicTacToe.Game;

namespace TicTacToe.Players
{
    public class RandomPlayer : ITicTacToePlayer
    {
        public BoardPosition PlayTurn(TicTacToeBoard board)
        {
            var random = new Random();

            BoardPosition move;
            do
            {
                move = new BoardPosition(random.Next(1, 4), random.Next(1, 4));
            } while (!board.SquareIsFree(move));
                
            return move;
        }
    }
}
