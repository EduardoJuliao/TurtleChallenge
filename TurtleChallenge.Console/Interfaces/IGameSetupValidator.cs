using TurtleChallenge.Console.Entities;

namespace TurtleChallenge.Console.Interfaces
{
    public interface IGameSetupValidator
    {
        void CheckForOutOfBounds(GameObject[,] board, GameObject input);
        void CheckForUniqueGameObjectType(GameObject[,] board, GameObject input);
        void CheckForValidPlacement(GameObject[,] board, GameObject input);
        void CheckValidBoardSize(int sizeX, int sizeY);
    }
}
