using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using music_taste_based_radio_discovery.Models;
using music_taste_based_radio_discovery.Repositories;
using Newtonsoft.Json;
using SpotifyWebAPI;
using SpotifyWebAPI.SpotifyModel;

namespace music_taste_based_radio_discovery
{
    public class StatisticsService
    {
        private readonly PlaylistInfoRepository _playlistRepository;
        public StatisticsService()
        {
            _playlistRepository = new PlaylistInfoRepository();
        }

        public async Task<List<ChannelModel>> GetChannels(string artist)
        {
            var list = new List<ChannelModel>();
            var results = await _playlistRepository.GetTracksByArtistName(artist);
            var channelGroups = results.GroupBy(r => r.ChannelId);

            foreach (var result in channelGroups)
            {
                var channel = await GetChannel(result.Key);
                list.Add(channel);
            }

            return list;
        }

        public async Task<ChannelModel> GetChannel(int id)
        {
            var json =
                    await HttpHelper.Get($"http://api.sr.se/api/v2/channels/{id}?format=json");
            dynamic data = Json.Decode(json);
            return new ChannelModel
            {
                Id = data.channel.id,
                Color = data.channel.color,
                Image = data.channel.image,
                Name = data.channel.name
            };
        }
    }
}