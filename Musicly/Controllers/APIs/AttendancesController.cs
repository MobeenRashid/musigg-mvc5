using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Musicly.Core.Dtos;
using Musicly.Core.Models;
using Musicly.Core.Repositories;

namespace Musicly.Controllers.APIs
{
    [Authorize]
    [RoutePrefix("api/UserAttendances")]
    public class AttendancesController : NotificationsController
    {
        private readonly IUnitOfWork _unitOfWork;
        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/Attendances
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET: api/Attendances/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("AddAttendance")]
        public IHttpActionResult AddAttendance(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();


            if (_unitOfWork.Attendances.Exist(dto.GigId, userId))
            {
                return BadRequest("The attendance already exist");
            }

            var attendance = new Attendance()
            {
                GigId = dto.GigId,
                ArtistId = userId
            };

            _unitOfWork.Attendances.AddAttendance(attendance);
            _unitOfWork.SaveWork();

            return Ok();
        }

        [HttpPost]
        [Route("RemoveAttendance")]
        public IHttpActionResult RemoveAttendance(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_unitOfWork.Attendances.RemoveAttendance(dto.GigId, userId))
            {
                _unitOfWork.SaveWork();
                return Ok();
            }

            return BadRequest("Attendance not exists");
        }
    }
}
