using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GameBase.Models;
using GameBase.Models.Database;

namespace GameBase.Controllers
{
    public class DevelopersController : Controller
    {
        private readonly IDbContext _db;

        public DevelopersController(IDbContext db)
        {
            _db = db;
        }

        // GET: Developers
        public ActionResult Index()
        {
            var developers = _db.Set<Developer>().Include(d => d.Country);
            return View(developers.ToList());
        }

        // GET: Developers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developer developer = _db.Set<Developer>().Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            return View(developer);
        }

        // GET: Developers/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(_db.Set<Developer>(), "CountryId", "Name");
            return View();
        }

        // POST: Developers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeveloperId,DeveloperName,CountryId")] Developer developer)
        {
            if (ModelState.IsValid)
            {
                _db.Set<Developer>().Add(developer);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(_db.Set<Developer>(), "CountryId", "Name", developer.CountryId);
            return View(developer);
        }

        // GET: Developers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developer developer = _db.Set<Developer>().Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(_db.Set<Developer>(), "CountryId", "Name", developer.CountryId);
            return View(developer);
        }

        // POST: Developers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeveloperId,DeveloperName,CountryId")] Developer developer)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(developer).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(_db.Set<Developer>(), "CountryId", "Name", developer.CountryId);
            return View(developer);
        }

        // GET: Developers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developer developer = _db.Set<Developer>().Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            return View(developer);
        }

        // POST: Developers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Developer developer = _db.Set<Developer>().Find(id);
            _db.Set<Developer>().Remove(developer);
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
