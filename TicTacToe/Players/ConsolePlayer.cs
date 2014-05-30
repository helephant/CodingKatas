using System;
using TicTacToe.ConsoleApp;
using TicTacToe.Game;

namespace TicTacToe.Players
{
    public class ConsolePlayer : ITicTacToePlayer
    {
        private readonly IConsoleInput _consoleInputStub;

        public ConsolePlayer(IConsoleInput consoleInputStub)
        {
            _consoleInputStub = consoleInputStub;
        }

        public BoardPosition PlayTurn(TicTacToeBoard boardStatus)
        {
            Console.WriteLine("Please enter in the coordinates for where you'd like to go (for example 3,1).");
            var input = _consoleInputStub.Read();
            return ParseTurn(input);
        }

        private BoardPosition ParseTurn(string input)
        {
            // I know, I know! I need to figure out what to do about error handling. 
            var pieces = input.Split(',');
            return new BoardPosition(int.Parse(pieces[0]), int.Parse(pieces[1]));
        }
    }
}