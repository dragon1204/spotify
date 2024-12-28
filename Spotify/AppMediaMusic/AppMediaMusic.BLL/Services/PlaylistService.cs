using AppMediaMusic.DAL;
using AppMediaMusic.DAL.Entities;
using AppMediaMusic.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMediaMusic.BLL.Services
{
    public class PlaylistService
    {
        private readonly AssignmentPrnContext _context;

        public PlaylistService()
        {
            _context = new AssignmentPrnContext();
        }

        public void AddSongToPlaylist(string filePath, int playlistId)
        {
            var song = _context.Songs.FirstOrDefault(s => s.FilePath == filePath);
            if (song == null)
            {
                throw new Exception("Song not found!");
            }

            var playlist = _context.Playlists.FirstOrDefault(p => p.PlaylistId == playlistId);
            if (playlist == null)
            {
                throw new Exception("Playlist not found!");
            }

            var existingEntry = _context.PlaylistSongs
                .FirstOrDefault(ps => ps.PlaylistId == playlist.PlaylistId && ps.SongId == song.SongId);

            if (existingEntry == null)
            {
                var playlistSong = new PlaylistSong
                {
                    PlaylistId = playlist.PlaylistId,
                    SongId = song.SongId
                };

                _context.PlaylistSongs.Add(playlistSong);
                _context.SaveChanges();
            }
        }

        private PlaylistRepository _playlistRepository = new PlaylistRepository();
        public List<Playlist> GetPlaylistsByUserId(int userId)
        {
            return _playlistRepository.GetPlaylistsByUserId(userId);
        }
        public void CreatePlaylist(int UserId, string playlistName)
        {
            _playlistRepository.Add(UserId, playlistName);
        }
        public void UpdatePlaylist(Playlist x)
        {
            _playlistRepository.Update(x);
        }
        public void DeletePlaylist(Playlist x)
        {
            _playlistRepository.Delete(x);
        }

    }
}
