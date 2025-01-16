using System.Text.Json.Serialization;
using System.Text.Json;

namespace TurtleChallange.Console.Data.Models
{
    internal class BoardModel
    {
        [JsonPropertyName("board_size")]
        public PositionModel BoardSize { get; set; } = null!;

        [JsonPropertyName("player_position")]
        public PlayerPositionModel PlayerPosition { get; set; } = null!;

        [JsonPropertyName("mines")]
        public List<MineModel> Mines { get; set; }

        [JsonPropertyName("exit_location")]
        public PositionModel ExitLocation { get; set; } = null!;

        public BoardModel()
        {
            Mines = [];
        }

        public static BoardModel Deserialize(string json) => JsonSerializer.Deserialize<BoardModel>(json)!;
    }
}
