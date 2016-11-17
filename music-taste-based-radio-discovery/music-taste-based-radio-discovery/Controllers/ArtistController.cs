using System;
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
        // GET: Artist
        public async Task<ActionResult> Index()
        {
            var token = Session["token"] as AuthenticationToken;
            if (token == null)
                return RedirectToAction("Index", "Home");
            
            var result = await SpotifyWebAPI.User.GetTopArtists(token);

            return View("MostPlayed", result.Items);
        }

        public async Task<ActionResult> Details(string id)
        {
            var token = Session["token"] as AuthenticationToken;
            if (token == null)
                return RedirectToAction("Index", "Home");

            var spotifyResult = await SpotifyWebAPI.Artist.GetArtist(id);
            var repo = new PlaylistInfoRepository();
            var dbResults = await repo.GetTracksByArtistName(spotifyResult.Name);
            var channel = dbResults.GroupBy(r => r.NewsName).OrderByDescending(g => g.Count()).FirstOrDefault().Key;

            var model = new ArtistDetails
            {
                ArtistName = spotifyResult.Name,
                PlayCount = dbResults.Count(),
                Channel = channel
            };

            return View(model);
        }
    }
}