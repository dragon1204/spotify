using AppMediaMusic.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AppMediaMusic.DAL.Repositories
{
    public class UserRepository
    {

        private AssignmentPrnContext _context;

        public UserRepository()
        {
            _context = new AssignmentPrnContext();
        }

        public User? User(string username, string password)
        {
            _context = new AssignmentPrnContext();
            return _context.Users.FirstOrDefault(x => x.Username.ToLower() == username && x.Password == password);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.UserProfiles.Add(user.UserProfile);
            _context.SaveChanges();
        }
        public List<User> GetAll()
        {
            return _context.Users
                 .Where(u => u.Role == 1) // Lọc người dùng có Role = 1
                 .Include(u => u.UserProfile) // Bao gồm thông tin UserProfile
                 .ToList();
        }
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void AddAdmin(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(c => c.UserId == id);
        }

    }
}