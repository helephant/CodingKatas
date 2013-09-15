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
        private TicTacToeBoard _board;

        public TicTacToeGame(ITicTacToePlayer naughts, ITicTacToePlayer crosses)
        {
            _naughts = naughts;
            _crosses = crosses;
            _board = new TicTacToeBoard(); 
        }

        public bool IsFinished()
        {
            return true;
        }

        public void PlayTurn()
        {
            var position = _naughts.PlayTurn();
            _board.PlaySquare(position, _naughts);
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
            return _board[position];
        }
    }

    public interface ITicTacToePlayer
    {
        Point PlayTurn();
    }
}
