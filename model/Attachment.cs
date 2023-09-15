using System.Text.Json.Serialization;

namespace TasksManager.Model{
    public class Attachment{
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("fileName")]
        public string? FileName { get; set; }

        [JsonPropertyName("contentType")]
        public string? ContentType { get; set; }

        [JsonPropertyName("base64EncodedData")]
        public string? Base64EncodedData { get; set; }

        [JsonPropertyName("taskId")]
        public int? TaskId { get; set; }
    }
}
