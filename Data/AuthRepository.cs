using dotnetcore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace dotnetcore.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            User user = await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());

            if (user == null)
            {
                response.IsSuccess = false;
                response.Message = "User not found";
                return response;
            }
            if (VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Message = "Login succesful";
                response.Data = user.Id.ToString();
            }
            else 
            {
                response.IsSuccess = false;
                response.Message = "Login failed";
            }
            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new();

            if (await UserExists(user.Username))
            {
                response.IsSuccess = false;
                response.Message = "User with this username already exists";

                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            response.Data = user.Id;
            
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hash = new HMACSHA512())
            {
                passwordSalt = hash.Key;
                passwordHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hash = new HMACSHA512(passwordSalt))
            {
                var computedHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }

            return true;
        }

    }
}
