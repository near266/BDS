using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Jhipster.Infrastructure.Configuration;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Jhipster.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Jhipster.Domain;
using LanguageExt;
using Jhipster.Infrastructure.Data;

namespace Jhipster.Security.Jwt
{
    public interface ITokenProvider
    {
        Task<string> CreateToken(IPrincipal principal, bool rememberMe);

        ClaimsPrincipal TransformPrincipal(ClaimsPrincipal principal);
        Task<IEnumerable<Claim>> GetRole(IPrincipal principal);
    }


    public class TokenProvider : ITokenProvider
    {
        private const string AuthoritiesKey = "auth";

        private readonly SecuritySettings _securitySettings;

        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<TokenProvider> _log;
        private readonly ApplicationDatabaseContext _context;
        private SigningCredentials _key;

        private long _tokenValidityInSeconds;

        private long _tokenValidityInSecondsForRememberMe;


        public TokenProvider(ILogger<TokenProvider> log, IOptions<SecuritySettings> securitySettings, UserManager<User> userManager,ApplicationDatabaseContext applicationDatabaseContext)
        {
            _log = log;
            _securitySettings = securitySettings.Value;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _context = applicationDatabaseContext;
            _userManager = userManager;
            Init();
        }

        public async Task<string> CreateToken(IPrincipal principal, bool rememberMe)
        {
            var roles = GetRoles(principal);

            var authValue = string.Join(",", roles.Map(it => it.Value));

            var id = principal is ClaimsPrincipal user
             ? user.FindFirst(it => it.Type == ClaimTypes.NameIdentifier)?.Value
             : string.Empty;

            var fullName = GetNameCus(id);
            var subject = CreateSubject(principal, fullName);
            var validity =
                DateTime.UtcNow.AddSeconds(rememberMe
                    ? _tokenValidityInSecondsForRememberMe
                    : _tokenValidityInSeconds);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = validity,
                SigningCredentials = _key
            };

            var token = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            return _jwtSecurityTokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal TransformPrincipal(ClaimsPrincipal principal)
        {
            var currentIdentity = (ClaimsIdentity)principal.Identity;
            var roleClaims = principal
                .Claims
                .Filter(it => it.Type == AuthoritiesKey).First().Value
                .Split(",")
                .Map(role => new Claim(ClaimTypes.Role, role))
                .ToList();

            return new ClaimsPrincipal(
                new ClaimsIdentity(
                    principal.Claims.Union(roleClaims),
                    currentIdentity.AuthenticationType,
                    currentIdentity.NameClaimType,
                    currentIdentity.RoleClaimType
                )
            );
        }

        private void Init()
        {
            byte[] keyBytes;
            var secret = _securitySettings.Authentication.Jwt.Secret;

            if (!string.IsNullOrWhiteSpace(secret))
            {
                _log.LogWarning("Warning: the JWT key used is not Base64-encoded. " +
                                "We recommend using the `security.authentication.jwt.base64-secret` key for optimum security.");
                keyBytes = Encoding.ASCII.GetBytes(secret);
            }
            else
            {
                _log.LogDebug("Using a Base64-encoded JWT secret key");
                keyBytes = Convert.FromBase64String(_securitySettings.Authentication.Jwt.Base64Secret);
            }

            _key = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature);
            _tokenValidityInSeconds = _securitySettings.Authentication.Jwt.TokenValidityInSeconds;
            _tokenValidityInSecondsForRememberMe =
                _securitySettings.Authentication.Jwt.TokenValidityInSecondsForRememberMe;
        }

        private static ClaimsIdentity CreateSubject(IPrincipal principal, string fullName)
        {
            var username = principal.Identity.Name;
            var userid = principal is ClaimsPrincipal user
              ? user.FindFirst(it => it.Type == ClaimTypes.NameIdentifier)?.Value
              : string.Empty;

            var roles = GetRoles(principal);
            var authValue = string.Join(",", roles.Map(it => it.Value));
            return new ClaimsIdentity(new[] {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(AuthoritiesKey, authValue),
                new Claim(JwtRegisteredClaimNames.Sid,userid),
                new Claim(JwtRegisteredClaimNames.Name,fullName)
            });
        }

        private static IEnumerable<Claim> GetRoles(IPrincipal principal)
        {
            return principal is ClaimsPrincipal user
                ? user.FindAll(it => it.Type == ClaimTypes.Role)
                : Enumerable.Empty<Claim>();
        }

        public async Task<string> GetFullName(string userId)
        {
            return await _userManager.FindByIdAsync(userId).Select(i => i.FirstName);
        }
        private string GetNameCus(string userId)
        {
            var check= _context.Customers.FirstOrDefault(i=>i.Id==Guid.Parse(userId));
            if(check!=null)
            {
                return check.CustomerName;
            }    
            return _context.Users.FirstOrDefault(i=>i.Id==userId).FirstName;
        }

        public async Task<IEnumerable<Claim>> GetRole(IPrincipal principal)
        {

            return principal is ClaimsPrincipal user
            ? user.FindAll(it => it.Type == ClaimTypes.Role)
            : Enumerable.Empty<Claim>();

        }
    }
}
