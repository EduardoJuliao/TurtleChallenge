using Moq;
using TurtleChallenge.Console;
using TurtleChallenge.Console.Entities;
using TurtleChallenge.Console.Interfaces;

namespace TurtleChallenge.UnitTests
{
    [Trait("Category", "Unit")]
    [Trait("Category", "GameBuilder")]
    public class GameBuilderTests
    {
        private readonly IGameBuilder _gameBuilder;
        private readonly IMock<IGameSetupValidator> _validator;

        public GameBuilderTests()
        {
            _validator = new Mock<IGameSetupValidator>();
            _gameBuilder = new GameBuilder(5, 4, _validator.Object);
        }

        [Fact]
        [Trait("Category", "Innitialize")]
        public void DoesNotThrowException_WhenBoardIsCreatedWithCorrectSize()
        {
            // Arrange
            var expectedWidth = 5;
            var expectedHeight = 4;

            // Act
            var gameBoard = _gameBuilder.Board;
            var actualWidth = gameBoard.GetLength(0);
            var actualHeight = gameBoard.GetLength(1);

            // Assert
            Assert.Equal(expectedWidth, actualWidth);
            Assert.Equal(expectedHeight, actualHeight);
        }

        [Fact]
        [Trait("Category", "Innitialize")]
        public void BoardIsCreated_AndAllValuesAreSetToNull()
        {
            // Arrange

            // Act
            var gameBoard = _gameBuilder.Board;

            // Assert
            Assert.All(gameBoard.Cast<GameObject>(), slot => Assert.Null(slot));
        }
    }
}