using System.Net;
using System.Net.Http.Json;
using BlazorWasmSuperHeroApi.Models;

namespace BlazorWasmSuperHeroApi.Services;

public sealed class SuperHeroService(HttpClient httpClient)
{
    private const string ApiEndpoint = "https://www.superheroapi.com/api.php";

    public async Task<IReadOnlyList<SuperHero>> SearchAsync(string accessToken, string name)
    {
        var tokenSegment = Uri.EscapeDataString(accessToken);
        var nameSegment = Uri.EscapeDataString(name);
        var url = $"{ApiEndpoint}/{tokenSegment}/search/{nameSegment}";

        using var response = await httpClient.GetAsync(url);
        if (response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
        {
            throw new SuperHeroApiException("A API recusou o token. Confira o código e tente novamente.");
        }

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync(SuperHeroJsonContext.Default.SuperHeroResponse);
        if (result?.Response == "success")
        {
            return result.Results ?? [];
        }

        var error = result?.Error;
        if (error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
        {
            return [];
        }

        if (string.Equals(error, "access denied", StringComparison.OrdinalIgnoreCase))
        {
            throw new SuperHeroApiException("A API recusou o token. Confira o código e tente novamente.");
        }

        throw new SuperHeroApiException("A SuperHero API retornou uma resposta inesperada. Tente novamente.");
    }

    public async Task<IReadOnlyList<SuperHero>> GetExampleAsync()
    {
        var result = await httpClient.GetFromJsonAsync(
            "data/batman.json", SuperHeroJsonContext.Default.SuperHeroResponse);

        return result?.Results ?? [];
    }
}

public sealed class SuperHeroApiException(string message) : Exception(message);
