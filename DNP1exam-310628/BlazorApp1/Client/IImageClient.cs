using Entities;

namespace BlazorApp1.Client; 

public interface IImageClient {
    Task AddImage(Image imageToAdd, string selectedAlbumTitle);
}