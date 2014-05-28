using System;
using NUnit.Framework;
using TicTacToe.ConsoleApp;
using TicTacToe.Game;
using TicTacToe.Players;

namespace TicTacToe.Tests
{
    [TestFixture]
    public class ConsolePlayerTests
    {
        [Test]
        public void ReadsValidTurnFromConsoleInput()
        {
            var player = new ConsolePlayer(new ConsoleInputStub("3,2"));

            Assert.That(player.PlayTurn(null), Is.EqualTo(new BoardPosition(3, 2)));
        }

    }

    public class ConsoleInputStub : IConsoleInput
    {
        private readonly string _input;

        public ConsoleInputStub(string input)
        {
            _input = input;
        }

        public string Read()
        {
            return _input;
        }
    }
}
