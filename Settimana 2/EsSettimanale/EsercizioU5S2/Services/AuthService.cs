using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using EsercizioU5S2.Models;

namespace EsercizioU5S2.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _connectionString;

        public AuthService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE UserName = @UserName", conn);
                cmd.Parameters.AddWithValue("@UserName", username);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        user = new User
                        {
                            UserId = (int)reader["UserId"],
                            UserName = reader["UserName"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            Email = reader["Email"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString()
                        };

                        Console.WriteLine($"Found user: {user.UserName}, {user.PasswordHash}");
                    }
                }
            }

            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                Console.WriteLine("Password verification succeeded.");
                return user;
            }

            Console.WriteLine("Password verification failed.");
            return null;
        }

        private bool VerifyPassword(string password, string storedPassword)
        {
            Console.WriteLine($"Comparing passwords: {password} == {storedPassword}");
            return password == storedPassword;
        }

        public async Task SignInAsync(HttpContext httpContext, User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FullName", $"{user.FirstName} {user.LastName}")
            };

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT r.RoleName FROM Roles r JOIN UserRoles ur ON r.RoleId = ur.RoleId WHERE ur.UserId = @UserId", conn);
                cmd.Parameters.AddWithValue("@UserId", user.UserId);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        claims.Add(new Claim(ClaimTypes.Role, reader["RoleName"].ToString()));
                    }
                }
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }
    }
}
