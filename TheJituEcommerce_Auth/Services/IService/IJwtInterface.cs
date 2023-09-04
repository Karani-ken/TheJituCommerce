using TheJituEcommerce_Auth.Models;

namespace TheJituEcommerce_Auth.Services.IService
{
    public interface IJwtInterface
    {
        //user can have several roles thus IEnumerable roles 
        string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
