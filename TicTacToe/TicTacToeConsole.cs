using System;
using TicTacToe.Game;
using TicTacToe.Players;

namespace TicTacToe
{
    public class TicTacToeConsole
    {
        public static void Main(string[] args)
        {
            var naughts = new RandomPlayer();
            var crosses = new RandomPlayer();
            var game = new TicTacToeGame(naughts, crosses);

            while (!game.IsFinished)
            {
                game.PlayTurn();

                // print out the board
                PrintBoard(game, naughts, crosses);

                Console.ReadLine();
            }

            Console.WriteLine("Game over!");
            Console.WriteLine("Winner: " + GetPlayerSymbol(game.Winner, naughts, crosses));
            Console.ReadLine();
        }

        private static void PrintBoard(TicTacToeGame game, ITicTacToePlayer naughts, ITicTacToePlayer crosses)
        {
            for (var row = 1; row <= 3; row++)
            {
                Console.WriteLine();
                for (var column = 1; column <= 3; column++)
                {
                    var playerOnSquare = game.PlayerOnSquare(row, column);
                    Console.Write(" | ");
                    Console.Write(GetPlayerSymbol(playerOnSquare, naughts, crosses) + " ");
                }
                Console.Write(" | ");
            }
        }

        private static string GetPlayerSymbol(ITicTacToePlayer playerOnSquare, ITicTacToePlayer naughts, ITicTacToePlayer crosses)
        {
            if (playerOnSquare == naughts)
                return "O";
            if (playerOnSquare == crosses)
                return "X";
            return " ";
        }
    }
}
