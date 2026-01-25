using System.Text.Json.Serialization;
using DiscordRPC.IO;
using DiscordRPC.RPC.Payload;
using DiscordRPC.Message;
using DiscordRPC.RPC.Commands;
using System.Text.Json;

namespace DiscordRPC.Helper
{
    [JsonSourceGenerationOptions(
        WriteIndented = false,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower
    )]
    [JsonSerializable(typeof(Handshake))]
    [JsonSerializable(typeof(EventPayload))]
    [JsonSerializable(typeof(ArgumentPayload))]
    [JsonSerializable(typeof(PresenceCommand))]
    [JsonSerializable(typeof(SubscribeCommand))]
    [JsonSerializable(typeof(RespondCommand))]
    [JsonSerializable(typeof(CloseCommand))]
    [JsonSerializable(typeof(ReadyMessage))]
    [JsonSerializable(typeof(PresenceMessage))]
    [JsonSerializable(typeof(SubscribeMessage))]
    [JsonSerializable(typeof(UnsubscribeMessage))]
    [JsonSerializable(typeof(JoinMessage))]
    [JsonSerializable(typeof(JoinRequestMessage))]
    [JsonSerializable(typeof(SpectateMessage))]
    [JsonSerializable(typeof(ErrorMessage))]
    [JsonSerializable(typeof(RichPresence))]
    [JsonSerializable(typeof(RichPresenceResponse))]
    [JsonSerializable(typeof(BaseRichPresence))]
    [JsonSerializable(typeof(Timestamps))]
    [JsonSerializable(typeof(Assets))]
    [JsonSerializable(typeof(Party))]
    [JsonSerializable(typeof(Secrets))]
    [JsonSerializable(typeof(User))]
    [JsonSerializable(typeof(Configuration))]
    [JsonSerializable(typeof(Button))]
    internal partial class DiscordJsonContext : JsonSerializerContext
    {
        public new static JsonSerializerOptions Options => new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            TypeInfoResolver = Default
        };
    }
}
