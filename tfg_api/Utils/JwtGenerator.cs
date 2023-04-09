using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace tfg_api.Utils
{
    public class JwtGenerator
    {
        private readonly JwtHeader jwtHeader;
        private readonly IList<Claim> jwtClaims;
        private readonly DateTime jwtDate;
        private readonly int tokenLifetimeInSeconds;

        public JwtGenerator(IConfiguration configuration)
        {

            var credentials = new SigningCredentials(
                key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:PrivateKey"])),
                algorithm: SecurityAlgorithms.HmacSha256
                );

            jwtHeader = new JwtHeader(credentials);
            jwtClaims = new List<Claim>();
            jwtDate = DateTime.UtcNow;
            tokenLifetimeInSeconds = int.Parse(configuration["Jwt:LifetimeInSeconds"]);


        }

        public JwtGenerator addClaim(Claim claim)
        {
            jwtClaims.Add(claim);
            return this;

        }

        public long GetTokenExpirationInUnixTime => new DateTimeOffset(jwtDate.AddSeconds(tokenLifetimeInSeconds)).ToUnixTimeMilliseconds();

        public string GetToken(IConfiguration configuration)
        {
            var jwt = new JwtSecurityToken(
                jwtHeader, new JwtPayload(
                    audience: configuration["Jwt:audience"],
                    issuer: configuration["Jwt:issuer"],
                    notBefore: jwtDate,
                    expires: jwtDate.AddSeconds(tokenLifetimeInSeconds),
                    claims: jwtClaims
                    )
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
