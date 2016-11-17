using System.Configuration;

namespace music_taste_based_radio_discovery
{
    public abstract class Settings
    {
        private static string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string SpotifyClientId => Get("SpotifyClientId");
        public static string SpotifyClientSecret => Get("SpotifyClientSecret");
        public static string SpotifyRedirectUri => Get("SpotifyRedirectUri");
        public static string IsidorConnectionString => Get("Isidor");
    }
}