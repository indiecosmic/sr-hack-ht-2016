using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpotifyWebAPI;
using System.Threading.Tasks;

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

            SpotifyWebAPI.Authentication.ClientId = "xxx";
            SpotifyWebAPI.Authentication.ClientSecret = "xxx";
            SpotifyWebAPI.Authentication.RedirectUri = "http://localhost:55791/callback";

            var authenticationToken = await SpotifyWebAPI.Authentication.GetAccessToken(code);
            var result = await SpotifyWebAPI.User.GetTopArtists(authenticationToken);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}