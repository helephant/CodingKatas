using NUnit.Framework;
using TicTacToe.Game;
using TicTacToe.Players;

namespace TicTacToe.Tests
{
    [TestFixture]
    public class MiniMaxPlayerTests
    {
        [Test]
        public void CanMakeWinningMove()
        {
            var naughts = new MiniMaxPlayer();
            var crosses = new MiniMaxPlayer();
            var board = new TicTacToeBoard(new []
                {
                    naughts, null, naughts,
                    crosses, crosses, null
                });

            var position = naughts.PlayTurn(board);
            Assert.That(position, Is.EqualTo(new BoardPosition(1, 2)));
        }
    }
}
