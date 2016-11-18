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

        public async Task<SummaryModel> CreateSummary(string artist)
        {
            var results = await _playlistRepository.GetTracksByArtistName(artist);
            var model = new SummaryModel();

            var channelGroups = results.GroupBy(r => r.ChannelId);
            model.Channels = await GetChannels(channelGroups.Select(cg => cg.Key));

            var unitGroups = results.GroupBy(r => r.UnitId);
            model.Programs = await GetPrograms(unitGroups.Select(cg => cg.Key));

            return model;
        }

        public async Task<List<ChannelModel>> GetChannels(IEnumerable<int> channelIds)
        {
            var list = new List<ChannelModel>();
            foreach (var channelId in channelIds)
            {
                var channel = await GetChannel(channelId);
                list.Add(channel);
            }

            return list;
        }

        public async Task<List<ProgramModel>> GetPrograms(IEnumerable<int> unitIds)
        {
            var list = new List<ProgramModel>();
            foreach (var unitId in unitIds)
            {
                if (unitId == 0) continue;
                var program = await GetProgram(unitId);
                if (program != null)
                    list.Add(program);
            }

            return list;
        }

        public async Task<ProgramModel> GetProgram(int id)
        {
            var json =
        await HttpHelper.Get($"http://api.sr.se/api/v2/programs/{id}?format=json");
            if (json == "404 Not Found")
                return null;

            dynamic data = Json.Decode(json);
            return new ProgramModel
            {
                Id = data.program.id,
                Name = data.program.name,
                Description = data.program.description,
                ImageUrl = data.program.programimage,
                Url = data.program.programurl
            };
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
                Name = data.channel.name,
                Url = data.channel.siteurl
            };
        }
    }
}