using Entities;

namespace WebAPI.DataAccess; 

public interface IDataAccess {
    Task<Album> AddAlbum(Album album);
    Task<List<string>> GetAllAlbumTitles
        ();

    Task<Image> AddImage(Image image, string albumTitle);

    Task<List<Image>> GetImages(string? albumCreator, string? topic);
}