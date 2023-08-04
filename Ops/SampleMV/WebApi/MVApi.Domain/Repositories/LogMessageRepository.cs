using QuickStars.MVApi.Domain.Data.Entities;
using QuickStars.MVApi.Domain.Interfaces;

namespace QuickStars.MVApi.Domain.Repositories
{
    public class LogMessageRepository : BaseRepository<LogMessage>, ILogMessageRepository
    {
        public LogMessageRepository(DatabaseContext databaseContext) : base(databaseContext)
        {

        }
    }
}