using NUnit.Framework;
using TicTacToe.Game;
using TicTacToe.Tests.Stubs;

namespace TicTacToe.Tests
{
    [TestFixture]
    class TicTacToeWinScenarioTests
    {
        [Test]
        public void ThreeInAHorizontalRowWins()
        {
            var naughts = new PrepopulatedPlayerStub(new[]
                {
                    new BoardPosition(1, 1),
                    new BoardPosition(1, 2),
                    new BoardPosition(1, 3),
                });
            var crosses = new PrepopulatedPlayerStub(new[]
                {
                    new BoardPosition(2, 1),
                    new BoardPosition(2, 2),
                });
            var game = new TicTacToeGame(naughts, crosses);

            game.Play();

            Assert.That(game.IsFinished);
            Assert.That(game.Winner, Is.EqualTo(game.Naughts));
        }

        [Test]
        public void ThreeInAVerticalColumnWins()
        {
            var naughts = new PrepopulatedPlayerStub(new[]
                {
                    new BoardPosition(1, 2),
                    new BoardPosition(2, 2),
                    new BoardPosition(3, 2),
                });
            var crosses = new PrepopulatedPlayerStub(new[]
                {
                    new BoardPosition(1, 3),
                    new BoardPosition(2, 3),
                });
            var game = new TicTacToeGame(naughts, crosses);

            game.Play();

            Assert.That(game.IsFinished);
            Assert.That(game.Winner, Is.EqualTo(game.Naughts));
        }

        [Test]
        public void ThreeInALeftToRightDiagonalWins()
        {
            var naughts = new PrepopulatedPlayerStub(new[]
                {
                    new BoardPosition(1, 1),
                    new BoardPosition(2, 2),
                    new BoardPosition(3, 3),
                });
            var crosses = new PrepopulatedPlayerStub(new[]
                {
                    new BoardPosition(1, 2),
                    new BoardPosition(2, 1),
                });
            var game = new TicTacToeGame(naughts, crosses);

            game.Play();

            Assert.That(game.IsFinished);
            Assert.That(game.Winner, Is.EqualTo(game.Naughts));
        }

        [Test]
        public void ThreeInARightToLeftDiagonalWins()
        {
            var naughts = new PrepopulatedPlayerStub(new[]
                {
                    new BoardPosition(1, 3),
                    new BoardPosition(2, 2),
                    new BoardPosition(3, 1),
                });
            var crosses = new PrepopulatedPlayerStub(new[]
                {
                    new BoardPosition(1, 2),
                    new BoardPosition(3, 2),
                });
            var game = new TicTacToeGame(naughts, crosses);

            game.Play();

            Assert.That(game.IsFinished);
            Assert.That(game.Winner, Is.EqualTo(game.Naughts));
        }

        [Test]
        public void WhenBoardIsFullButNobodyHasWonPlayersDraw()
        {
            var naughts = new PrepopulatedPlayerStub(new[]
                {
                    new BoardPosition(1, 1),
                    new BoardPosition(1, 3),
                    new BoardPosition(2, 1),
                    new BoardPosition(3, 2),
                    new BoardPosition(3, 3),
                });
            var crosses = new PrepopulatedPlayerStub(new[]
                {
                    new BoardPosition(1, 2),
                    new BoardPosition(2, 2),
                    new BoardPosition(2, 3),
                    new BoardPosition(3, 1),
                });
            var game = new TicTacToeGame(naughts, crosses);

            game.Play();

            Assert.That(game.IsFinished);
            Assert.That(game.Winner, Is.EqualTo(null));
        }
    }
}
