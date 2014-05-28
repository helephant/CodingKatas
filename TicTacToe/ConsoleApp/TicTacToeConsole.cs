using System;
using TicTacToe.Game;
using TicTacToe.Players;

namespace TicTacToe.ConsoleApp
{
    public class TicTacToeConsole
    {
        public static void Main(string[] args)
        {
            var naughts = new ConsolePlayer(new ConsoleInput());
            var crosses = new MiniMaxPlayer(naughts);
            var game = new TicTacToeGame(naughts, crosses);

            var printer = new GameBoardPrinter(game);
            Console.WriteLine("Welcome to Helen's TicTacToe game!");
            Console.WriteLine(printer.Board());

            while (!game.IsFinished)
            {
                Console.WriteLine("It's " + printer.NameForPlayer(game.CurrentPlayer) + "'s turn!");
                game.PlayTurn();

                Console.WriteLine(printer.Board());
            }

            Console.WriteLine("Game over!");
            Console.WriteLine(printer.WinnerMessage());
            Console.ReadLine();
        }
    }
}
