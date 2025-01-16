using TurtleChallenge.Console.Enums;

namespace TurtleChallenge.Console.Interfaces
{
    public interface IGame
    {
        IGame SetMoves(IEnumerable<MoveInstruction> moves);
        GameResult Play();
    }
}
