using System.Text;
using System.Text.Json;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace BlazorApp1.Client; 

public class AlbumRestClient : IAlbumClient {


    public async Task AddAlbum(Album albumToCreate) {
        using HttpClient client = new HttpClient();
        string albumAsJson = JsonSerializer.Serialize(albumToCreate);
        StringContent content = new StringContent(albumAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage responseMessage = await client.PostAsync("http://localhost:5274/Albums", content);
        await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);


    }

    public async Task<ICollection<string>> GetAllAlbumTitles() {
        using HttpClient client = new HttpClient();
        HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:5274/Albums/titles");
        string responseContent = await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
        return GetDeserialized<List<string>>(responseContent);
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