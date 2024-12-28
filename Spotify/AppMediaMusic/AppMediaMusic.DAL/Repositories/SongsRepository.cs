using AppMediaMusic.DAL.Entities;
using TagLib;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace AppMediaMusic.DAL.Repositories
{
    public class SongsRepository
    {
        private AssignmentPrnContext _context;

        public void Delete(Song selected)
        {

            _context = new AssignmentPrnContext();
            _context.Songs.Remove(selected);
            _context.SaveChanges();
        }

        public List<Song> GetAll()
        {
            _context = new AssignmentPrnContext();
            var songs = _context.Songs.ToList();

            foreach (var song in songs)
            {
                try
                {
                    // Use TagLib to extract the album art
                    var file = TagLib.File.Create(song.FilePath);
                    if (file.Tag.Pictures.Length > 0)
                    {
                        // Extract album art as a byte array
                        byte[] albumArt = file.Tag.Pictures[0].Data.Data;

                        // You can now use albumArt in your application (e.g., bind it to a UI element)
                        song.AlbumArt = albumArt; // This will hold the album art for display without saving it in the DB
                    }
                    else
                    {
                        song.AlbumArt = null; // No album art found
                    }
                }
                catch (Exception e)
                {
                    Debug.Write($"Error retrieving album art for {song.Title}: {e.Message}");
                    song.AlbumArt = null; // If there's an error, set to null
                }
            }

            return songs;
        }
        public bool Add(string filePath)
        {
            var file = TagLib.File.Create(filePath);
            string title = file.Tag.Title ?? "Unknown Title";
            string artist = file.Tag.FirstPerformer ?? "Unknown Artist";
            string album = file.Tag.Album ?? "Unknown Album";
            string genre = file.Tag.FirstGenre ?? "Unknown Genre";
            DateTime dateAdded = DateTime.Now;

            _context = new AssignmentPrnContext();

            var existingSong = _context.Songs
                          .FirstOrDefault(s => s.Title == title && s.Artist == artist);

            if (existingSong != null)
            {

                return false; //song already exist
            }

            Song song = new Song
            {
                Title = title,
                Artist = artist,
                Album = album,
                Genre = genre,
                FilePath = filePath,
                CreatedAt = dateAdded
            };
            _context.Songs.Add(song);
            _context.SaveChanges();
            return true;
        }

        public void Update(Song song)
        {
            try
            {
                using var context = new AssignmentPrnContext();
                context.Update(song);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Song GetSongById(int id)
        {
            using var context = new AssignmentPrnContext();
            return context.Songs.FirstOrDefault(c => c.SongId == id);

        }
    }
    }