using System;
using NUnit.Framework;

namespace TicTacToe
{
    [TestFixture]
    public class TicTacToeTests
    {
        private PlayerStub _naughts;
        private PlayerStub _crosses;
        private TicTacToeGame _game;

        [SetUp]
        public void Setup()
        {
            _naughts = new PlayerStub("Naughts");
            _crosses = new PlayerStub("Crosses");
            _game = new TicTacToeGame(_naughts, _crosses);
        }

        [Test]
        public void NaughtsStartsTheGame()
        {
            _naughts.NextTurn = () => new BoardPosition(1, 1);
            _game.PlayTurn();

            Assert.That(_game.PlayerOnSquare(1, 1), Is.EqualTo(_naughts));
        }

        [Test]
        public void PlayersAlternateTurns()
        {
            _naughts.NextTurn = () => new BoardPosition(1, 1);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(2, 2);
            _game.PlayTurn();

            Assert.That(_game.PlayerOnSquare(2, 2), Is.EqualTo(_crosses));
        }

        [Test]
        public void PlayersMustMoveWithinTheTopLeftBoundsOfTheBoard()
        {
            _naughts.NextTurn = () => new BoardPosition(0, 0);
            Assert.Throws<InvalidMoveException>(() => _game.PlayTurn());
        }

        [Test]
        public void PlayersMustMoveWithinTheBottomRightBoundsOfTheBoard()
        {
            _naughts.NextTurn = () => new BoardPosition(4, 4);
            Assert.Throws<InvalidMoveException>(() =>_game.PlayTurn());
        }

        [Test]
        public void OnlyOnePlayerCanClaimEachSquare()
        {
            _naughts.NextTurn = () => new BoardPosition(1, 1);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(1, 1);
            _game.PlayTurn();

            Assert.That(_game.PlayerOnSquare(1, 1), Is.EqualTo(_naughts));
        }

        [Test]
        public void PlayersTurnIsNotOverUntilTheyMakeAValidMove()
        {
            _naughts.NextTurn = () => new BoardPosition(1, 1);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(1, 1);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(2, 2);
            _game.PlayTurn();

            Assert.That(_game.PlayerOnSquare(1, 1), Is.EqualTo(_naughts));
            Assert.That(_game.PlayerOnSquare(2, 2), Is.EqualTo(_crosses));
        }

        [Test]
        public void ThreeInAHorizontalRowWins()
        {
            _naughts.NextTurn = () => new BoardPosition(1, 1);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(2, 1);
            _game.PlayTurn();

            _naughts.NextTurn = () => new BoardPosition(1, 2);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(2, 2);
            _game.PlayTurn();

            _naughts.NextTurn = () => new BoardPosition(1, 3);
            _game.PlayTurn();

            Assert.That(_game.IsFinished);
            Assert.That(_game.Winner, Is.EqualTo(_naughts));
        }

        [Test]
        public void ThreeInAVerticalColumnWins()
        {
            _naughts.NextTurn = () => new BoardPosition(1, 2);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(1, 1);
            _game.PlayTurn();

            _naughts.NextTurn = () => new BoardPosition(2, 2);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(2, 1);
            _game.PlayTurn();

            _naughts.NextTurn = () => new BoardPosition(3, 2);
            _game.PlayTurn();

            Assert.That(_game.IsFinished);
            Assert.That(_game.Winner, Is.EqualTo(_naughts));
        }

        [Test]
        public void ThreeInALeftToRightDiagonalWins()
        {
            _naughts.NextTurn = () => new BoardPosition(1, 1);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(1, 2);
            _game.PlayTurn();

            _naughts.NextTurn = () => new BoardPosition(2, 2);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(3, 2);
            _game.PlayTurn();

            _naughts.NextTurn = () => new BoardPosition(3, 3);
            _game.PlayTurn();

            Assert.That(_game.IsFinished);
            Assert.That(_game.Winner, Is.EqualTo(_naughts));
        }

        [Test]
        public void ThreeInARightToLeftDiagonalWins()
        {
            _naughts.NextTurn = () => new BoardPosition(1, 3);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(1, 2);
            _game.PlayTurn();

            _naughts.NextTurn = () => new BoardPosition(2, 2);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(3, 2);
            _game.PlayTurn();

            _naughts.NextTurn = () => new BoardPosition(3, 1);
            _game.PlayTurn();

            Assert.That(_game.IsFinished);
            Assert.That(_game.Winner, Is.EqualTo(_naughts));
        }

        [Test]
        public void WhenBoardIsFullButNobodyHasWonPlayersDraw()
        {
            _naughts.NextTurn = () => new BoardPosition(1, 1);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(1, 2);
            _game.PlayTurn();

            _naughts.NextTurn = () => new BoardPosition(1, 3);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(2, 2);
            _game.PlayTurn();

            _naughts.NextTurn = () => new BoardPosition(2, 1);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(2, 3);
            _game.PlayTurn();

            _naughts.NextTurn = () => new BoardPosition(3, 2);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(3, 1);
            _game.PlayTurn();

            _naughts.NextTurn = () => new BoardPosition(3, 3);
            _game.PlayTurn();

            Assert.That(_game.IsFinished);
            Assert.That(_game.Winner, Is.EqualTo(null));
        }

        [Test]
        public void WhenGameIsOverStopPlaying()
        {
            _naughts.NextTurn = () => new BoardPosition(1, 1);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(2, 1);
            _game.PlayTurn();

            _naughts.NextTurn = () => new BoardPosition(1, 2);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(2, 2);
            _game.PlayTurn();

            _naughts.NextTurn = () => new BoardPosition(1, 3);
            _game.PlayTurn();

            _crosses.NextTurn = () => new BoardPosition(2, 3);
            Assert.Throws<GameOverException>(() => _game.PlayTurn());
            Assert.That(_game.IsFinished);
            Assert.That(_game.Winner, Is.EqualTo(_naughts));
        }
    }

    public class PlayerStub : ITicTacToePlayer
    {
        private readonly string _name;

        public PlayerStub(string name)
        {
            _name = name;
        }

        public BoardPosition PlayTurn()
        {
            return NextTurn();
        }

        public Func<BoardPosition> NextTurn { get; set; }

        public override string ToString()
        {
            return "TicTacToePlayer: " + _name;
        }
    }
}
