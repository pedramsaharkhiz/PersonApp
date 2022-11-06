using API.Models;

namespace API.Interface
{
    public interface ITokenService
    {
         public string CreateToken(User user);
    }
}