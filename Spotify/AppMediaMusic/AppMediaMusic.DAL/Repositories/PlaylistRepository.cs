using AppMediaMusic.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppMediaMusic.DAL.Repositories
{
    public class PlaylistRepository
    {
        private  AssignmentPrnContext _context;


        public List<Playlist> GetPlaylistsByUserId(int userId)
        {
            _context = new AssignmentPrnContext();
            return _context.Playlists
                .Where(p => p.UserId == userId)
                .Include(p => p.User)
                .ToList();
        }
        public void Add(int userId, string playlistName)
        {
            var user = _context.Users.Find(userId);
            if (user == null) throw new ArgumentException("User not found.");

            var playlist = new Playlist
            {
                Name = playlistName,
                UserId = userId,
                CreatedAt = DateTime.Now
            };

            _context.Playlists.Add(playlist);
            _context.SaveChanges();
        }

        public void Update(Playlist playlist)
        {
            _context.Playlists.Attach(playlist);
            _context.Entry(playlist).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Playlist playlist)
        {
            _context.Playlists.Remove(playlist);
            _context.SaveChanges();
        }
    }
}
