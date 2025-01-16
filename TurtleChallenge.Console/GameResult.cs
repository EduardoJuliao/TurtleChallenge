using TurtleChallenge.Console.Enums;

namespace TurtleChallenge.Console
{
    public class GameResult(GameStatus gameStatus = GameStatus.InProgress, string message = "")
    {
        public GameStatus GameStatus { get; set; } = gameStatus;
        public string Message { get; set; } = message;
    }
}
