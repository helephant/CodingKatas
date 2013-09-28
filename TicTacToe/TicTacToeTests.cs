using System;
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
}
