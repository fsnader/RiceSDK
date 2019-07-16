using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Rice.SDK.Authentication
{
    public class TokenBuilder
    {
        private readonly string _issuer;
        private readonly string _key;
        private int _expirationMinutes = 30;
        private readonly List<Claim> _claims = new List<Claim>();

        public TokenBuilder(string issuer,
            string key)
        {
            _issuer = issuer;
            _key = key;
        }

        public TokenBuilder WithExpirationMinutes(int expirationMinutes)
        {
            _expirationMinutes = expirationMinutes;
            return this;
        }

        public TokenBuilder WithClaims(IEnumerable<Claim> claims)
        {
            _claims.AddRange(claims);
            return this;
        }

        public TokenBuilder WithRoles(IEnumerable<string> roles)
        {
            _claims.AddRange(roles
                .Select(x => new Claim(ClaimTypes.Role,
                    x)));

            return this;
        }

        public TokenBuilder WithUsername(string username)
        {
            _claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
            return this;
        }
        

        public string Build()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_issuer,
                _issuer, _claims,
                expires: DateTime.Now.AddMinutes(_expirationMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
