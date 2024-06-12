using Microsoft.AspNetCore.Identity;

namespace ShoeStore_Nhom2_Web2_API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> Roles);
    }
}
