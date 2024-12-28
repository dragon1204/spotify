using AppMediaMusic.DAL.Entities;
using AppMediaMusic.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMediaMusic.BLL.Services
{
    public class SongsService
    {
        private SongsRepository _repo = new SongsRepository();

        public void Delete(Song selected)
        {
            _repo.Delete(selected);
        }

        public List<Song> GetAllSongs()
        {
            return _repo.GetAll();
        }
        public bool AddSong(string filePath)
        {
            return _repo.Add(filePath);
        }

        public void Update(Song song)
        {
            _repo.Update(song);
        }

        public Song GetSongById(int id)
        {
            return _repo.GetSongById(id);
        }

    }
}
