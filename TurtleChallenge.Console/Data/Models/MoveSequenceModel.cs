using System.Text.Json.Serialization;
using System.Text.Json;
using TurtleChallenge.Console.Enums;

namespace TurtleChallenge.Console.Data.Models
{
    public class MoveSequenceModel
    {
        [JsonPropertyName("instructions")]
        public List<MoveInstruction> Instructions { get; set; } = new List<MoveInstruction>();

        public static List<MoveSequenceModel> Deserialize(string json) => JsonSerializer.Deserialize<List<MoveSequenceModel>>(json)!;
    }
}