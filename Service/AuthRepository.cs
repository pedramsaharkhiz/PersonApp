using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Interface;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Service
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AuthRepository(DataContext context, ITokenService tokenService)
        {
            this._tokenService = tokenService;
            this._context = context;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(
                c => c.Username.ToLower().Equals(username.ToLower())
            );
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found!";
            }
            else if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password!";
            }
            else
            {
                response.Success = true;
                response.Message = user.Username;
                response.Data = _tokenService.CreateToken(user);
            }
            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if (await UserExists(user.Username))
            {
                response.Success = false;
                response.Message = "User Already Exists!";
                return response;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            response.Data = user.Id;
            response.Success = true;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username.ToLower().Equals(username.ToLower())))
            {
                return true;
            }
            return false;
        }

        public void CreatePasswordHash(
            string password,
            out byte[] passwordHash,
            out byte[] passwordSalt
        )
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < ComputedHash.Length; i++)
                {
                    if (ComputedHash[i] != PasswordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
