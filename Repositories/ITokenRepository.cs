using Microsoft.AspNetCore.Identity;

namespace IndiaWalks.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
