using API.Core.DTOs;
using Application.Exceptions;
using DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Core
{
    public class JwtManager
    {
        private readonly WebApiPracticeContext _context;
        private readonly JwtSettings _settings;

        public JwtManager(WebApiPracticeContext context, JwtSettings settings)
        {
            _settings = settings;
            _context = context;
        }

        public TokenResponseDto MakeToken(TokenRequestDto request)
        {
            var user = _context.Users.Include(x => x.UserUseCases).FirstOrDefault(x => x.Email.ToLower() == request.Email.ToLower());

            if (user == null)
            {
                throw new CustomUnauthorizedAccessException($"Email: {request.Email}");
            }

            var valid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

            if (!valid)
            {
                throw new CustomUnauthorizedAccessException($"Email: {request.Email}");
            }

            var actor = new JwtUser
            {
                Id = user.Id,
                UseCaseIds = user.UserUseCases.Select(x => x.UseCaseId).ToList(),
                Identity = user.Email,
                Email = user.Email
            };

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, _settings.Issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _settings.Issuer),
                new Claim("UserId", actor.Id.ToString(), ClaimValueTypes.String, _settings.Issuer),
                new Claim("UserUseCases", JsonConvert.SerializeObject(actor.UseCaseIds)),
                new Claim("Email", user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_settings.Minutes),
                signingCredentials: credentials);

            return new TokenResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
