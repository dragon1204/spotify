using AppMediaMusic.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMediaMusic.DAL.Repositories
{
    public class PlaylistSongRepository
    {
        private AssignmentPrnContext _context;

        public PlaylistSongRepository()
        {
            _context = new AssignmentPrnContext();
        }

        public List<PlaylistSong> GetSongsByPlaylistId(int playlistId)
        {
            return _context.PlaylistSongs
                .Where(ps => ps.PlaylistId == playlistId)  
                .Include(ps => ps.Song)  
                .Include(ps => ps.Playlist) 
                .ToList();  
        }
        public void Delete(PlaylistSong songToDelete)
        {
           
            var playlistSong = _context.PlaylistSongs
                .FirstOrDefault(ps => ps.SongId == songToDelete.SongId && ps.PlaylistId == songToDelete.PlaylistId);

            if (playlistSong != null)
            {
                
                _context.PlaylistSongs.Remove(playlistSong);
                _context.SaveChanges();
            }
        }
        public void Add(int playlistId, string songName,  string artist, string filePath)
        {
            // Tìm playlist dựa trên playlistId và bao gồm danh sách PlaylistSongs
            var playlist = _context.Playlists
                .Include(p => p.PlaylistSongs)
                .FirstOrDefault(p => p.PlaylistId == playlistId);

            if (playlist == null)
            {
                throw new ArgumentException("Playlist not found.");
            }

         
            var song = _context.Songs.FirstOrDefault(s => s.FilePath == filePath);
            if (song == null)
            {
                // Nếu bài hát chưa tồn tại, tạo mới bài hát với các thông tin truyền vào
                song = new Song
                {
                    Title = songName,
                    Artist = artist,
                    FilePath = filePath
                };
                _context.Songs.Add(song);
                _context.SaveChanges(); 
            }

            
            bool isSongInPlaylist = _context.PlaylistSongs
                .Any(ps => ps.PlaylistId == playlistId && ps.SongId == song.SongId);

            if (!isSongInPlaylist)
            {
               
                var playlistSong = new PlaylistSong
                {
                    PlaylistId = playlistId,
                    SongId = song.SongId
                };
                _context.PlaylistSongs.Add(playlistSong);
                _context.SaveChanges(); 
            }
        }
        public int GetPlaylistIdBySongId(int songId)
        {
       
            var playlistSong = _context.PlaylistSongs.FirstOrDefault(ps => ps.SongId == songId);
            return playlistSong?.PlaylistId ?? 0;  
        }
    }
}
