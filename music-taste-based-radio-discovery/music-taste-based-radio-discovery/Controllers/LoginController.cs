using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace music_taste_based_radio_discovery.Controllers
{
    public class LoginController : Controller
    {
        const string StateKey = "spotify_auth_state";

        // GET: Login
        public ActionResult Index()
        {
            var state = GenerateRandomString(16);
            Response.SetCookie(new HttpCookie(StateKey, state));

            var scope = "user-top-read";
            var clientId = "876bf1b29c814914a3da12d0da02ec44";
            var callback = HttpUtility.UrlEncode("http://localhost:55791/callback");
            var redirectAddress = $"https://accounts.spotify.com/authorize?response_type=code&client_id={clientId}&scope={scope}&redirect_uri={callback}&state={state}";

            return Redirect(redirectAddress);
        }

        private string GenerateRandomString(int length)
        {
            var text = "";
            var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var rnd = new Random();

            for (var i = 0; i < length; i++)
            {
                text += possible.Substring(rnd.Next(0, possible.Length), 1);
            }
            return text;
        }
    }
}