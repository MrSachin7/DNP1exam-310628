using System.Text.Json;
using Entities;

namespace BlazorApp1.Client;

public class DisplayRestClient : IDisplayClient {
    public async Task<ICollection<Image>> GetImagesAsync(string? albumCreator, string? topic) {
        using HttpClient client = new HttpClient();
        HttpResponseMessage responseMessage =
            await client.GetAsync($"http://localhost:5274/Images?albumCreator={albumCreator}&&topic={topic}");

        string responseContent = await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
        return GetDeserialized<ICollection<Image>>(responseContent);
    }

    private T GetDeserialized<T>(string jsonFormat) {
        T obj = JsonSerializer.Deserialize<T>(jsonFormat, new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        }) !;
        return obj;
    }

    private async Task<string> GetResponseContentFromResponseMessageAndThrowAppropriateException(
        HttpResponseMessage responseMessage) {
        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode) {
            throw new Exception($"Error :{responseMessage.StatusCode}, {responseContent}");
        }

        return responseContent;
    }
}