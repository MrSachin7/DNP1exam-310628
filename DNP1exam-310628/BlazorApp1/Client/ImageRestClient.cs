using System.Text;
using System.Text.Json;
using Entities;

namespace BlazorApp1.Client; 

public class ImageRestClient :IImageClient {
    public async Task AddImage(Image imageToAdd, string selectedAlbumTitle) {
        using HttpClient client = new HttpClient();
        string imageAsJson = JsonSerializer.Serialize(imageToAdd);
        StringContent content = new StringContent(imageAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage responseMessage = await client.PostAsync($"http://localhost:5274/Images/{selectedAlbumTitle}", content);
        await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
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