using First_Practice.Models;
using First_Practice.Models.DTO;

namespace First_Practice.Services
{
    public interface IAuthService
    {
        public Task<AuthModel> RegisterAsync(RegisterDTO register);
        public Task<AuthModel> Login(LoginDTO login);
        public Task<string> AddRole(RoleDTO role);
    }
}
