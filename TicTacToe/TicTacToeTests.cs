using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;

namespace TicTacToe
{
    [TestFixture]
    public class TicTacToeTests
    {
        [Test]
        public void PlayUntilPlayerHasWon()
        {
            var naughts = new PlayerStub();
            var game = new TicTacToeGame(naughts, new PlayerStub());
            while (!game.IsFinished())
            {
                game.PlayTurn();
            }

            Assert.That(game.Winner, Is.EqualTo(naughts));
        }

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
        private TicTacToeBoard _board;

        public TicTacToeGame(ITicTacToePlayer naughts, ITicTacToePlayer crosses)
        {
            _naughts = naughts;
            _crosses = crosses;
            _board = new TicTacToeBoard();

            _currentPlayer = _naughts;
        }

        public bool IsFinished()
        {
            return true;
        }

        public void PlayTurn()
        {
            Point position = _currentPlayer.PlayTurn();
            if(_board.GetPlayer(position) == null)
            {
                _board.PlaySquare(position, _currentPlayer);
                _currentPlayer = _currentPlayer == _naughts ? _crosses : _naughts;
            }
        }

        public ITicTacToePlayer Winner
        {
            get { return null; }
        }

        public ITicTacToePlayer PlayerOnSquare(int x, int y)
        {
            return _board.GetPlayer(new Point(x, y));
        }
    }

    internal class TicTacToeBoard
    {
        private Dictionary<Point, ITicTacToePlayer> _board = new Dictionary<Point, ITicTacToePlayer>();

        public void PlaySquare(Point position, ITicTacToePlayer player)
        {
            _board.Add(position, player);
        }



        public ITicTacToePlayer GetPlayer(Point position)
        {
            if(_board.ContainsKey(position))
                return _board[position];
            return null;
        }
    }

    public interface ITicTacToePlayer
    {
        Point PlayTurn();
    }
}
