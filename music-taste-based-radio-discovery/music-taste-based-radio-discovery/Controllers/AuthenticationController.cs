using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace music_taste_based_radio_discovery.Controllers
{
    public class AuthenticationController : Controller
    {
        const string StateKey = "spotify_auth_state";
        const string Scope = "user-top-read";

        public ActionResult Login()
        {
            var state = GenerateRandomString(16);
            Response.SetCookie(new HttpCookie(StateKey, state));

            var clientId = Settings.SpotifyClientId;
            var callback = HttpUtility.UrlEncode(Settings.SpotifyRedirectUri);
            var redirectAddress = $"https://accounts.spotify.com/authorize?response_type=code&client_id={clientId}&scope={Scope}&redirect_uri={callback}&state={state}";

            return Redirect(redirectAddress);
        }

        public async Task<ActionResult> Callback(string code, string state)
        {
            var storedState = Request.Cookies[StateKey]?.Value;
            if (string.IsNullOrEmpty(state) || !state.Equals(storedState))
                return Redirect("/#error=state_mismatch");

            Response.Cookies.Remove(StateKey);

            SpotifyWebAPI.Authentication.ClientId = Settings.SpotifyClientId;
            SpotifyWebAPI.Authentication.ClientSecret = Settings.SpotifyClientSecret;
            SpotifyWebAPI.Authentication.RedirectUri = Settings.SpotifyRedirectUri;

            var authenticationToken = await SpotifyWebAPI.Authentication.GetAccessToken(code);
            Session["token"] = authenticationToken;

            return RedirectToAction("Index", "Artist");
        }

        private static string GenerateRandomString(int length)
        {
            const string possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var rnd = new Random();

            var text = "";
            for (var i = 0; i < length; i++)
            {
                text += possible.Substring(rnd.Next(0, possible.Length), 1);
            }
            return text;
        }
    }
}