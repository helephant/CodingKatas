using TicTacToe.Game;

namespace TicTacToe.Players
{
    public interface ITicTacToePlayer
    {
        BoardPosition PlayTurn();
    }
}