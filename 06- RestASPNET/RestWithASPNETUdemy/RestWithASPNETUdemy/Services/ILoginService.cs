using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Services
{
    public interface ILoginService
    {
        TokenVO ValidadeCredentials(UserVO user);
        TokenVO ValidadeCredentials(TokenVO token);

        bool RevokeToken(string userName);

    }
}
