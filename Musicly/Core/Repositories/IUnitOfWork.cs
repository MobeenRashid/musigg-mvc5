namespace Musicly.Core.Repositories
{
    public interface IUnitOfWork
    {
        IGigsRepository Gigs { get; }
        IAttendanceRepository Attendances { get; }
        IFollowingRepository Followings { get; }
        IArtistRepository Artists { get; }
        void Complete();
    }
}