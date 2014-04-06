using TicTacToe.Players;

namespace TicTacToe.Game.WinEvaluators
{
    public interface IWinEvaluator
    {
        bool HasPlayerWon(ITicTacToePlayer currentPlayer, TicTacToeBoard board);
    }
}