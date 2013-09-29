using TicTacToe.Game;
using TicTacToe.Players;

namespace TicTacToe.Tests.Stubs
{
    public class GameBuilder
    {
        private ITicTacToePlayer _naughts;
        private ITicTacToePlayer _crosses;

        public GameBuilder NaughtsWins()
        {
            _naughts = new PrepopulatedPlayerStub(new[]
                {
                    new BoardPosition(1, 1),
                    new BoardPosition(1, 2),
                    new BoardPosition(1, 3),
                });
            _crosses = new PrepopulatedPlayerStub(new[]
                {
                    new BoardPosition(2, 1),
                    new BoardPosition(2, 2),
                });

            return this;
        }

        public GameBuilder PlayersDraw()
        {
            _naughts = new PrepopulatedPlayerStub(new[]
                {
                    new BoardPosition(1, 1),
                    new BoardPosition(1, 3),
                    new BoardPosition(2, 1),
                    new BoardPosition(3, 2),
                    new BoardPosition(3, 3),
                });
            _crosses = new PrepopulatedPlayerStub(new[]
                {
                    new BoardPosition(1, 2),
                    new BoardPosition(2, 2),
                    new BoardPosition(2, 3),
                    new BoardPosition(3, 1),
                });

            return this;
        }

        public TicTacToeGame Build()
        {
            return new TicTacToeGame(_naughts, _crosses);
        }
    }
}