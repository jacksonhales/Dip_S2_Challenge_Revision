using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Challenge_Revision.ASP.NET.Models;

namespace Challenge_Revision.ASP.NET.Controllers
{
    public class PetsController : Controller
    {
        private Entities db = new Entities();

        // GET: Pets
        public async Task<ActionResult> Index()
        {
            var pets = db.Pets.Include(p => p.Owner);
            return View(await pets.ToListAsync());
        }

        // GET: Pets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = await db.Pets.FindAsync(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // GET: Pets/Create
        public ActionResult Create()
        {
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "Surname");
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PetID,Name,Type,OwnerID")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                db.Pets.Add(pet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "Surname", pet.OwnerID);
            return View(pet);
        }

        // GET: Pets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = await db.Pets.FindAsync(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "Surname", pet.OwnerID);
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PetID,Name,Type,OwnerID")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "Surname", pet.OwnerID);
            return View(pet);
        }

        // GET: Pets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = await db.Pets.FindAsync(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Pet pet = await db.Pets.FindAsync(id);
            db.Pets.Remove(pet);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
