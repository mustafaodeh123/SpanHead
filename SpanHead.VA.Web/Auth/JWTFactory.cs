namespace SpanHead.VA.Web.Auth
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using SpanHead.VA.DTO.Account;
    using SpanHead.VA.Web.Common;
    using SpanHead.VA.Web.Models;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;

    public interface IJWTFactory {
        Task<AppUser> GetEncodedToken(AppUser appUser);
    }

    public class JWTFactory : IJWTFactory
    {
        private readonly JwtIssuerOptions jwtOptions;
        private readonly ILogger logger;
        private readonly JsonSerializerSettings serializerSettings;

        public JWTFactory(IOptions<JwtIssuerOptions> jwtOptions, ILoggerFactory loggerFactory)
        {
            this.jwtOptions = jwtOptions.Value;
            this.jwtOptions.CheckForValidOption();
            this.logger = loggerFactory.CreateLogger<JWTFactory>();
            this.serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
        }

        public async Task<AppUser> GetEncodedToken(AppUser appUser)
        {
            var identity = await GetClaimsIdentity(appUser);
           
            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName),
              new Claim(JwtRegisteredClaimNames.Jti, await this.jwtOptions.JtiGenerator()),
              new Claim(JwtRegisteredClaimNames.Iat,this.jwtOptions.IssuedAt.ToUnixEpochDate().ToString(),
                  ClaimValueTypes.Integer64), identity.FindFirst("spanHeadUser")
            };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: this.jwtOptions.Issuer,
                audience: this.jwtOptions.Audience,
                claims: claims,
                notBefore: this.jwtOptions.NotBefore,
                expires: this.jwtOptions.Expiration,
                signingCredentials: this.jwtOptions.SigningCredentials);

            appUser.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt); ;
            appUser.ExpiresIn = (int)this.jwtOptions.ValidFor.TotalSeconds;

            return appUser;
        }
#pragma warning disable CS1998
        private async Task<ClaimsIdentity> GetClaimsIdentity(AppUser appUser)
#pragma warning restore CS1998
        {
              return new ClaimsIdentity(
                  new GenericIdentity(appUser.UserName, "Token"),
                  new[] { new Claim("spanHeadUser", appUser.UserName) });
        }
    }
}
