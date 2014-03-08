using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Players;

namespace TicTacToe.Game
{
    public class TicTacToeBoard : IEnumerable<ITicTacToePlayer>
    {
        private readonly BoardBoundaries _boundries = new BoardBoundaries(new BoardPosition(3, 3));
        private readonly IDictionary<BoardPosition, ITicTacToePlayer> _board = new Dictionary<BoardPosition, ITicTacToePlayer>();

        public TicTacToeBoard()
        {
        }

        public TicTacToeBoard(IEnumerable<ITicTacToePlayer> boardState)
        {
            var players = boardState.GetEnumerator();
            var positions = _boundries.GetSquares().GetEnumerator();

            while (players.MoveNext() && positions.MoveNext())
            {
                if(players.Current != null)
                    _board[positions.Current] = players.Current;
            }
        }

        public IEnumerable<BoardPosition> VacantSquares
        {
            get
            {
                foreach (var square in _boundries.GetSquares())
                {
                    if (!_board.ContainsKey(square))
                        yield return square;
                }
            }
        } 

        public bool SquareIsFree(BoardPosition position)
        {
            return this[position] == null;
        }

        public ITicTacToePlayer this[BoardPosition position]
        {
            get
            {
                return _board.ContainsKey(position) ? _board[position] : null;
            }
        }

        public TicTacToeBoard UpdateBoard(BoardPosition position, ITicTacToePlayer player)
        {
            // TODO: check whether the square is free
            if (!Boundries.IsInside(position))
                throw new InvalidMoveException(position);

            var boardState = new List<ITicTacToePlayer>();
            foreach (var square in _boundries.GetSquares())
            {
                if(square == position)
                    boardState.Add(player);
                else if(_board.ContainsKey(square))
                    boardState.Add(_board[square]);
                else
                    boardState.Add(null);
            }

            return new TicTacToeBoard(boardState);
        }

        public IEnumerator<ITicTacToePlayer> GetEnumerator()
        {
            foreach (var position in _boundries.GetSquares())
            {
                yield return this[position];
            }
        }

        #region enumerator boiler plate
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        public BoardBoundaries Boundries
        {
            get { return _boundries; }
        }

        public bool IsComplete
        {
            get { return _board.Count >= Boundries.TotalNumberOfSquares; } 
        }
    }
}