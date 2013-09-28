using System;

namespace TicTacToe
{
    public class TicTacToeGameOverException : Exception
    {
        public TicTacToeGameOverException() : base("Can not play because the game is complete")
        {
        }
    }
}
