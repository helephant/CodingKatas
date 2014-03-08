using System;

namespace TicTacToe.Game
{
    public class BoardPosition : IEquatable<BoardPosition>
    {


        public int Row { get; set; }
        public int Column { get; set; }

        public BoardPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        #region equality 
        public static bool operator ==(BoardPosition left, BoardPosition right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BoardPosition left, BoardPosition right)
        {
            return !Equals(left, right);
        }

        public bool Equals(BoardPosition other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Row == other.Row && Column == other.Column;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BoardPosition) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Row*397) ^ Column;
            }
        }
        #endregion
    }
}