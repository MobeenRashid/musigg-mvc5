using System.Collections.Generic;
using Musicly.Core.Models;

namespace Musicly.Core.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendancesForUser(string userId);
        IEnumerable<Attendance> GetAttendancesForGig(int gigId);
        bool IsGoing(int gigId, string userId);
        bool Exist(int gigId, string userId);
        void AddAttendance(Attendance attendance);
        bool RemoveAttendance(int gigId, string userId);
    }
}