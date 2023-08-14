using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR.LeaveManagement.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
           var user = await _userManager.FindByEmailAsync(request.Email);
           if (user == null) 
            {
                throw new ApplicationException($"User with {request.Email} not found");
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);
            if (!result.Succeeded) 
            {
                throw new Exception($"Credentials for '{request.Email} aren't valid'");
            }

        JwtSecurityToken JwtSecurityToken = await GenerateToken(user);

        AuthResponse response = new AuthResponse
        {
             Id = user.Id,
             Token= new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken),
             Email=user.Email,
             UserName=user.UserName
        };
            return response;
    }
        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles) 
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName) ,
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) ,
                new Claim(JwtRegisteredClaimNames.Email, user.Email) ,
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricalSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signinngCredentials = new SigningCredentials(symmetricalSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(                
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signinngCredentials
                );
            return jwtSecurityToken;

        }
    public Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
