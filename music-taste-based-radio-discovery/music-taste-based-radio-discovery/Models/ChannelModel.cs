using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace music_taste_based_radio_discovery.Models
{
    public class ChannelModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int HitCount { get; set; }
    }
}