using TurtleChallenge.Console;
using TurtleChallenge.Console.Entities;
using TurtleChallenge.Console.Enums;
using TurtleChallenge.Console.Interfaces;
using TurtleChallenge.Console.Messages;
using TurtleChallenge.Console.Validators;

namespace TurtleChallenge.UnitTests
{
    [Trait("Category", "Unit")]
    [Trait("Class", "Game")]
    public class GameTests
    {
        private readonly IGame _game;

        // In this test, we will test the game with the following setup:
        // T: Turtle
        // M: Mine
        // E: Exit Point
        // Blank: Empty Slot
        // +---+---+---+---+---+
        // | T |   |   |   |   |
        // +---+---+---+---+---+
        // |   | M |   |   |   |
        // +---+---+---+---+---+
        // |   |   | M |   | E |
        // +---+---+---+---+---+
        // |   |   |   | M |   |
        // +---+---+---+---+---+
        public GameTests()
        {
            var gameBuilder = new GameBuilder(5, 4, new GameSetupValidator());
            gameBuilder
                .SetMines(
                    [
                        new GameObject(1, 1, GameObjectType.Mine),
                        new GameObject(2, 2, GameObjectType.Mine),
                        new GameObject(3, 3, GameObjectType.Mine)
                    ])
                .SetExitPoint(new GameObject(4, 2, GameObjectType.ExitPoint))
                .SetTurtlePosition(new GameObject(0, 0, GameObjectType.Turtle, Direction.North));


            _game = gameBuilder.Build();
        }

        [Fact]
        public void TurtleIsInDanger_WhenGameIsStarted_AndWithoutMoves()
        {
            // Act
            var result = _game
                .SetMoves([])
                .Play();

            // Assert
            Assert.Equal(GameStatus.StillInDanger, result.GameStatus);
            Assert.Equal(Message.TurtleStillInDanger, result.Message);
        }

        [Theory]
        [InlineData(new MoveInstruction[] { MoveInstruction.Move })]
        [InlineData(new MoveInstruction[] { MoveInstruction.Rotate, MoveInstruction.Rotate, MoveInstruction.Rotate, MoveInstruction.Move })]
        [InlineData(new MoveInstruction[] { MoveInstruction.Rotate, MoveInstruction.Rotate, MoveInstruction.Move, MoveInstruction.Move, MoveInstruction.Move, MoveInstruction.Move })]
        [InlineData(new MoveInstruction[] { MoveInstruction.Rotate, MoveInstruction.Rotate, MoveInstruction.Move, MoveInstruction.Move, MoveInstruction.Rotate, MoveInstruction.Move })]
        [InlineData(new MoveInstruction[] { MoveInstruction.Rotate, MoveInstruction.Move, MoveInstruction.Move, MoveInstruction.Move, MoveInstruction.Move, MoveInstruction.Move })]
        public void TurtleIsInDanger_WhenGameIsStarted_AndMovesAreOutOfBounds(MoveInstruction[] instructions)
        {
            // Act
            var result = _game
                .SetMoves(instructions)
                .Play();

            // Assert
            Assert.Equal(GameStatus.OutOfBounds, result.GameStatus);
            Assert.Equal(Message.TurtleIsOutOfBounds, result.Message);
        }

        [Fact]
        public void GameStops_WhenTurtleHitsAMine_AndThereAreMoreMoves()
        {
            // Arrange
            var moves = new List<MoveInstruction>
            {
                MoveInstruction.Rotate,
                MoveInstruction.Move,
                MoveInstruction.Rotate,
                MoveInstruction.Move,
                MoveInstruction.Rotate,
                MoveInstruction.Move,
                MoveInstruction.Rotate,
                MoveInstruction.Move,
            };

            // Act
            var result = _game
                .SetMoves(moves)
                .Play();

            // Assert
            Assert.Equal(GameStatus.MineHit, result.GameStatus);
            Assert.Equal(Message.TurtleHasHitAMine, result.Message);
        }

        [Theory]
        // Test Cases Scenarios
        // The turtle will move and hit the first mine
        // +---+---+---+---+---+
        // |   |   |   |   |   |
        // +---+---+---+---+---+
        // |   |T M|   |   |   |
        // +---+---+---+---+---+
        // |   |   | M |   | E |
        // +---+---+---+---+---+
        // |   |   |   | M |   |
        // +---+---+---+---+---+
        [InlineData(new MoveInstruction[] { MoveInstruction.Rotate, MoveInstruction.Move, MoveInstruction.Rotate, MoveInstruction.Move })]
        // The turtle will move and hit the second mine
        // +---+---+---+---+---+
        // |   |   |   |   |   |
        // +---+---+---+---+---+
        // |   | M |   |   |   |
        // +---+---+---+---+---+
        // |   |   |T M|   | E |
        // +---+---+---+---+---+
        // |   |   |   | M |   |
        // +---+---+---+---+---+
        [InlineData(new MoveInstruction[] { MoveInstruction.Rotate, MoveInstruction.Move, MoveInstruction.Move, MoveInstruction.Rotate, MoveInstruction.Move, MoveInstruction.Move })]
        // The turtle will move and hit the third mine
        // +---+---+---+---+---+
        // |   |   |   |   |   |
        // +---+---+---+---+---+
        // |   | M |   |   |   |
        // +---+---+---+---+---+
        // |   |   | M |   | E |
        // +---+---+---+---+---+
        // |   |   |   |T M|   |
        // +---+---+---+---+---+
        [InlineData(new MoveInstruction[] {
            MoveInstruction.Rotate,
            MoveInstruction.Move,
            MoveInstruction.Move,
            MoveInstruction.Move,
            MoveInstruction.Rotate,
            MoveInstruction.Move,
            MoveInstruction.Move,
            MoveInstruction.Move,
        })]
        public void TurtleHitsAMine_WhenGameIsStarted_AndTurtleMovesToMine(MoveInstruction[] instructions)
        {
            // Act
            var result = _game
                .SetMoves(instructions)
                .Play();

            // Assert
            Assert.Equal(GameStatus.MineHit, result.GameStatus);
            Assert.Equal(Message.TurtleHasHitAMine, result.Message);
        }

        [Fact]
        // +---+---+---+---+---+
        // |   |   |   |   |   |
        // +---+---+---+---+---+
        // |   | M | T |   |   |
        // +---+---+---+---+---+
        // |   |   | M |   | E |
        // +---+---+---+---+---+
        // |   |   |   | M |   |
        // +---+---+---+---+---+
        public void TurtleStilInDanger_WhenNoMovesLeftToMake()
        {
            // Arrange
            var moves = new List<MoveInstruction>
            {
                MoveInstruction.Rotate,
                MoveInstruction.Move,
                MoveInstruction.Move,
                MoveInstruction.Rotate,
                MoveInstruction.Move,
            };

            // Act
            var result = _game
                .SetMoves(moves)
                .Play();

            // Assert
            Assert.Equal(GameStatus.StillInDanger, result.GameStatus);
            Assert.Equal(Message.TurtleStillInDanger, result.Message);
        }

        [Fact]
        // +---+---+---+---+---+
        // |   |   |   |   |   |
        // +---+---+---+---+---+
        // |   | M |   |   |   |
        // +---+---+---+---+---+
        // |   |   | M |   |T E|
        // +---+---+---+---+---+
        // |   |   |   | M |   |
        // +---+---+---+---+---+
        public void TurtleReachesExitPoint()
        {
            // Arrange
            var moves = new List<MoveInstruction>
            {
                MoveInstruction.Rotate,
                MoveInstruction.Move,
                MoveInstruction.Move,
                MoveInstruction.Move,
                MoveInstruction.Move,
                MoveInstruction.Rotate,
                MoveInstruction.Move,
                MoveInstruction.Move,
            };

            // Act
            var result = _game
                .SetMoves(moves)
                .Play();

            // Assert
            Assert.Equal(GameStatus.Success, result.GameStatus);
            Assert.Equal(Message.TurtleHasExited, result.Message);
        }
    }
}
