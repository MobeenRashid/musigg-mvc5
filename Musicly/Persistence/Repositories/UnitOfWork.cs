using Musicly.Core.Repositories;

namespace Musicly.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IGigsRepository Gigs { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public IFollowingRepository Followings { get; private set; }
        public IArtistRepository Artists { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            Gigs= new GigsRepository(_dbContext); 
            Attendances =new AttendanceRepository(_dbContext);
            Followings =new FollowingRepository(_dbContext);
            Artists =new ArtistRepository(_dbContext);
        }

        public void Complete()
        {
            _dbContext.SaveChanges();
        }
    }
}