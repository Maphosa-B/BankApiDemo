using Microsoft.AspNetCore.Identity;

namespace BankApiDemo.Services
{
    public interface IAuthService
    {
        public string GenerateToken(IdentityUser user);
    }
}
