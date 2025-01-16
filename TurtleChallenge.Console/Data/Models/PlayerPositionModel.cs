using System.Text.Json.Serialization;
using TurtleChallenge.Console.Enums;

namespace TurtleChallange.Console.Data.Models
{
    public class PlayerPositionModel
    {
        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }

        [JsonPropertyName("direction")]
        public Direction Direction { get; set; }
    }
}
