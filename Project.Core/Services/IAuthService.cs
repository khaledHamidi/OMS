using OMS.Core.DTOs.Users;

namespace OMS.Core.Services;
public interface IAuthService
{
    Task Register(UserRegisterDto userRegisterDto);
    Task<UserLoggedDto> Authenticate(UserLoginDto userLoginDto);
    Task<Dictionary<string, string>> RefreshToken(int userId);
    bool CheckExpires(string refreshToken);
}