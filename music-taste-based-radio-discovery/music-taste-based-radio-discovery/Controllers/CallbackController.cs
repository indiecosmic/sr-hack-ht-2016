using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpotifyWebAPI;
using System.Threading.Tasks;
using music_taste_based_radio_discovery.Models.Callback;

namespace music_taste_based_radio_discovery.Controllers
{
    public class CallbackController : Controller
    {
        const string StateKey = "spotify_auth_state";

        // GET: Callback
        public async Task<ActionResult> Index(string code, string state)
        {
            var storedState = Request.Cookies[StateKey].Value;
            if (string.IsNullOrEmpty(state) || !state.Equals(storedState))
                return Redirect("/#error=state_mismatch");

            Response.Cookies.Remove(StateKey);

            SpotifyWebAPI.Authentication.ClientId = Settings.SpotifyClientId;
            SpotifyWebAPI.Authentication.ClientSecret = Settings.SpotifyClientSecret;
            SpotifyWebAPI.Authentication.RedirectUri = Settings.SpotifyRedirectUri;

            var authenticationToken = await SpotifyWebAPI.Authentication.GetAccessToken(code);
            var result = await SpotifyWebAPI.User.GetTopArtists(authenticationToken);
            var artists = result.Items;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
