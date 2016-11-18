using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace music_taste_based_radio_discovery.Models
{
    public class ProgramModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public int HitCount { get; set; }
        public List<string> Artists { get; set; }
    }
}