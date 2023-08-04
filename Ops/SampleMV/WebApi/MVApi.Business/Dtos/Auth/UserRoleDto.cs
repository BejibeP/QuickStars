using System.ComponentModel.DataAnnotations;

namespace QuickStars.MVApi.Business.Dtos.Auth
{
    public class UserRoleDto
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
    }
}