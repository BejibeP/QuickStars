namespace QuickStars.MVApi.Business.Dtos
{
    public class LogMessageDto
    {
        public long Id { get; set; }

        public string Message { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}