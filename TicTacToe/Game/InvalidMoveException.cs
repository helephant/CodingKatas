using System;

namespace TicTacToe.Game
{
    public class InvalidMoveException : Exception
    {
        public InvalidMoveException(BoardPosition position) : base(position.Column + ", " + position.Row + " is not a valid TicTacToe move. It is outside the bounds of the board.")
        {
        }
    }
}