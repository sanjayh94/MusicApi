﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Interfaces;
using MusicApi.Models;
using MusicApi.Services;

namespace MusicApi.Controllers
{
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
            _tracksService = tracksService;
            _logger = logger;
        }
        #endregion

        [HttpGet]
        public async Task<List<Track>> Get()
        {
            return await _tracksService.GetAsync();
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<Track>> Get(long id)
        {
            var track = await _tracksService.GetAsync(id);

            if (track is null)
            {
                return NotFound();
            }

            return track;
        }

        [HttpGet("search/{word}")]
        public async Task<ActionResult<Track>> FindTracksByWord(string word)
        {
            List<Track>? tracks = await _tracksService.ListByWordAsync(word);

            if (tracks.Count == 0)
            {
                return NotFound();
            }

            return Ok(tracks);
        }

        [HttpGet("count/{word}")]
        public async Task<ActionResult<Track>> GetTrackCountByWord(string word)
        {
            long tracks = await _tracksService.GetTrackCountByWord(word);

            return Ok($"{{\"count\":{tracks}}}");
        }
    }
}