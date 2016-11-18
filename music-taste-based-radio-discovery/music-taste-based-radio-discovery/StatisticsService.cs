using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using music_taste_based_radio_discovery.Models;
using music_taste_based_radio_discovery.Repositories;
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
            var results = await _playlistRepository.GetTracksByArtistName(artist);
            var list = new List<ChannelModel>();
            foreach (var result in results)
            {
                var channel = await GetChannel(result.ChannelId);
                list.Add(channel);
            }

            return list;
        }

        public async Task<ChannelModel> GetChannel(int id)
        {
            var json =
                    await HttpHelper.Get($"http://api.sr.se/api/v2/channels/{id}?format=json");
            //var obj = JsonConvert.DeserializeObject<artistsearchresult>(json, new JsonSerializerSettings
            //{
            //    TypeNameHandling = TypeNameHandling.All,
            //    TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            //});
            var channel = new ChannelModel { };
            return channel;
        }
    }
}