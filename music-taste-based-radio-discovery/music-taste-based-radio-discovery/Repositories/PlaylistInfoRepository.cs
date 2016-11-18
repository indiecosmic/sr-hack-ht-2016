using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using music_taste_based_radio_discovery.Models;

namespace music_taste_based_radio_discovery.Repositories
{
    public class PlaylistInfoRepository
    {
        public async Task<IEnumerable<PlaylistTrack>> GetTracksByArtistName(string artistName)
        {
            using (var sqlConnection = new SqlConnection(Settings.IsidorConnectionString))
            {
                await sqlConnection.OpenAsync();

                var tracks =
                    await sqlConnection.QueryAsync<PlaylistTrack>(
                        "SELECT top 1000 p.ChannelId, " +
                        "p.StartTime, " +
                        "p.Title, " +
                        "p.Performer, " +
                        "p.EpisodeId, " +
                        "e.UnitID, " +
                        "u.NewsName " +
                        "FROM PlaylistInfo AS p " +
                        "INNER JOIN Unit AS u ON p.ChannelId = u.Id " +
                        "LEFT OUTER JOIN Episode AS e ON p.EpisodeId = e.Id " +
                        "WHERE Performer = @artistName", new {artistName});

                sqlConnection.Close();
                return tracks;
            }
        }
    }
}