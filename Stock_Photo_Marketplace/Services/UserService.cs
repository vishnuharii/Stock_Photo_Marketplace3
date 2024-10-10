using Stock_Photo_Marketplace.Data;
using Stock_Photo_Marketplace.Models;
using System.Linq;

namespace Stock_Photo_Marketplace.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public User Login(string username)
        {
            Console.WriteLine($"Attempting to log in user: {username}");
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
                    return user;                
            }
            return null;
        }

        public void Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
