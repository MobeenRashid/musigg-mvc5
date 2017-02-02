using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Musicly.Core.Models;
using Musicly.Core.Repositories;

namespace Musicly.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _db;

        public AttendanceRepository(ApplicationDbContext db)
        {
           _db = db;
        }

        public IEnumerable<Attendance> GetFutureAttendancesForUser(string userId)
        {
            return _db.Attendances
                .Where(at => at.ArtistId == userId && at.Gig.DateTime > DateTime.Now)
                .ToList();
        }

        public IEnumerable<Attendance> GetAttendancesForGig(int gigId)
        {
            return _db.Attendances.Where(at => at.GigId == gigId).ToList();
        }

        public bool IsGoing(int gigId, string userId)
        {
            var attendances = GetAttendancesForGig(gigId);
            return attendances.Any(at => at.ArtistId == userId && at.GigId == gigId);
        }

        public bool Exist(int gigId, string userId)
        {
            return _db.Attendances.Any(at => at.ArtistId == userId && at.GigId == gigId);
        }

        public void AddAttendance(Attendance attendance)
        {
            _db.Attendances.Add(attendance);
        }

        public bool RemoveAttendance(int gigId, string userId)
        {
            var attendance=_db.Attendances.FirstOrDefault(a => a.GigId == gigId && a.ArtistId == userId);
            if (attendance != null)
            {
                _db.Entry(attendance).State = EntityState.Deleted;
                return true;
            }
            return false;
        }
    }
}