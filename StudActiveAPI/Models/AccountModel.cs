using Microsoft.EntityFrameworkCore;
using StudActiveAPI.Services;
using System.Text;
using System.Security.Cryptography;

namespace StudActiveAPI.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public async Task<AccountModel> LoginHash()
        {
            using var context = new Context();

            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == UserName);

                if (user is not null)
                {
                    string pass = HashPass(Password, user.Salt);

                    if (user.Hash == pass)
                    {
                        var role = await context.UserRoles.FirstOrDefaultAsync(x => x.UserRoleId == user.Role);
                        var roleName = role.Name;
                        var account = new AccountModel
                        {
                            UserName = user.UserName,
                            LastName = user.LastName,
                            FirstName = user.FirstName,
                            MiddleName = user.MiddleName,
                            Role = roleName,
                            Id = user.Id
                        };
                        return account;
                    }
                    return new AccountModel();
                }
                return new AccountModel();
            }
            catch
            {
                return null;
            }
        }

        public string HashPass(string text, byte[] salt)
        {
            var hash = new SHA256Managed();

            var saltBytes = salt;
            var textBytes = Encoding.UTF8.GetBytes(text);
            var textBytesSalt = new byte[textBytes.Length + saltBytes.Length];

            for (int i = 0; i < textBytes.Length; i++)
                textBytesSalt[i] = textBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                textBytesSalt[textBytes.Length + i] = saltBytes[i];

            var hashSaltBytes = hash.ComputeHash(textBytesSalt);

            var hastValue = Convert.ToBase64String(hashSaltBytes);
            return hastValue;
        }
    }
    public class AccountModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
