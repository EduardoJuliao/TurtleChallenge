using TurtleChallange.Console.Data.Models;
using TurtleChallenge.Console;
using TurtleChallenge.Console.Data.Models;
using TurtleChallenge.Console.Entities;
using TurtleChallenge.Console.Enums;
using TurtleChallenge.Console.Helpers;
using TurtleChallenge.Console.Validators;

Console.Title = "Turtle Adventure!";
Console.WriteLine(WelcomePanel.GetPanel());

var setupPath = args.Length >= 1 ? args[0] : string.Empty;
var instructionsPath = args.Length >= 2 ? args[1] : string.Empty;

FilePathHelper.ValidateBoardSetupFilePath(ref setupPath);
FilePathHelper.ValidateMoveInstructionsFilePath(ref instructionsPath);

BoardModel setup = BoardModel.Deserialize(File.ReadAllText(setupPath))!;
List<MoveSequenceModel> moves = MoveSequenceModel.Deserialize(File.ReadAllText(instructionsPath))!;

var validator = new GameSetupValidator();
var mines = setup.Mines.Select(m => new GameObject(m.Position.X, m.Position.Y, GameObjectType.Mine)).ToArray();

var game = new GameBuilder(setup.BoardSize.X, setup.BoardSize.Y, validator)
    .SetTurtlePosition(new GameObject(setup.PlayerPosition.X, setup.PlayerPosition.Y, GameObjectType.Turtle, setup.PlayerPosition.Direction))
    .SetExitPoint(new GameObject(setup.ExitLocation.X, setup.ExitLocation.Y, GameObjectType.ExitPoint))
    .SetMines(mines)
    .Build();

var counter = 0;
foreach(var movementInstructions in moves)
{
    var result = game
        .SetMoves(movementInstructions.Instructions)
        .Play();
    Console.WriteLine($"Sequence {counter++}: {result.Message}");

}

Console.WriteLine("Press any key to exit...");
