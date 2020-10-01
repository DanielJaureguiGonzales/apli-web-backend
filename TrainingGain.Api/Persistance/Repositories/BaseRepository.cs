using TrainingGain.Api.Domain.Persistance.Context;

namespace TrainingGain.Api.Persistance.Repositories
{
    public class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
