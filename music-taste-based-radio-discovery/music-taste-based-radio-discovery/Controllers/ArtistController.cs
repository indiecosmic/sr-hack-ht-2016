using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace music_taste_based_radio_discovery.Controllers
{
    public class ArtistController : Controller
    {
        // GET: Artist
        public async Task<ActionResult> Index()
        {
            var code = Session["code"] as string;
            if (string.IsNullOrEmpty(code))
                return RedirectToAction("Index", "Home");

            SpotifyWebAPI.Authentication.ClientId = Settings.SpotifyClientId;
            SpotifyWebAPI.Authentication.ClientSecret = Settings.SpotifyClientSecret;
            SpotifyWebAPI.Authentication.RedirectUri = Settings.SpotifyRedirectUri;

            var authenticationToken = await SpotifyWebAPI.Authentication.GetAccessToken(code);
            var result = await SpotifyWebAPI.User.GetTopArtists(authenticationToken);

            return View("MostPlayed", result.Items);
        }
    }
}