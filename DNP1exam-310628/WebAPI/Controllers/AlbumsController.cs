using Entities;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataAccess;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AlbumsController : ControllerBase {
    private readonly IDataAccess _dataAccess;

    public AlbumsController(IDataAccess dataAccess) {
        _dataAccess = dataAccess;
    }

    [HttpPost]
    public async Task<ActionResult<Album>> AddAlbum([FromBody] Album album) {
        try {
            Album addedAlbum = await _dataAccess.AddAlbum(album);
            return Ok(addedAlbum);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("titles")]
    public async Task<ActionResult<List<string>>> GetAllAlbumTitles() {
        try {
            List<string> allAlbumTitles =await _dataAccess.GetAllAlbumTitles();
            return Ok(allAlbumTitles);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

}