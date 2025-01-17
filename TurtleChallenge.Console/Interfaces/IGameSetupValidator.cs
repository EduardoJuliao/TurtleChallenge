using TurtleChallenge.Console.Entities;
using TurtleChallenge.Console.Enums;

namespace TurtleChallenge.Console.Interfaces
{
    public interface IGameSetupValidator
    {
        void CheckForOutOfBounds(GameObject[,] board, GameObject input);
        void CheckForUniqueGameObjectType(GameObject[,] board, GameObject input);
        void CheckForValidPlacement(GameObject[,] board, GameObject input);
        void CheckValidBoardSize(int sizeX, int sizeY);
        void CheckForMissingGamingObject(GameObject[,] board, GameObjectType gameObjectType);
    }
}
