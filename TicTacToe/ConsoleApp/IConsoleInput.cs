using System;

namespace TicTacToe.ConsoleApp
{
    public interface IConsoleInput
    {
        string Read();
    }

    internal class ConsoleInput : IConsoleInput
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}