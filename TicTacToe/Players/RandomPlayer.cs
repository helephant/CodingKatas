using System;
using TicTacToe.Game;

namespace TicTacToe.Players
{
    public class RandomPlayer : ITicTacToePlayer
    {
        public BoardPosition PlayTurn()
        {
            var random = new Random();
            return new BoardPosition(random.Next(1, 4), random.Next(1, 4));
        }
    }
}
