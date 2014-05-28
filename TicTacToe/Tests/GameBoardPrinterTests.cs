using System;
using NUnit.Framework;
using TicTacToe.ConsoleApp;
using TicTacToe.Game;
using TicTacToe.Players;
using TicTacToe.Tests.Stubs;

namespace TicTacToe.Tests
{
    [TestFixture]
    public class GameBoardPrinterTests
    {
        [Test]
        public void PrintGameBoard()
        {
            var game = new GameBuilder().NaughtsWins().Build();
            var printer = new GameBoardPrinter(game);

            game.Play();

            // I think this might be a bit over-specified.. 
            Assert.That(printer.Board(), Is.EqualTo(" | O | O | O | " + Environment.NewLine +
                                                    " | X | X |   | " + Environment.NewLine +
                                                    " |   |   |   | " + Environment.NewLine));
        }

        [Test]
        public void PrintNameForPlayers()
        {
            var naughts = new RandomPlayer();
            var crosses = new RandomPlayer();
            var game = new TicTacToeGame(naughts, crosses);

            var printer = new GameBoardPrinter(game);
            Assert.That(printer.NameForPlayer(naughts), Is.EqualTo("Naughts"));
            Assert.That(printer.NameForPlayer(crosses), Is.EqualTo("Crosses"));
            Assert.That(printer.NameForPlayer(null), Is.EqualTo(null));
        }

        [Test]
        public void PrintSymbolForPlayers()
        {
            var naughts = new RandomPlayer();
            var crosses = new RandomPlayer();
            var game = new TicTacToeGame(naughts, crosses);

            var printer = new GameBoardPrinter(game);
            Assert.That(printer.SymbolForPlayer(naughts), Is.EqualTo("O"));
            Assert.That(printer.SymbolForPlayer(crosses), Is.EqualTo("X"));
            Assert.That(printer.SymbolForPlayer(null), Is.EqualTo(" "));
        }

        [Test]
        public void MessageIfPlayerHasWon()
        {
            var game = new GameBuilder().NaughtsWins().Build();

            game.Play();

            var printer = new GameBoardPrinter(game);
            Assert.That(printer.WinnerMessage(), Is.EqualTo("The winner is Naughts."));
        }

        [Test]
        public void MessageIfPlayersDraw()
        {
            var game = new GameBuilder().PlayersDraw().Build();

            game.Play();

            var printer = new GameBoardPrinter(game);
            Assert.That(printer.WinnerMessage(), Is.EqualTo("No winner, players have drawn."));
        }
    }
}
