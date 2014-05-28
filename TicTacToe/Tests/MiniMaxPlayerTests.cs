using NUnit.Framework;
using TicTacToe.Game;
using TicTacToe.Players;
using TicTacToe.Tests.Stubs;

namespace TicTacToe.Tests
{
    [TestFixture]
    public class MiniMaxPlayerTests
    {
        [Test]
        public void MakeWinningMove()
        {
            var naughts = new TurnByTurnPlayerStub();
            var crosses = new MiniMaxPlayer(naughts); 
            var board = new TicTacToeBoard(new ITicTacToePlayer[]
            {
                naughts, null, crosses,
                null, naughts, null,
                naughts, null, crosses
            });

            var position = crosses.PlayTurn(board);
            Assert.That(position, Is.EqualTo(new BoardPosition(2, 3)));
        }

        [Test]
        public void BlockOpponentsWinningMove()
        {
            var naughts = new TurnByTurnPlayerStub();
            var crosses = new MiniMaxPlayer(naughts); 
            var board = new TicTacToeBoard(new ITicTacToePlayer[]
            {
                crosses, null, null,
                naughts, naughts, null,
                null, null, null
            });

            var position = crosses.PlayTurn(board);
            Assert.That(position, Is.EqualTo(new BoardPosition(2, 3)));
        }
    }
}