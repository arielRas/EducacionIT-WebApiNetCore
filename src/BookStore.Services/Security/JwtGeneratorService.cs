﻿using BookStore.Services.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Services.Security
{
    public class JwtGeneratorService
    {
        private readonly IConfigurationSection _jwtSettings;

        public JwtGeneratorService(IConfiguration configuration)
            => _jwtSettings = configuration.GetSection("JWT");

        public string Generate(UserLoggedDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings["SecretKey"]!));

            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = [
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             ];

            claims.AddRange(
                user.Roles.Select(role =>  new Claim(ClaimTypes.Role, role)
             ));

            var token = new JwtSecurityToken
            (
                signingCredentials: credential,
                issuer: _jwtSettings["Issuer"],
                audience: _jwtSettings["Audience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["ExpirationMinutes"])),
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
