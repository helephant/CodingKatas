using NUnit.Framework;
using TicTacToe.Game;
using TicTacToe.Game.WinEvaluators;
using TicTacToe.Players;
using TicTacToe.Tests.Stubs;

namespace TicTacToe.Tests
{
    [TestFixture]
    public class MiniMaxPlayAllGamesTests
    {
        [Test, Explicit]
        public void PlayerAlwaysWinsOrDrawsIfStartsTheGame()
        {
            var naughts = new TurnByTurnPlayerStub();
            var crosses = new MiniMaxPlayer(naughts);

            var result = PlayAllPossibleGames(new TicTacToeBoard(), crosses, naughts);

            Assert.That(result.Losses, Is.EqualTo(0));
        }

        [Test, Explicit]
        public void PlayerAlwaysWinsOrDrawsIfOtherPlayerStartsTheGame()
        {
            var naughts = new TurnByTurnPlayerStub();
            var crosses = new MiniMaxPlayer(naughts);

            var board = new TicTacToeBoard();
            var tally = ScoreTally.Start;
            foreach (var square in board.VacantSquares)
            {
                var results = PlayAllPossibleGames(board.UpdateBoard(square, naughts), crosses, naughts);
                tally = tally + results;
            }

            Assert.That(tally.Losses, Is.EqualTo(0));
        }

        private ScoreTally PlayAllPossibleGames(TicTacToeBoard currentBoard, ITicTacToePlayer player, ITicTacToePlayer opponent)
        {
            var tally = ScoreTally.Start;

            var position = player.PlayTurn(currentBoard);
            var nextBoard = currentBoard.UpdateBoard(position, player);
            if (new BitMaskWinEvaluator().HasPlayerWon(player, nextBoard))
                return ScoreTally.Win;
            if (nextBoard.IsComplete)
                return ScoreTally.Draw;


            foreach (var boardPosition in nextBoard.VacantSquares)
            {
                var possibleMove = nextBoard.UpdateBoard(boardPosition, opponent);
                if (new BitMaskWinEvaluator().HasPlayerWon(opponent, possibleMove))
                    return ScoreTally.Loss;
                if (possibleMove.IsComplete)
                    return ScoreTally.Draw;

                var result = PlayAllPossibleGames(possibleMove, player, opponent);
                tally = result + tally;
            }

            return tally;
        }

        private class ScoreTally
        {
            public readonly static ScoreTally Start = new ScoreTally(0, 0, 0);
            public readonly static ScoreTally Win = new ScoreTally(1, 0, 0);
            public readonly static ScoreTally Draw = new ScoreTally(0, 1, 0);
            public readonly static ScoreTally Loss = new ScoreTally(0, 0, 1);

            public ScoreTally(int wins, int draws, int loses)
            {
                Wins = wins;
                Draws = draws;
                Losses = loses;
            }

            public int Wins { get; private set; }
            public int Draws { get; private set; }
            public int Losses { get; private set; }

            public static ScoreTally operator +(ScoreTally score1, ScoreTally score2)
            {
                var wins = score1.Wins + score2.Wins;
                var draws = score1.Draws + score2.Draws;
                var losses = score1.Losses + score2.Losses;
                return new ScoreTally(wins, draws, losses);
            }
        }
    }
}