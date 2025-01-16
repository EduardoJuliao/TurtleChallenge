using System.Text.Json.Serialization;

namespace TurtleChallange.Console.Data.Models
{
    public class MineModel
    {
        [JsonPropertyName("position")]
        public PositionModel Position { get; set; }
    }
}
