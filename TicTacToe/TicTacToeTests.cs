using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;

namespace TicTacToe
{
    [TestFixture]
    public class TicTacToeTests
    {
        [Test]
        public void NaughtsStartsTheGame()
        {
            var naughts = new PlayerStub();
            naughts.NextTurn = () => new BoardPosition(1, 1);

            var game = new TicTacToeGame(naughts, null);
            game.PlayTurn();

            Assert.That(game.PlayerOnSquare(1, 1), Is.EqualTo(naughts));
        }

        [Test]
        public void PlayersAlternateTurns()
        {
            var naughts = new PlayerStub();
            naughts.NextTurn = () => new BoardPosition(1, 1);

            var crosses = new PlayerStub();
            crosses.NextTurn = () => new BoardPosition(2, 2);

            var game = new TicTacToeGame(naughts, crosses);
            game.PlayTurn();
            game.PlayTurn();

            Assert.That(game.PlayerOnSquare(2, 2), Is.EqualTo(crosses));
        }

        [Test]
        public void OnlyOnePlayerCanClaimEachSquare()
        {
            var naughts = new PlayerStub();
            var crosses = new PlayerStub();
            var game = new TicTacToeGame(naughts, crosses);

            naughts.NextTurn = () => new BoardPosition(1, 1);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(1, 1);
            game.PlayTurn();

            Assert.That(game.PlayerOnSquare(1, 1), Is.EqualTo(naughts));
        }

        [Test]
        public void PlayersTurnIsNotOverUntilTheyMakeAValidMove()
        {
            var naughts = new PlayerStub();
            var crosses = new PlayerStub();
            var game = new TicTacToeGame(naughts, crosses);

            naughts.NextTurn = () => new BoardPosition(1, 1);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(1, 1);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(2, 2);
            game.PlayTurn();

            Assert.That(game.PlayerOnSquare(1, 1), Is.EqualTo(naughts));
            Assert.That(game.PlayerOnSquare(2, 2), Is.EqualTo(crosses));
        }

        [Test]
        public void ThreeInAHorizontalRowWins()
        {
            var naughts = new PlayerStub();
            var crosses = new PlayerStub();
            var game = new TicTacToeGame(naughts, crosses);

            naughts.NextTurn = () => new BoardPosition(1, 1);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(2, 1);
            game.PlayTurn();

            naughts.NextTurn = () => new BoardPosition(1, 2);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(2, 2);
            game.PlayTurn();

            naughts.NextTurn = () => new BoardPosition(1, 3);
            game.PlayTurn();

            Assert.That(game.IsFinished());
            Assert.That(game.Winner, Is.EqualTo(naughts));
        }

        [Test]
        public void ThreeInAVerticalColumnWins()
        {
            var naughts = new PlayerStub();
            var crosses = new PlayerStub();
            var game = new TicTacToeGame(naughts, crosses);

            naughts.NextTurn = () => new BoardPosition(1, 2);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(1, 1);
            game.PlayTurn();

            naughts.NextTurn = () => new BoardPosition(2, 2);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(2, 1);
            game.PlayTurn();

            naughts.NextTurn = () => new BoardPosition(3, 2);
            game.PlayTurn();

            Assert.That(game.IsFinished());
            Assert.That(game.Winner, Is.EqualTo(naughts));
        }

        [Test]
        public void ThreeInALeftToRightDiagonalWins()
        {
            var naughts = new PlayerStub();
            var crosses = new PlayerStub();
            var game = new TicTacToeGame(naughts, crosses);

            naughts.NextTurn = () => new BoardPosition(1, 1);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(1, 2);
            game.PlayTurn();

            naughts.NextTurn = () => new BoardPosition(2, 2);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(3, 2);
            game.PlayTurn();

            naughts.NextTurn = () => new BoardPosition(3, 3);
            game.PlayTurn();

            Assert.That(game.IsFinished());
            Assert.That(game.Winner, Is.EqualTo(naughts));
        }

        [Test]
        public void ThreeInARightToLeftDiagonalWins()
        {
            var naughts = new PlayerStub();
            var crosses = new PlayerStub();
            var game = new TicTacToeGame(naughts, crosses);

            naughts.NextTurn = () => new BoardPosition(1, 3);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(1, 2);
            game.PlayTurn();

            naughts.NextTurn = () => new BoardPosition(2, 2);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(3, 2);
            game.PlayTurn();

            naughts.NextTurn = () => new BoardPosition(3, 1);
            game.PlayTurn();

            Assert.That(game.IsFinished());
            Assert.That(game.Winner, Is.EqualTo(naughts));
        }

        [Test]
        public void WhenBoardIsFullButNobodyHasWonPlayersDraw()
        {
            var naughts = new PlayerStub();
            var crosses = new PlayerStub();
            var game = new TicTacToeGame(naughts, crosses);

            naughts.NextTurn = () => new BoardPosition(1, 1);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(1, 2);
            game.PlayTurn();

            naughts.NextTurn = () => new BoardPosition(1, 3);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(2, 2);
            game.PlayTurn();

            naughts.NextTurn = () => new BoardPosition(2, 1);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(2, 3);
            game.PlayTurn();

            naughts.NextTurn = () => new BoardPosition(3, 2);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(3, 1);
            game.PlayTurn();

            naughts.NextTurn = () => new BoardPosition(3, 3);
            game.PlayTurn();

            Assert.That(game.IsFinished());
            Assert.That(game.Winner, Is.EqualTo(null));
        }

        [Test]
        public void WhenGameIsOverStopPlaying()
        {
            var naughts = new PlayerStub();
            var crosses = new PlayerStub();
            var game = new TicTacToeGame(naughts, crosses);

            naughts.NextTurn = () => new BoardPosition(1, 1);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(2, 1);
            game.PlayTurn();

            naughts.NextTurn = () => new BoardPosition(1, 2);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(2, 2);
            game.PlayTurn();

            naughts.NextTurn = () => new BoardPosition(1, 3);
            game.PlayTurn();

            crosses.NextTurn = () => new BoardPosition(2, 3);
            Assert.Throws<TicTacToeGameOverException>(() => game.PlayTurn());
            Assert.That(game.IsFinished());
            Assert.That(game.Winner, Is.EqualTo(naughts));
        }
    }

    public class PlayerStub : ITicTacToePlayer
    {
        public BoardPosition PlayTurn()
        {
            return NextTurn();
        }

        public Func<BoardPosition> NextTurn { get; set; }
    }

    public class TicTacToeGame
    {
        private readonly ITicTacToePlayer _naughts;
        private readonly ITicTacToePlayer _crosses;
        private ITicTacToePlayer _currentPlayer;
        private readonly TicTacToeBoard _board;

        public TicTacToeGame(ITicTacToePlayer naughts, ITicTacToePlayer crosses)
        {
            _naughts = naughts;
            _crosses = crosses;
            _board = new TicTacToeBoard();

            _currentPlayer = _naughts;
        }

        public bool IsFinished()
        {
            return _board.IsComplete();
        }

        public void PlayTurn()
        {
            if(IsFinished())
                throw new TicTacToeGameOverException();

            BoardPosition position = _currentPlayer.PlayTurn();
            if(_board.GetPlayerAtPosition(position) == null)
            {
                _board.PlaySquare(position, _currentPlayer);
                _currentPlayer = _currentPlayer == _naughts ? _crosses : _naughts;
            }
        }

        public ITicTacToePlayer Winner
        {
            get { return _board.Winner; }
        }

        public ITicTacToePlayer PlayerOnSquare(int row, int column)
        {
            return _board.GetPlayerAtPosition(new BoardPosition(row, column));
        }
    }

    internal class TicTacToeBoard : IEnumerable<ITicTacToePlayer>
    {
        private readonly BoardPosition _topLeft = new BoardPosition(1, 1);
        private readonly BoardPosition _bottomRight = new BoardPosition(3, 3);

        private readonly Dictionary<BoardPosition, ITicTacToePlayer> _board = new Dictionary<BoardPosition, ITicTacToePlayer>();
        private ITicTacToePlayer _winner;

        public void PlaySquare(BoardPosition position, ITicTacToePlayer player)
        {
            _board.Add(position, player);

            if (HasMadeWinningMove(player))
                _winner = player;
        }

        private bool HasMadeWinningMove(ITicTacToePlayer player)
        {
            if (_board.Count < 5) return false;

            var winEvaluator = new BinaryMaskWinEvaluator();
            return winEvaluator.HasPlayerWon(GetPositionsForPlayer(player));
        }

        public ITicTacToePlayer GetPlayerAtPosition(int row, int column)
        {
            return GetPlayerAtPosition(new BoardPosition(row, column));
        }

        public ITicTacToePlayer GetPlayerAtPosition(BoardPosition position)
        {
            return _board.ContainsKey(position) ? _board[position] : null;
        }

        public ITicTacToePlayer Winner
        {
            get { return _winner; }
        }

        public bool IsComplete()
        {
            return _winner != null || _board.Count >= 9;
        }
       

        public IEnumerable<bool> GetPositionsForPlayer(ITicTacToePlayer currentPlayer)
        {
            foreach (var player in this)
            {
                yield return player == currentPlayer;
            }
        }

        public IEnumerator<ITicTacToePlayer> GetEnumerator()
        {
            for (var y = _topLeft.Row; y <= _bottomRight.Row; y++)
            {
                for (var x = _topLeft.Column; x <= _bottomRight.Column; x++)
                {
                    yield return GetPlayerAtPosition(x, y);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

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

    internal class BinaryMaskWinEvaluator
    {
        private readonly int[] _wins = new[]
            {
                // Did this because there is no binary literal 
                // and I wanted it to be easy to read
                Convert.ToInt32("111000000",2),
                Convert.ToInt32("000111000",2),
                Convert.ToInt32("000000111",2),
                Convert.ToInt32("100100100",2),
                Convert.ToInt32("010010010",2),
                Convert.ToInt32("001001001",2),
                Convert.ToInt32("100010001",2),
                Convert.ToInt32("001010100",2)
            };

        public bool HasPlayerWon(IEnumerable<bool> playersPositions)
        {
            var playerMask = GetPositionMaskForPlayer(playersPositions);
            foreach (var win in _wins)
            {
                if ((win & playerMask) == win)
                    return true;
            }
            return false;
        }

        private int GetPositionMaskForPlayer(IEnumerable<bool> playersPositions)
        {
            var positionMask = 0;
            var gridIndex = 1;
            foreach (var hasPosition in playersPositions)
            {
                if (hasPosition)
                    positionMask |= 1 << (9 - gridIndex);
                gridIndex++;
            }
            return positionMask;
        }
    }

    public interface ITicTacToePlayer
    {
        BoardPosition PlayTurn();
    }
}
