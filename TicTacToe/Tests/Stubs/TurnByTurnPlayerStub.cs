using System;
using TicTacToe.Game;
using TicTacToe.Players;

namespace TicTacToe.Tests.Stubs
{
    public class TurnByTurnPlayerStub : ITicTacToePlayer
    {
        // I know I could have used a mocking framework but my requirements were pretty simple.
        // I think mocking frameworks put a lot of noise and ugly code into your tests.
        public TurnByTurnPlayerStub()
        {
        }

        public BoardPosition PlayTurn()
        {
            return NextTurn();
        }

        public void Next(int column, int row)
        {
            NextTurn = () => new BoardPosition(column, row);
        }

        public Func<BoardPosition> NextTurn { get; set; }
    }
}