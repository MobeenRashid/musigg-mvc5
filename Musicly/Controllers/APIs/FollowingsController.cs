using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Musicly.Core.Dtos;
using Musicly.Core.Models;
using Musicly.Persistence;

namespace Musicly.Controllers.APIs
{
    [Authorize]
    [RoutePrefix("api/UserFollowings")]
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _db;

        public FollowingsController()
        {
            _db = new ApplicationDbContext();
        }

        [HttpGet]
        [Route("Get")]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("Get/{id:int}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("Follow")]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            string userId = User.Identity.GetUserId();
            if (_db.Followings.Any(f => f.FolloweeId == userId && f.FolloweeId == dto.FolloweeId))
            {
                return BadRequest("Already Following that User");
            }

            Following following = new Following()
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };
            _db.Followings.Add(following);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("Unfollow")]
        public IHttpActionResult Unfollow(FollowingDto dto)
        {

            string followeeId = dto.FolloweeId;
            string followerId = User.Identity.GetUserId();

            var entityToDelete = _db.Followings.FirstOrDefault(f => f.FolloweeId == followeeId && f.FollowerId == followerId);

            _db.Followings.Remove(entityToDelete);
            _db.SaveChanges();

            return Ok();
        }
        // PUT: api/Followings/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Followings/5
        public void Delete(int id)
        {
        }

    }
}
