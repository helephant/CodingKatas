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
            naughts.NextTurn = () => new Point(1, 1);

            var game = new TicTacToeGame(naughts, null);
            game.PlayTurn();

            Assert.That(game.PlayerOnSquare(1, 1), Is.EqualTo(naughts));
        }

        [Test]
        public void PlayersAlternateTurns()
        {
            var naughts = new PlayerStub();
            naughts.NextTurn = () => new Point(1, 1);

            var crosses = new PlayerStub();
            crosses.NextTurn = () => new Point(2, 2);

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

            naughts.NextTurn = () => new Point(1, 1);
            game.PlayTurn();

            crosses.NextTurn = () => new Point(1, 1);
            game.PlayTurn();

            Assert.That(game.PlayerOnSquare(1, 1), Is.EqualTo(naughts));
        }

        [Test]
        public void PlayersTurnIsNotOverUntilTheyMakeAValidMove()
        {
            var naughts = new PlayerStub();
            var crosses = new PlayerStub();
            var game = new TicTacToeGame(naughts, crosses);

            naughts.NextTurn = () => new Point(1, 1);
            game.PlayTurn();

            crosses.NextTurn = () => new Point(1, 1);
            game.PlayTurn();

            crosses.NextTurn = () => new Point(2, 2);
            game.PlayTurn();

            Assert.That(game.PlayerOnSquare(2, 2), Is.EqualTo(crosses));
        }

        [Test]
        public void ThreeInAHorizontalRowWins()
        {
            var naughts = new PlayerStub();
            var crosses = new PlayerStub();
            var game = new TicTacToeGame(naughts, crosses);

            naughts.NextTurn = () => new Point(1, 1);
            game.PlayTurn();

            crosses.NextTurn = () => new Point(2, 1);
            game.PlayTurn();

            naughts.NextTurn = () => new Point(1, 2);
            game.PlayTurn();

            crosses.NextTurn = () => new Point(2, 2);
            game.PlayTurn();

            naughts.NextTurn = () => new Point(1, 3);
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

            naughts.NextTurn = () => new Point(1, 2);
            game.PlayTurn();

            crosses.NextTurn = () => new Point(1, 1);
            game.PlayTurn();

            naughts.NextTurn = () => new Point(2, 2);
            game.PlayTurn();

            crosses.NextTurn = () => new Point(2, 1);
            game.PlayTurn();

            naughts.NextTurn = () => new Point(3, 2);
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

            naughts.NextTurn = () => new Point(1, 1);
            game.PlayTurn();

            crosses.NextTurn = () => new Point(1, 2);
            game.PlayTurn();

            naughts.NextTurn = () => new Point(2, 2);
            game.PlayTurn();

            crosses.NextTurn = () => new Point(3, 2);
            game.PlayTurn();

            naughts.NextTurn = () => new Point(3, 3);
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

            naughts.NextTurn = () => new Point(1, 3);
            game.PlayTurn();

            crosses.NextTurn = () => new Point(1, 2);
            game.PlayTurn();

            naughts.NextTurn = () => new Point(2, 2);
            game.PlayTurn();

            crosses.NextTurn = () => new Point(3, 2);
            game.PlayTurn();

            naughts.NextTurn = () => new Point(3, 1);
            game.PlayTurn();

            Assert.That(game.IsFinished());
            Assert.That(game.Winner, Is.EqualTo(naughts));
        }
    }

    public class PlayerStub : ITicTacToePlayer
    {
        public Point PlayTurn()
        {
            return NextTurn();
        }

        public Func<Point> NextTurn { get; set; }
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
            Point position = _currentPlayer.PlayTurn();
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

        public ITicTacToePlayer PlayerOnSquare(int x, int y)
        {
            return _board.GetPlayerAtPosition(new Point(x, y));
        }
    }

    internal class TicTacToeBoard : IEnumerable<ITicTacToePlayer>
    {
        private readonly Point _topLeft = new Point(1, 1);
        private readonly Point _bottomRight = new Point(3, 3);

        private readonly Dictionary<Point, ITicTacToePlayer> _board = new Dictionary<Point, ITicTacToePlayer>();
        private ITicTacToePlayer _winner;

        public void PlaySquare(Point position, ITicTacToePlayer player)
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
            return GetPlayerAtPosition(new Point(row, column));
        }

        public ITicTacToePlayer GetPlayerAtPosition(Point position)
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
            for (var x = _topLeft.X; x <= _bottomRight.X; x++)
            {
                for (var y = _topLeft.Y; y <= _bottomRight.Y; y++)
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
        Point PlayTurn();
    }
}
