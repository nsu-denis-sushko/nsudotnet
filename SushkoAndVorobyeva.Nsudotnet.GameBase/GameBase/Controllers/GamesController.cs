using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using GameBase.Models.Database;

namespace GameBase.Controllers
{
    public class GamesController : Controller
    {
        private readonly IDbContext _db;

        public GamesController(IDbContext db)
        {
            _db = db;
 
        }

        // GET: Games
        public ActionResult Index(string sortOrder, string searchString)
        {
            
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";
            var games = from s in _db.Set<Game>().Include(g => g.Developer).Include(g => g.Publisher)
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(s => s.GameName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Name desc":
                    games = games.OrderByDescending(s => s.GameName);
                    break;
                case "Date":
                    games = games.OrderBy(s => s.ReleaseDate);
                    break;
                case "Date desc":
                    games = games.OrderByDescending(s => s.ReleaseDate);
                    break;
                default:
                    games = games.OrderBy(s => s.GameName);
                    break;
            }
           

            return View(games.ToList());
        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = _db.Set<Game>().Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.DevelopersId = new SelectList(_db.Set<Developer>(), "DeveloperId", "DeveloperName");
            ViewBag.PublisherId = new SelectList(_db.Set<Publisher>(), "PublisherId", "PublisherName");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameId,GameName,Serial,DevelopersId,PublisherId,ReleaseDate,Description,Multiplayer,Age,RatingId,Photo")] Game game, HttpPostedFileBase uploadImage)
        {
            if (uploadImage != null) { 
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                game.Photo = imageData;
            }

            if (ModelState.IsValid)
            {

                _db.Set<Game>().Add(game);
                _db.SaveChanges();
                return RedirectToAction("Index", "Games");
            }

            ViewBag.DevelopersId = new SelectList(_db.Set<Developer>(), "DeveloperId", "DeveloperName", game.DevelopersId);
            ViewBag.PublisherId = new SelectList(_db.Set<Publisher>(), "PublisherId", "PublisherName", game.PublisherId);
            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = _db.Set<Game>().Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.DevelopersId = new SelectList(_db.Set<Developer>(), "DeveloperId", "DeveloperName", game.DevelopersId);
            ViewBag.PublisherId = new SelectList(_db.Set<Publisher>(), "PublisherId", "PublisherName", game.PublisherId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameId,GameName,Serial,DevelopersId,PublisherId,ReleaseDate,Description,Multiplayer,Age,RatingId,Photo")] Game game, HttpPostedFileBase uploadImage)
        {

            if (uploadImage != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                game.Photo = imageData;
            }

            if (ModelState.IsValid)
            {
                _db.Entry(game).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DevelopersId = new SelectList(_db.Set<Developer>(), "DeveloperId", "DeveloperName", game.DevelopersId);
            ViewBag.PublisherId = new SelectList(_db.Set<Publisher>(), "PublisherId", "PublisherName", game.PublisherId);
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = _db.Set<Game>().Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = _db.Set<Game>().Find(id);
            _db.Set<Game>().Remove(game);
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
