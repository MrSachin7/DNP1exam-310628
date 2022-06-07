using Entities;

namespace BlazorApp1.Client; 

public interface IAlbumClient {
    Task AddAlbum(Album albumToCreate);
    Task<ICollection<string>> GetAllAlbumTitles();
}