using System.Collections.Generic;
using TicTacToe.Game;
using TicTacToe.Players;

namespace TicTacToe.Tests.Stubs
{
    public class PrepopulatedPlayerStub : ITicTacToePlayer
    {
        private readonly IEnumerator<BoardPosition> _moves;

        // I know I could have used a mocking framework but my requirements were pretty simple.
        // I think mocking frameworks put a lot of noise and ugly code into your tests.
        public PrepopulatedPlayerStub(IEnumerable<BoardPosition> positions)
        {
            _moves = positions.GetEnumerator();
        }

        public BoardPosition PlayTurn(TicTacToeBoard boardStatus)
        {
            _moves.MoveNext();
            return _moves.Current;
        }
    }
}