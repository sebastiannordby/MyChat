using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using MyChat.DTO;

namespace MyChat.Classes
{
    public class TokenIssuer
    {
        public static string IssueToken(UserDto user)
        {
            var jwToken = new JwtSecurityToken(
                issuer: SystemVariables.TokenIssuer,
                audience: SystemVariables.Audience,
                claims: GetUserClaims(user),
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(SystemVariables.SecretKey),
                algorithm: SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwToken);
        }

        private static Claim[] GetUserClaims(UserDto user)
        {
            return new Claim[]
            {
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("EmailAddress", user.EmailAddress),
                new Claim("UserName", user.UserName)
            };
        }
    }
}
