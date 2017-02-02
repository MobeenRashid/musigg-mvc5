using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Musicly.Persistence;

namespace Musicly.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController()
        {
            _db = new ApplicationDbContext();
        }
        // GET: Admin
        public ActionResult Index()
        {
            var gigs = _db.Gigs.Select(g => g).Include(g=>g.Genre).Include(g=>g.Artist).Include(g=>g.Attendances).ToList();
            
            return View(gigs);
        }
    }
}