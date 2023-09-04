using TheJituEcommerce_Auth.Models;

namespace TheJituEcommerce_Auth.Services.IService
{
    public interface IJwtInterface
    {
        string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
