using TurtleChallenge.Console.Enums;

namespace TurtleChallenge.Console.Entities
{
    public class GameObject
    {
        public GameObject()
        {
            
        }

        public GameObject(int positionX, int positionY, GameObjectType gameObjectType = GameObjectType.Empty, Direction direction = Direction.North)
        {
            PositionX = positionX;
            PositionY = positionY;
            GameObjectType = gameObjectType;
            Direction = direction;
        }

        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public GameObjectType GameObjectType { get; set; }
        public Direction Direction { get; set; } = Direction.North;
    }
}
