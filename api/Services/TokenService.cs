using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace api.Services
{
    public class TokenService : ITokenService
    {
        // Fields for configuration, key, issuer, and audience
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly string? _issuer;
        private readonly string? _audience;


        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["JWT:Key"] ?? ""));
            _issuer = _config["JWT:Issuer"] ?? "";
            _audience = _config["JWT:Audience"] ?? "";
        }

        // Method to create a JWT token for a given user
        public string CreateToken(User user)
        {
            // Create a list of claims based on the user's information
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName ?? string.Empty)
        };

            // Create signing credentials using the symmetric key and HMACSHA512 algorithm
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            // Define the token's properties (claims, expiration, issuer, audience, signing credentials)
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // Set claims
                Expires = DateTime.Now.AddDays(7), // Set expiration date (7 days)
                SigningCredentials = creds,
                Issuer = _issuer,
                Audience = _audience
            };

            // Create a token handler to generate the token
            var tokenHandler = new JwtSecurityTokenHandler();

            // Create the token based on the token descriptor
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the token as a string
            return tokenHandler.WriteToken(token);
        }
    }

}