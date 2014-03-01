namespace TicTacToe.Game
{
    public class BoardBoundaries
    {
        public BoardBoundaries(BoardPosition topLeft, BoardPosition bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }

        public bool IsInside(BoardPosition position)
        {
            return position.Column < TopLeft.Column || position.Column > BottomRight.Column ||
                   position.Row < TopLeft.Row || position.Column > BottomRight.Column;
        }

        public BoardPosition TopLeft { get; private set; }
        public BoardPosition BottomRight { get; private set; }

        public int TotalNumberOfSquares
        {
            get { 
                return (BottomRight.Column - TopLeft.Column + 1) * 
                         (BottomRight.Row - TopLeft.Row + 1); 
            }
        }
    }
}