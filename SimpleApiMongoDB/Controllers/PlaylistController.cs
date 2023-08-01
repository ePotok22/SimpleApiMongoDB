using Microsoft.AspNetCore.Mvc;
using SimpleApiMongoDB.Entities;
using SimpleApiMongoDB.Services;
using SimpleApiMongoDB.Services.Interfaces;

namespace SimpleApiMongoDB.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class PlaylistController : ControllerBase
    {
        private readonly IMongoDBService _mongoDBService;
        private readonly ILogger<PlaylistController> _logger;

        public PlaylistController(ILogger<PlaylistController> logger, IMongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<PlaylistDto>> GetAll() =>
             await _mongoDBService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<PlaylistDto> Get(string id) =>
           await _mongoDBService.GetAsync(id);

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Playlist playList)
        {
            PlaylistDto pl = PlaylistDto.CreateDto(playList);
            await _mongoDBService.CreateAsync(pl);
            return CreatedAtAction(nameof(Get), new { id = pl.Id }, pl);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AddToPlayList(string id, [FromBody] string movieId)
        {
            PlaylistDto find = await _mongoDBService.GetAsync(id);
            if (find == null)
                return NotFound("id does not exist.");
            await _mongoDBService.AddToPlayListAsync(id, movieId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            PlaylistDto find = await _mongoDBService.GetAsync(id);
            if (find == null)
                return NotFound("id does not exist.");
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }
    }
}