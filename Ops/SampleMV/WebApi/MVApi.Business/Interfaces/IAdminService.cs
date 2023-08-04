using QuickStars.MVApi.Business.Dtos.Auth;

namespace QuickStars.MVApi.Business.Interfaces
{
    public interface IAdminService
    {
        Task<ServiceResult<bool>> ResetUserPassword(ResetPasswordDto dto);
        Task<ServiceResult<bool>> AddUserRole(UserRoleDto dto);
        Task<ServiceResult<bool>> DeleteUserRole(UserRoleDto dto);
    }
}