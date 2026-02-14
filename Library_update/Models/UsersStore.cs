using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

namespace Library_update.Models
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
    
    public sealed class UserRecord
    {
        public string Id;
        public string Email;
        public string PasswordHash;
        public string Role;


    }

    public sealed class UsersStore
    {
        private readonly ConcurrentDictionary<string, UserRecord> _users = new();

        public UserRecord Find(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }
            _users.TryGetValue(email, out var user);

            return user;
        }
        public bool Create(string email, string passwordHash, string role)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(passwordHash) || string.IsNullOrEmpty(role))
            {
                return false;
            }
            if(_users.ContainsKey(email))
            {
                return false;
            }
            var user = new UserRecord
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                PasswordHash = passwordHash,
                Role = role
            };

            return _users.TryAdd(email, user);
        }

        
        public bool CheckPassword(UserRecord user,string password)
        {
            return user != null && user.PasswordHash == Hash(password);
        }

        private static string Hash(string value)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(value ?? string.Empty);
            var hash = sha.ComputeHash(bytes);
            var sb = new StringBuilder();
            foreach (var b in hash) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        public IReadOnlyCollection<UserRecord> GetAll()
        {
            return _users.Values.ToList().AsReadOnly();
        }
    }
}
