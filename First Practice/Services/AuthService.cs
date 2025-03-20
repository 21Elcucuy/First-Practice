using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using First_Practice.Helper;
using First_Practice.Models;
using First_Practice.Models.Domain;
using First_Practice.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;

namespace First_Practice.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<ApplicationUser> userManager ,JWT jwt , RoleManager<IdentityRole> roleManager) 
        {
         _userManager = userManager;
           _jwt = jwt;
            _roleManager = roleManager;
        }

       

        public async Task<AuthModel> RegisterAsync(RegisterDTO register)
        {
            if (await _userManager.FindByEmailAsync(register.Email) is not null)
                return new AuthModel { Message = "Email is Already registerd" };
            if (await _userManager.FindByNameAsync(register.UserName) is not null)
                return new AuthModel { Message = "UserName is Already Exist" };
            var user = new ApplicationUser
            {
                UserName = register.UserName,
                Email = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName,

            };
            var Result = await _userManager.CreateAsync(user, register.Password);
            if (!Result.Succeeded)
            {
                return new AuthModel { Message = "Something went Wrong" };
            }
            var jwtSecurityToken = await GenerateToken(user);
            return new AuthModel
            {
                IsAuthenticated = true,
                Email = user.Email,
                Roles = new List<string> { "User" },
                ExpiresOn = jwtSecurityToken.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName,
            };
               

        }
        public async Task<AuthModel> Login(LoginDTO login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user is null || await _userManager.CheckPasswordAsync(user, login.Password))
                return new AuthModel { Message  = "Email or password is incorrect"};
           var jwtSecurityToken =  await GenerateToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            
            return new AuthModel
            {
                IsAuthenticated = true,
                Email = user.Email,
                Roles = roles.ToList(),
                ExpiresOn = jwtSecurityToken.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName,
            };


        }
      
        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var claimuser = await _userManager.GetClaimsAsync(user);
            var Roles = await _userManager.GetRolesAsync(user);
            var roleClaim = new List<Claim>();
            foreach (var role in Roles)
            {
                roleClaim.Add(new Claim("roles", role));
            }
            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Sub , user.UserName),
               new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
               new Claim(JwtRegisteredClaimNames.Email , user.Email),
               new Claim("uid" , user.Id)
            }
            .Union(claimuser)
            .Union(roleClaim);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);

            var jwtSecurityKey = new JwtSecurityToken
                 (
                 issuer: _jwt.Issuer,
                 audience: _jwt.Audience,
                 claims: claims,
                 signingCredentials: signingCredentials,
                 expires: DateTime.UtcNow.AddDays(_jwt.DurationInDays)

                );
            return jwtSecurityKey;

        }

        public async Task<string> AddRole(RoleDTO role)
        {
            var user = await _userManager.FindByIdAsync(role.UserId);
            if (user is null || ! await _roleManager.RoleExistsAsync(role.RoleName))
                return "Invalid user ID or Role";
            if (await _userManager.IsInRoleAsync(user, role.RoleName))
                return "User Already assigned to this role";

            var Result = await _userManager.AddToRoleAsync(user, role.RoleName);

            if(Result.Succeeded)
                return string.Empty;

            return "Something went wrong";


        }
    }
}
