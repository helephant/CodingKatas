﻿using System.Collections;
using System.Collections.Generic;
using TicTacToe.Players;

namespace TicTacToe.Game
{
    public class TicTacToeBoard : IEnumerable<ITicTacToePlayer>
    {
        private readonly BoardBoundaries _boundries = new BoardBoundaries(new BoardPosition(3, 3));
        private readonly Dictionary<BoardPosition, ITicTacToePlayer> _board = new Dictionary<BoardPosition, ITicTacToePlayer>();

        public TicTacToeBoard()
        {
        }

        public TicTacToeBoard(IEnumerable<ITicTacToePlayer> boardState)
        {
            var players = boardState.GetEnumerator();
            var positions = _boundries.GetSquares().GetEnumerator();

            while (players.MoveNext() && positions.MoveNext())
            {
                this[positions.Current] = players.Current;
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
            set
            {
                if (Boundries.IsInside(position))
                    throw new InvalidMoveException(position);

                _board.Add(position, value);
            }
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