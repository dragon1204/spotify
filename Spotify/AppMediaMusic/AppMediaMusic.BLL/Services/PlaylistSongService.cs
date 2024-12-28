using AppMediaMusic.DAL;
using AppMediaMusic.DAL.Entities;
using AppMediaMusic.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppMediaMusic.BLL.Services
{
    public class PlaylistSongService
    {
        private readonly PlaylistSongRepository _songRepository = new PlaylistSongRepository();

        public List<PlaylistSong> GetSongsByPlaylistId(int playListId)
        {
            return _songRepository.GetSongsByPlaylistId(playListId);
        }

        public void Add(int playlistId, string songName, string artist, string filePath)
        {
            var song = new Song
            {
                Title = songName,
                Artist = artist,
                FilePath = filePath
            };

            using (var context = new AssignmentPrnContext())
            {
                context.Songs.Add(song);
                context.SaveChanges();

                var playlistSong = new PlaylistSong
                {
                    PlaylistId = playlistId,
                    SongId = song.SongId
                };

                context.PlaylistSongs.Add(playlistSong);
                context.SaveChanges(); 
            }
        }

        public void Delete(PlaylistSong playlistSong)
        {
            _songRepository.Delete(playlistSong);
        }

        public int GetPlaylistIdBySongId(int songId)
        {
            return _songRepository.GetPlaylistIdBySongId(songId);
        }

        public IEnumerable<PlaylistSong> GetSongsByPlaylistId2(int playlistId)
        {
            using var context = new AssignmentPrnContext();
            return context.PlaylistSongs
                          .Include(ps => ps.Song) 
                          .Where(ps => ps.PlaylistId == playlistId)
                          .ToList();
        }
    }
}
