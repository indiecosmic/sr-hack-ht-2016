using System.Collections.Generic;

namespace music_taste_based_radio_discovery.Models.Callback
{
    public class MostPlayedModel
    {
        public List<string> Artists { get; private set; }

        public MostPlayedModel(List<string> artists)
        {
            Artists = artists;
        }
    }
}