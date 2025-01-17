namespace TurtleChallenge.Console.Messages
{
    public partial class Message
    {
        public const string InvalidBoardSize = "Invalid board size. SizeX: {0}, SizeY: {1}";
        public const string GameObjectOutsideTheBoard = "Invalid position to set up the object position. Slot x: {0}, y: {1} is out of the board bounds.";
        public const string SlotAlreadyInUse = "Invalid position to set up the object position. Slot x: {0}, y: {1} being used by {2}.";
        public const string UniqueGameObjectAlreadyExists = "Invalid position to set up a Unique Game object position. Slot x: {0}, y: {1} being used by {2}.";
        public const string MissingGameObject = "A required Game Object of type {0} is missing from the board.";
    }
}
