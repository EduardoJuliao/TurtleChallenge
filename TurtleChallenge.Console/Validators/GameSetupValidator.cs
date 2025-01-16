using TurtleChallenge.Console.Entities;
using TurtleChallenge.Console.Enums;
using TurtleChallenge.Console.Interfaces;
using TurtleChallenge.Console.Messages;

namespace TurtleChallenge.Console.Validators
{
    public class GameSetupValidator : IGameSetupValidator
    {
        public void CheckValidBoardSize(int sizeX, int sizeY)
        {
            if (sizeX <= 0 || sizeY <= 0)
                throw new ArgumentOutOfRangeException(string.Format(Message.InvalidBoardSize, sizeX, sizeY));
        }

        public void CheckForOutOfBounds(GameObject[,] board, GameObject input)
        {
            var x = input.PositionX;
            var y = input.PositionY;
            if (x < 0 || x >= board.GetLength(0) || y < 0 || y >= board.GetLength(1))
            {
                throw new IndexOutOfRangeException(string.Format(Message.GameObjectOutsideTheBoard, x, y));
            }
        }

        public void CheckForUniqueGameObjectType(GameObject[,] board, GameObject input)
        {
            var x = input.PositionX;
            var y = input.PositionY;

            if (board.Cast<GameObject>().Where(x => x != null).Select(x => x.GameObjectType).Contains(input.GameObjectType))
            {
                throw new InvalidOperationException(string.Format(Message.UniqueGameObjectAlreadyExists, x, y, board[x, y].GameObjectType));
            }
        }

        public void CheckForValidPlacement(GameObject[,] board, GameObject input)
        {
            var x = input.PositionX;
            var y = input.PositionY;

            if (board[x, y] != null && board[x, y].GameObjectType != GameObjectType.Empty)
            {
                throw new Exception(string.Format(Message.SlotAlreadyInUse, x, y, board[x, y].GameObjectType));
            }
        }
    }
}
