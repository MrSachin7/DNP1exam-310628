using Entities;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataAccess;

namespace WebAPI.Controllers; 

[ApiController]
[Route("[controller]")]
public class ImagesController :ControllerBase {

    private readonly IDataAccess _dataAccess;

    public ImagesController(IDataAccess dataAccess) {
        _dataAccess = dataAccess;
    }


    [HttpPost]
    [Route("{AlbumTitle}")]
    public async Task<ActionResult<Image>> AddImage([FromBody] Image image, [FromRoute] string albumTitle) {
        try {
            Image addedImage = await _dataAccess.AddImage(image, albumTitle);
            return Ok(addedImage);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<Image>>> GetImages([FromQuery] string? albumCreator , [FromQuery] string? topic ) {
        try {
            List<Image> images = await _dataAccess.GetImages(albumCreator, topic);
            return Ok(images);
        }
        catch (Exception e) {
            return StatusCode(500, e.Message);
        }


    }
}