using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Services
{
    public class TimeSpanConverterService : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && TimeSpan.TryParse(reader.GetString(), out var timeSpan))
            {
                return timeSpan;
            }
            throw new JsonException("Invalid TimeSpan format");
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(@"hh\:mm\:ss"));
        }
    }
}
