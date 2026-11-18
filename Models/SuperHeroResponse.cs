using System.Text.Json.Serialization;

namespace BlazorWasmSuperHeroApi.Models;

public sealed class SuperHeroResponse
{
    public string? Response { get; init; }
    public string? Error { get; init; }

    [JsonPropertyName("results-for")]
    public string? ResultsFor { get; init; }

    public List<SuperHero>? Results { get; init; }
}

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(SuperHeroResponse))]
internal partial class SuperHeroJsonContext : JsonSerializerContext
{
}
