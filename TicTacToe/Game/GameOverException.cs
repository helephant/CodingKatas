using System;

namespace TicTacToe.Game
{
    public class GameOverException : Exception
    {
        public GameOverException() : base("Can not play because the game is complete")
        {
        }
    }
}
