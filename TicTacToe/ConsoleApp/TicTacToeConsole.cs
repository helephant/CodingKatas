using System;
using TicTacToe.Game;
using TicTacToe.Players;

namespace TicTacToe.ConsoleApp
{
    public class TicTacToeConsole
    {
        public static void Main(string[] args)
        {
            var naughts = new RandomPlayer();
            var crosses = new RandomPlayer();
            var game = new TicTacToeGame(naughts, crosses);

            var printer = new GameBoardPrinter(game);
            while (!game.IsFinished)
            {
                game.PlayTurn();

                Console.WriteLine(printer.Board());
                Console.ReadLine();
            }

            Console.WriteLine("Game over!");
            Console.WriteLine(printer.WinnerMessage());
            Console.ReadLine();
        }
    }
}
