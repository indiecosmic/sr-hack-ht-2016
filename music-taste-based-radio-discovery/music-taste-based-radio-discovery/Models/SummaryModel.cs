using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace music_taste_based_radio_discovery.Models
{
    public class SummaryModel
    {
        public IEnumerable<SpotifyWebAPI.Artist> Artists { get; set; }
        public IEnumerable<ChannelModel> Channels { get; set; }
    }
}