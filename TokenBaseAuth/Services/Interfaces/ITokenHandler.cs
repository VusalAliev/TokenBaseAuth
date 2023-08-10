using TokenBaseAuth.DTOs;

namespace TokenBaseAuth.Services.Interfaces
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int minute);
    }
}
