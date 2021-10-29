using App.Domain.Entities;
using App.Infrastructure.Persistence;
using App.Domain.Persistence.Repositories;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using App.Domain.Helpers;
using Microsoft.Extensions.Options;
using App.Domain.Dtos;
using static App.Domain.Enums.CommonConstant;

namespace App.Infrastructure.Repositories
{
    public class UserBusiness : IUserRepository
    {
        private readonly EcommerceContext _context;
        private readonly JWTSettings _jwtSettings;

        public UserBusiness(EcommerceContext ecommerceDBContext, IOptions<JWTSettings> jwtSettings)
        {
            _context = ecommerceDBContext;
            _jwtSettings = jwtSettings.Value;
        }

        public UserWithTokenDto LoginUser(LoginModel user)
        {
            var userLogin = _context.Users.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
            if (userLogin != null)
            {
                // Create Refresh Token
                RefreshToken refreshToken = GenerateRefreshToken();

                // (Create token)Sign your token here...     
                string token = GenerateAccessToken(userLogin);

                // Save token in database in order to Refresh token
                RefreshToken refreshModel = new RefreshToken
                {
                    UserId = userLogin.UserId,
                    Token = refreshToken.Token,
                    ExpiryDate = refreshToken.ExpiryDate
                };
                _context.RefreshTokens.Add(refreshModel);
                _context.SaveChanges();

                UserWithTokenDto userWithTokenDto = new UserWithTokenDto
                {
                    AccessToken = token,
                    RefreshToken = refreshToken.Token
                };

                return userWithTokenDto;
            }
            return null;
        }

        public string GenerateAccessToken(User userLogin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userLogin.UserId)),
                    new Claim(ClaimTypes.Email, userLogin.Email),
                    new Claim(ClaimTypes.Surname, userLogin.Fullname),
                    new Claim(ClaimTypes.SerialNumber, userLogin.IsActive.ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public User RegisterUser(RegisterUserModel model)
        {
            var userRegister = _context.Users.Where(u => u.Email == model.Email).FirstOrDefault();

            if (userRegister != null)
            {
                return null;
            }
            User user = new User
            {
                Email = model.Email,
                Password = model.Password,
                Fullname = model.Fullname,
                IsActive = 1,
                TypeUserId = UserTypeConstant.User
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddMinutes(120);

            return refreshToken;
        }
    }
}
