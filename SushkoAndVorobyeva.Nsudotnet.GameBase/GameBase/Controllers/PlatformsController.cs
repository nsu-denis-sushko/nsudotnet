using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GameBase.Models;
using GameBase.Models.Database;
using Newtonsoft.Json.Linq;

namespace GameBase.Controllers
{
    public class PlatformsController : Controller
    {
        private readonly IDbContext _db;

        public PlatformsController(IDbContext db)
        {
            _db = db;
        }

        // GET: Platforms
        public ActionResult Index()
        {
            return View(_db.Set<Platform>().ToList());
        }

        // GET: Platforms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Platform platform = _db.Set<Platform>().Find(id);
            if (platform == null)
            {
                return HttpNotFound();
            }
            return View(platform);
        }

        // GET: Platforms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Platforms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlatformId,Name")] Platform platform)
        {
            if (ModelState.IsValid)
            {
                _db.Set<Platform>().Add(platform);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(platform);
        }

        // GET: Platforms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Platform platform = _db.Set<Platform>().Find(id);
            if (platform == null)
            {
                return HttpNotFound();
            }
            return View(platform);
        }

        // POST: Platforms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlatformId,Name")] Platform platform)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(platform).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(platform);
        }

        // GET: Platforms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Platform platform = _db.Set<Platform>().Find(id);
            if (platform == null)
            {
                return HttpNotFound();
            }
            return View(platform);
        }

        // POST: Platforms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Platform platform = _db.Set<Platform>().Find(id);
            _db.Set<Platform>().Remove(platform);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
