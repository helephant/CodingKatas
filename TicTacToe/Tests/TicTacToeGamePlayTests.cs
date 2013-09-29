using NUnit.Framework;
using TicTacToe.Game;
using TicTacToe.Tests.Stubs;

namespace TicTacToe.Tests
{
    [TestFixture]
    public class TicTacToeGamePlayTests
    {
        [Test]
        public void NaughtsStartsTheGame()
        {
            var naughts = new TurnByTurnPlayerStub();
            var game = new TicTacToeGame(naughts, null);

            naughts.Next(1, 1);
            game.PlayTurn();

            Assert.That(game.PlayerOnSquare(1, 1), Is.EqualTo(naughts));
        }

        [Test]
        public void PlayersAlternateTurns()
        {
            var naughts = new TurnByTurnPlayerStub();
            var crosses = new TurnByTurnPlayerStub();
            var game = new TicTacToeGame(naughts, crosses);

            naughts.Next(1, 1);
            game.PlayTurn();

            crosses.Next(2, 2);
            game.PlayTurn();

            Assert.That(game.PlayerOnSquare(2, 2), Is.EqualTo(crosses));
        }

        [Test]
        public void PlayersMustMoveWithinTheTopLeftBoundsOfTheBoard()
        {
            var naughts = new TurnByTurnPlayerStub();
            var game = new TicTacToeGame(naughts, null);

            naughts.Next(0, 0);

            Assert.Throws<InvalidMoveException>(() => game.PlayTurn());
        }

        [Test]
        public void PlayersMustMoveWithinTheBottomRightBoundsOfTheBoard()
        {
            var naughts = new TurnByTurnPlayerStub();
            var game = new TicTacToeGame(naughts, null);

            naughts.Next(4, 4);

            Assert.Throws<InvalidMoveException>(() => game.PlayTurn());
        }

        [Test]
        public void OnlyOnePlayerCanClaimEachSquare()
        {
            var naughts = new TurnByTurnPlayerStub();
            var crosses = new TurnByTurnPlayerStub();
            var game = new TicTacToeGame(naughts, crosses);

            naughts.Next(1, 1);
            game.PlayTurn();

            crosses.Next(1, 1);
            game.PlayTurn();

            Assert.That(game.PlayerOnSquare(1, 1), Is.EqualTo(naughts));
        }

        [Test]
        public void PlayersTurnIsNotOverUntilTheyMakeAValidMove()
        {
            var naughts = new TurnByTurnPlayerStub();
            var crosses = new TurnByTurnPlayerStub();
            var game = new TicTacToeGame(naughts, crosses);

            naughts.Next(1, 1);
            game.PlayTurn();

            crosses.Next(1, 1);
            game.PlayTurn();

            crosses.Next(2, 2);
            game.PlayTurn();

            Assert.That(game.PlayerOnSquare(1, 1), Is.EqualTo(naughts));
            Assert.That(game.PlayerOnSquare(2, 2), Is.EqualTo(crosses));
        }

        [Test]
        public void WhenGameIsOverStopPlaying()
        {
            var game = new GameBuilder().NaughtsWins().Build();
            game.Play();
            Assert.Throws<GameOverException>(() => game.PlayTurn());
        }
    }
}
