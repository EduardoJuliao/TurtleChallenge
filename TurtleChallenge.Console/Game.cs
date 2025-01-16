using TurtleChallenge.Console.Entities;
using TurtleChallenge.Console.Enums;
using TurtleChallenge.Console.Interfaces;
using TurtleChallenge.Console.Messages;

namespace TurtleChallenge.Console
{
    public class Game(GameObject[,] board) : IGame, IDisposable
    {
        private readonly GameObject[,] _board = board;
        private IEnumerator<MoveInstruction> _moveInstructions = null!;
        private GameObject _turtlePosition = null!;

        public IGame SetMoves(IEnumerable<MoveInstruction> moves)
        {
            _moveInstructions = moves.GetEnumerator();
            return this;
        }

        public GameResult Play()
        {
            _turtlePosition = _board.Cast<GameObject>().Where(x => x != null).Single(x => x.GameObjectType == GameObjectType.Turtle);

            GameResult? gameResult = new(GameStatus.InProgress);

            while(_moveInstructions.MoveNext() && gameResult.GameStatus == GameStatus.InProgress)
            {
                var currentInstruction = _moveInstructions.Current;
                switch (currentInstruction)
                {
                    case MoveInstruction.Rotate:
                        _turtlePosition.Direction = _turtlePosition.Direction switch
                        {
                            Direction.North => Direction.East,
                            Direction.East => Direction.South,
                            Direction.South => Direction.West,
                            Direction.West => Direction.North,
                            _ => throw new ArgumentOutOfRangeException()
                        };
                        break;
                    case MoveInstruction.Move:
                        {

                            var x = _turtlePosition.PositionX;
                            var y = _turtlePosition.PositionY;
                            switch (_turtlePosition.Direction)
                            {
                                case Direction.North:
                                    gameResult = Move(x, y - 1);
                                    break;
                                case Direction.East:
                                    gameResult = Move(x + 1, y);
                                    break;
                                case Direction.South:
                                    gameResult = Move(x, y + 1);
                                    break;
                                case Direction.West:
                                    gameResult = Move(x - 1, y);
                                    break;
                            }
                            break;
                        }
                }
            }

            if (gameResult.GameStatus == GameStatus.InProgress)
                gameResult = new GameResult(GameStatus.StillInDanger, Message.TurtleStillInDanger);

            return gameResult;
        }

        private GameResult Move(int x, int y)
        {
            if (x < 0 || x >= _board.GetLength(0) || y < 0 || y >= _board.GetLength(1))
                return new GameResult(GameStatus.OutOfBounds, Message.TurtleIsOutOfBounds);

            if (_board[x, y] != null && _board[x, y].GameObjectType == GameObjectType.Mine)
                return new GameResult(GameStatus.MineHit, Message.TurtleHasHitAMine);

            if (_board[x, y] != null && _board[x, y].GameObjectType == GameObjectType.ExitPoint)
                return new GameResult(GameStatus.Success, Message.TurtleHasExited);

            _board.SetValue(null, _turtlePosition.PositionX, _turtlePosition.PositionY);
            _turtlePosition.PositionX = x;
            _turtlePosition.PositionY = y;
            _board[x, y] = _turtlePosition;
            return new GameResult(GameStatus.InProgress);
        }

        public void Dispose()
        {
            _moveInstructions?.Dispose();
        }
    }
}
