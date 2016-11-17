using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using music_taste_based_radio_discovery.Models;


namespace music_taste_based_radio_discovery.Repositories
{
    public class PlaylistInfoRepository
    {
        public IEnumerable<PlaylistTrack> GetTracksByArtistName(string artistName)
        {
            using (var sqlConnection = new SqlConnection(Settings.IsidorConnectionString))
            {
                sqlConnection.Open();

                IEnumerable<PlaylistTrack> tracks =
                    sqlConnection.Query<PlaylistTrack> ("SELECT * FROM PlaylistInfo WHERE Performer = @artistName", new {artistName})
                        .ToList();

                sqlConnection.Close();
                return tracks;
            }
        }
    }
}