using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebAPI.DataAccess;

public class AlbumContext : DbContext, IDataAccess {
    public DbSet<Album> Albums { get; set; }
    public DbSet<Image> Images { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite(
            @"Data Source = C:\Users\nepal\OneDrive\Desktop\DNP-Exam-310628\DNP1exam-310628\WebAPI\albums.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Image>().HasKey(image => new {image.URL, image.Title});
    }

    public async Task<Album> AddAlbum(Album album) {
        EntityEntry<Album> entry = Albums.Add(album);
        await SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<List<string>> GetAllAlbumTitles() {
        var allAlbumTitles = await Albums.Select(album => new {
            album.Title
        }).ToListAsync();


        List<string> listToReturn = new List<string>();
        foreach (var i in allAlbumTitles) {
            listToReturn.Add(i.Title);
        }

        return listToReturn;
    }

    public async Task<Image> AddImage(Image image, string albumTitle) {
        Album? album = await Albums.Include(album1 => album1.Images)
            .FirstOrDefaultAsync(album1 => album1.Title.Equals(albumTitle));
        if (album == null) {
            throw new Exception("Album not found in database");
        }

        image.Album = album;
        EntityEntry<Image> entry = await Images.AddAsync(image);
        await SaveChangesAsync();
        Image imagetoReturn = entry.Entity;
        imagetoReturn.Album = null;
        return imagetoReturn;
    }

    public async Task<List<Image>> GetImages(string? albumCreator, string? topic) {

        IQueryable<Image> queryable = Images.AsQueryable();

        if (!string.IsNullOrEmpty(albumCreator)) {
            queryable = queryable.Where(image => image.Album.CreatedBy.ToLower().Equals(albumCreator.ToLower()));
        }

        if (! string.IsNullOrEmpty(topic)) {
            queryable = queryable.Where(image => image.Topic.Equals(topic));
        }

        return await queryable.ToListAsync();

    }
}

   