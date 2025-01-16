using TurtleChallenge.Console.Entities;
using TurtleChallenge.Console.Interfaces;

namespace TurtleChallenge.Console
{
    public class GameBuilder : IGameBuilder
    {
        public GameObject[,] Board { get; }
        private readonly IGameSetupValidator _gameSetupValidator;

        public GameBuilder(int sizeX, int sizeY, IGameSetupValidator gameSetupValidator)
        {
            _gameSetupValidator = gameSetupValidator ?? throw new ArgumentNullException(nameof(gameSetupValidator));
            _gameSetupValidator.CheckValidBoardSize(sizeX, sizeY);
            Board = new GameObject[sizeX, sizeY];
        }

        private void SetGameObject(GameObject input, bool isUnique = true)
        {
            _gameSetupValidator.CheckForOutOfBounds(Board, input);
            if (isUnique)
                _gameSetupValidator.CheckForUniqueGameObjectType(Board, input);
            _gameSetupValidator.CheckForValidPlacement(Board, input);

            var x = input.PositionX;
            var y = input.PositionY;

            Board[x, y] = input;
        }

        public IGameBuilder SetTurtlePosition(GameObject input)
        {
            SetGameObject(input);
            return this;
        }

        public IGameBuilder SetExitPoint(GameObject input)
        {
            SetGameObject(input);
            return this;
        }

        public IGameBuilder SetMines(GameObject[] mines)
        {
            foreach (var mine in mines)
                SetGameObject(mine, false);

            return this;
        }

        public IGame Build()
        {
            return new Game(Board);
        }
    }
}
