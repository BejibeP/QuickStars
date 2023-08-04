using QuickStars.MVApi.Business.Dtos.Auth;

namespace QuickStars.MVApi.Business.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResult<TokenDto>> Login(LoginDto dto);

        Task<ServiceResult<bool>> Register(RegisterDto dto);

        Task<ServiceResult<bool>> ResetPassword(ResetPasswordDto dto);
    }
}