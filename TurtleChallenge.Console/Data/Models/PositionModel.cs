using System.Text.Json.Serialization;

namespace TurtleChallange.Console.Data.Models
{
    public class PositionModel
    {
        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }
    }
}
