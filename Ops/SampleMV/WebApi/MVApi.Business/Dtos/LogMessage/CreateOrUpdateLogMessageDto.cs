using System.ComponentModel.DataAnnotations;

namespace QuickStars.MVApi.Business.Dtos
{
    public class CreateOrUpdateLogMessageDto
    {
        [Required]        
        public string Message { get; set; }
    }
}