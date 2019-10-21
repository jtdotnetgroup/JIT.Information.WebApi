using System;
using Microsoft.IdentityModel.Tokens;

namespace JIT.InformationSystem.Authentication.JwtBearer
{
    public class TokenAuthConfiguration
    {
        public SymmetricSecurityKey SecurityKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public SigningCredentials SigningCredentials { get; set; }

        public TimeSpan Expiration { get; set; }

        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }

        public string ClientName { get; set; }
    }
}
