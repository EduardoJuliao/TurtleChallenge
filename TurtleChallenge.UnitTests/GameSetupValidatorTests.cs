using TurtleChallenge.Console.Entities;
using TurtleChallenge.Console.Enums;
using TurtleChallenge.Console.Interfaces;
using TurtleChallenge.Console.Messages;
using TurtleChallenge.Console.Validators;

namespace TurtleChallenge.UnitTests
{
    [Trait("Category", "Unit")]
    [Trait("Category", "Validator")]
    public class GameSetupValidatorTests
    {
        IGameSetupValidator _gameSetupValidator;
        GameObject[,] _mockupBoard = new GameObject[5, 4];

        public GameSetupValidatorTests()
        {
            _gameSetupValidator = new GameSetupValidator();
        }

        [Trait("Category", "Innitialize")]
        [Theory]
        [InlineData(-1, 0)]
        [InlineData(-1, -1)]
        [InlineData(0, -1)]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        public void ThrowsException_OnCreatingBoard_InvalidArraySize(int invalidX, int invalidY)
        {
            // Arrange
            void setupBoard() => _gameSetupValidator.CheckValidBoardSize(invalidX, invalidY);

            // Act
            var exception = Assert.Throws<ArgumentOutOfRangeException>(setupBoard);

            // Assert
            var expectedMessage = string.Format(Message.InvalidBoardSize, invalidX, invalidY);
            Assert.Equal($"Specified argument was out of the range of valid values. (Parameter '{expectedMessage}')", exception.Message);
        }

        [Theory]
        [InlineData(6, 4)]
        [InlineData(5, 5)]
        [InlineData(-1,4)]
        [InlineData(5,-1)]
        public void ThrowsException_OnSettingGameObject_WhenSlotIsOutOfBounds(int x, int y)
        {
            // Arrange
            var input = new GameObject
            {
                PositionX = x,
                PositionY = y
            };

            // Act
            void setGameObjectAct() => _gameSetupValidator.CheckForOutOfBounds(_mockupBoard, input);

            // Assert
            var exception = Assert.Throws<IndexOutOfRangeException>(setGameObjectAct);
            Assert.Equal(string.Format(Message.GameObjectOutsideTheBoard, x, y), exception.Message);
        }

        [Theory]
        [InlineData(GameObjectType.Turtle)]
        [InlineData(GameObjectType.ExitPoint)]
        public void ThrowsException_OnSettingUniqueGameObject_WhenNewGameObjectIsSameType(GameObjectType existingGameObjectTypeInSlot)
        {
            // Arrange
            var x = 1;
            var y = 1;

            var input = new GameObject
            {
                PositionX = x,
                PositionY = y,
                GameObjectType = existingGameObjectTypeInSlot
            };

            _mockupBoard[x, y] = new GameObject
            {
                GameObjectType = existingGameObjectTypeInSlot
            };

            // Act
            void setGameObjectAct() => _gameSetupValidator.CheckForUniqueGameObjectType(_mockupBoard, input);

            // Assert
            var exception = Assert.Throws<InvalidOperationException>(setGameObjectAct);
            Assert.Equal(string.Format(Message.UniqueGameObjectAlreadyExists, x, y, _mockupBoard[x, y].GameObjectType), exception.Message);
        }

        [Theory]
        [InlineData(GameObjectType.Turtle)]
        [InlineData(GameObjectType.ExitPoint)]
        [InlineData(GameObjectType.Mine)]
        public void ThrowsException_OnSettingGameObject_WhenSlotIsOccupied(GameObjectType gameObjectType)

        {
            // Arrange
            var x = 1;
            var y = 1;

            var input = new GameObject
            {
                PositionX = x,
                PositionY = y,
                GameObjectType = gameObjectType
            };

            _mockupBoard[x, y] = new GameObject
            {
                GameObjectType = GameObjectType.Mine
            };

            // Act
            void setGameObjectAct() => _gameSetupValidator.CheckForValidPlacement(_mockupBoard, input);

            // Assert
            var exception = Assert.Throws<Exception>(setGameObjectAct);
            Assert.Equal(string.Format(Message.SlotAlreadyInUse, x, y, _mockupBoard[x, y].GameObjectType), exception.Message);
        }



        [Theory]
        [InlineData(GameObjectType.Turtle)]
        [InlineData(GameObjectType.ExitPoint)]
        [InlineData(GameObjectType.Mine)]
        public void DoesNotThrowException_OnSettingGameObject_WhenSlotIsEmpty(GameObjectType gameObjectType)
        {
            // Arrange
            var x = 1;
            var y = 1;

            var input = new GameObject
            {
                PositionX = x,
                PositionY = y,
                GameObjectType = gameObjectType
            };

            // Act
            void setGameObjectAct() => _gameSetupValidator.CheckForValidPlacement(_mockupBoard, input);

            // Assert
            Assert.Null(Record.Exception(setGameObjectAct));
        }
    }
}
