using Entities;

namespace BlazorApp1.Client; 

public interface IDisplayClient {
    Task<ICollection<Image>> GetImagesAsync(string? albumCreator, string? topic);
}                           