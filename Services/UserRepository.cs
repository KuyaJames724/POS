using System.Linq;
using UserData;
using System.Text;
using PasswordHasher;

public class UserService
{
    private readonly _DbContext _context;

    public UserService(_DbContext context)
    {
        _context = context;
    }

    public User GetUserByUsername(string username)
    {
        return _context.Users.SingleOrDefault(u => u.Username == username);
    }

    public bool ValidateUserPassword(string username, string password)
    {
        var user = GetUserByUsername(username);

        if (user == null)
        {
            return false; // User not found
        }
    

    return PasswordHasher1.VerifyPassword(password, user.PasswordHash);
    }
}
