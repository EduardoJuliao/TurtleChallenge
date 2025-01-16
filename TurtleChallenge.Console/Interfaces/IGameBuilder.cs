using TurtleChallenge.Console.Entities;

namespace TurtleChallenge.Console.Interfaces
{
    public interface IGameBuilder
    {
        GameObject[,] Board { get; }
        IGameBuilder SetTurtlePosition(GameObject input);
        IGameBuilder SetExitPoint(GameObject input);
        IGameBuilder SetMines(GameObject[] mines);
        IGame Build();
    }
}
