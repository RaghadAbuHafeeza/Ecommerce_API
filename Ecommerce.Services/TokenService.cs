using Ecommerce.Core.Entities;
using Ecommerce.Core.IRepositories.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<LocalUser> userManager;
        private readonly string secretKey; 

        public TokenService(IConfiguration configuration , UserManager<LocalUser> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            // Fetch the secret key from the configuration file under the "ApiSettings" section.
            secretKey = configuration.GetSection("ApiSettings")["SecretKey"];
        }

        public async Task<string> CreateTokenAsync(LocalUser localUser)
        {
            // Convert the secret key into a byte array for use in token signing.
            var key = Encoding.ASCII.GetBytes(secretKey);

            // Retrieve the roles assigned to the user.
            var roles = await userManager.GetRolesAsync(localUser);

            // Initialize a list to hold the user's claims (identity and roles).
            var claims = new List<Claim>
            {
                // Add a claim for the user's name (FirstName in this case).
                new Claim(ClaimTypes.Name , localUser.FirstName),
            };

            // Add a claim for each role the user has, identifying the user's access level(s).
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // Define the details of the JWT token.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Attach the claims to the token for the recipient to verify user details and roles.
                Subject = new ClaimsIdentity(claims),
                // Set the token's expiration date to 5 days from the current UTC time.
                Expires = DateTime.UtcNow.AddDays(5),
                // Sign the token using the secret key and the HMAC SHA-256 algorithm for security.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key) , SecurityAlgorithms.HmacSha256Signature)
            };

            // JwtSecurityTokenHandler is a tool that helps create access to tokens and JWT
            var tokenHandler = new JwtSecurityTokenHandler();

            // Generate the token using the defined descriptor.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the generated token as a string for use in authentication.
            return tokenHandler.WriteToken(token);
        }
    }
}
