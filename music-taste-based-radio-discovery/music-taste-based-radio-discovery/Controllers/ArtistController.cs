﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using music_taste_based_radio_discovery.Models;
using music_taste_based_radio_discovery.Repositories;
using SpotifyWebAPI;

namespace music_taste_based_radio_discovery.Controllers
{
    public class ArtistController : Controller
    {
        private StatisticsService _statisticsService;
        public ArtistController()
        {
            _statisticsService = new StatisticsService();
        }

        // GET: Artist
        public async Task<ActionResult> Index()
        {
            var token = Session["token"] as AuthenticationToken;
            if (token == null)
                return RedirectToAction("Index", "Home");

            var result = await SpotifyWebAPI.User.GetTopArtists(token);

            var model = await _statisticsService.CreateSummary(result.Items.Select(r => r.Name));
            model.Artists = result.Items;

            return View("MostPlayed", model);
        }

        public async Task<ActionResult> Details(string id)
        {
            var token = Session["token"] as AuthenticationToken;
            if (token == null)
                return RedirectToAction("Index", "Home");

            var spotifyResult = await SpotifyWebAPI.Artist.GetArtist(id);
            var repo = new PlaylistInfoRepository();
            var dbResults = await repo.GetTracksByArtistName(spotifyResult.Name);
            var channelPlays =
                dbResults.GroupBy(r => r.NewsName)
                    .OrderByDescending(g => g.Count())
                    .Select(x => new Tuple<string, int>(x.Key, x.Count()));

            var model = new ArtistDetails
            {
                ArtistName = spotifyResult.Name,
                PlayCount = dbResults.Count(),
                ChannelPlays = channelPlays
            };

            return View(model);
        }
    }
}