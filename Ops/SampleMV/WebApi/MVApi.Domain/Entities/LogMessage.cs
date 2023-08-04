namespace QuickStars.MVApi.Domain.Data.Entities
{
    public class LogMessage : BaseEntity
    {
        public string Message { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}