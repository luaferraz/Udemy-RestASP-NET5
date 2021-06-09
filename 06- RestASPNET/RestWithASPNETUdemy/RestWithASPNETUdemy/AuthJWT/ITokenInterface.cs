using System.Collections.Generic;
using System.Security.Claims;

namespace RestWithASPNETUdemy.AuthJWT
{
    public interface ITokenInterface
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
