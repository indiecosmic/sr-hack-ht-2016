using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace music_taste_based_radio_discovery.Models
{
    public class PlaylistTrack
    {
        public int ChannelId { get; set; }
        public DateTime StartTime { get; set; }
        public string Title { get; set; }
        public string Performer { get; set; }
        public int? EpisodeId { get; set; }
        public int UnitId { get; set; }
    }
}