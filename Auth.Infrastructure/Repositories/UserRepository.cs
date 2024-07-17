using Auth.Application.Contracts;
using Auth.Application.DTOs;
using Auth.Domain.Entites;
using Auth.Infrastructure.DataContext;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repositories
{
    public class UserRepository : IUser
    {
        private  readonly  AppDbContext     _context;
        private readonly IConfiguration _configuration;
        public UserRepository(AppDbContext context , IConfiguration configuration) {

            _context = context;
            _configuration = configuration;
        }
        public async Task<LoginResponse> LoginAsync(LoginDto loginDto)
        {
            var getUser = await FindUserByEmail(loginDto.email!);
            if (getUser == null) {
                return new  LoginResponse(false, "User not Found");

            }
            var checkPassword = BCrypt.Net.BCrypt.Verify(loginDto.password , getUser.password);
            if (checkPassword)
            {
                return new LoginResponse(true, "Login Succfuly", GetnerateToken(getUser));
            }
            else return new LoginResponse(false, "login filed");

        }

        private string GetnerateToken(User user)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier , user.id.ToString()),
                new Claim(ClaimTypes.Name , user.firstName.ToString()),
                new Claim(ClaimTypes.Email , user.email.ToString()),
                new Claim(ClaimTypes.Role , user.userType.ToString()),
            };
            var token = new JwtSecurityToken(
                  issuer: _configuration["Jwt : Issur"],
                  audience: _configuration["Jwt : Audience"],
                  claims: userClaims,
                  expires: DateTime.Now.AddDays(1),
                  signingCredentials : credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async  Task<User> FindUserByEmail(string email ) => await _context.Users.FirstOrDefaultAsync(u => u.email == email);

        public async Task<RegisterResponse> RegisterAsync(RegisterDto registerDto)
        {
            var getUser = await FindUserByEmail(registerDto.email);
            if (getUser != null)
            
                return new RegisterResponse(false, "User aready exsit");
            
            _context.Users.Add(new User()
            {
                id = (Guid.NewGuid()).ToString(),
                firstName = registerDto.firstName,
                lastName  = registerDto.lastName,
                email = registerDto.email,
                phoneNumber=registerDto.phoneNumber,
                userType= "Admin", 
                password=BCrypt.Net.BCrypt.HashPassword(registerDto.password),
                createdAt   = DateTime.UtcNow,
                lastLoginDate = DateTime.UtcNow,
  
            });
            await _context.SaveChangesAsync();
            return new RegisterResponse(true, "registration completed");
        }
    }
}
