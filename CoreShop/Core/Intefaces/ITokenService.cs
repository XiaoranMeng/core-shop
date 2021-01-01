using Core.Entities.Identity;

namespace Core.Intefaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
