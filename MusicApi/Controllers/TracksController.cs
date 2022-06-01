using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Interfaces;
using MusicApi.Models;
using MusicApi.Services;

namespace MusicApi.Controllers
{
    /// <summary>
    /// Controller class to handle 'api/Tracks' route
    /// </summary>
    [Route("api/Tracks")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        #region Declarations
        // Setting up private Dependency Injection Variables
        private readonly ITracksService _tracksService;
        private readonly ILogger<TracksController> _logger;

        public TracksController(ITracksService tracksService, ILogger<TracksController> logger)
        {
            //  Setting up the tracksService using Dependency Injection.
            //  The service will retrieve Track Info for the controller from the DB
            _tracksService = tracksService;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Get a list of all tracks currently in DB. Returns a subset of properties using TrackDTO.
        /// </summary>
        /// <returns>List of Tracks (DTO class)</returns>
        [HttpGet]
        public async Task<List<TrackDTO>> Get()
        {
            return await _tracksService.GetAsync();
        }

        /// <summary>
        /// Get a track from the TrackService from an id. 
        /// Route at /api/Tracks/{id}
        /// </summary>
        /// <param name="id">Track id</param>
        /// <returns>ActionResult with a Track (DTO Class)</returns>
        [HttpGet("{id}")] 
        public async Task<ActionResult<TrackDTO>> Get(long id)
        {
            var track = await _tracksService.GetAsync(id);

            if (track is null)
            {
                return NotFound();
            }

            return track;
        }

        /// <summary>
        /// For a given word, will return all the tracks where given word is included in the title.
        /// Route at /api/Tracks/search/{word}
        /// </summary>
        /// <param name="word">word to search in Track title</param>
        /// <returns>List of tracks with input word in the title</returns>
        [HttpGet("search/{word}")]
        public async Task<ActionResult<TrackDTO>> FindTracksByWord(string word)
        {
            // Basic input validation. 
            // In production, more advanced input validation can be configured using libraries such as Fluent Validation
            // that can help increase security, especially during POST calls.
            if (string.IsNullOrWhiteSpace(word))
            {
                return BadRequest();
            }

            List<TrackDTO>? tracks = await _tracksService.ListByWordAsync(word);

            if (tracks.Count == 0)
            {
                return NotFound();
            }

            return Ok(tracks);
        }

        /// <summary>
        /// For a given word, will return the count of tracks where given word is included in the title
        /// Route at /api/Tracks/count/{word}
        /// </summary>
        /// <param name="word">word to search in Track title</param>
        /// <returns>count of tracks with input word in the title</returns>
        [HttpGet("count/{word}")]
        public async Task<ActionResult<TrackDTO>> GetTrackCountByWord(string word)
        {
            // Basic input validation. 
            // In production, more advanced input validation can be configured using libraries such as Fluent Validation
            // that can help increase security, especially during POST calls.
            if (string.IsNullOrWhiteSpace(word))
            {
                return BadRequest();
            }

            long tracks = await _tracksService.GetTrackCountByWord(word);

            return Ok($"{{\"count\":{tracks}}}"); // Returns count of tracks
        }
    }
}
