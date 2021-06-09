using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Services
{
    public interface ILoginService
    {
        TokenVO ValidadeCredentials(UserVO user);

    }
}
