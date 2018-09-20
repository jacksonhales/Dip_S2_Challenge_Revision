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
    public class TreatmentsController : Controller
    {
        private Entities db = new Entities();

        // GET: Treatments
        public async Task<ActionResult> Index()
        {
            var treatments = db.Treatments.Include(t => t.Pet).Include(t => t.Procedure);
            return View(await treatments.ToListAsync());
        }

        // GET: Treatments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = await db.Treatments.FindAsync(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // GET: Treatments/Create
        public ActionResult Create()
        {
            ViewBag.PetID = new SelectList(db.Pets, "PetID", "Name");
            ViewBag.ProcedureID = new SelectList(db.Procedures, "ProcedureID", "Description");
            return View();
        }

        // POST: Treatments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TreatmentID,Date,Notes,Price,PetID,ProcedureID")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                db.Treatments.Add(treatment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PetID = new SelectList(db.Pets, "PetID", "Name", treatment.PetID);
            ViewBag.ProcedureID = new SelectList(db.Procedures, "ProcedureID", "Description", treatment.ProcedureID);
            return View(treatment);
        }

        // GET: Treatments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = await db.Treatments.FindAsync(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PetID = new SelectList(db.Pets, "PetID", "Name", treatment.PetID);
            ViewBag.ProcedureID = new SelectList(db.Procedures, "ProcedureID", "Description", treatment.ProcedureID);
            return View(treatment);
        }

        // POST: Treatments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TreatmentID,Date,Notes,Price,PetID,ProcedureID")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treatment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PetID = new SelectList(db.Pets, "PetID", "Name", treatment.PetID);
            ViewBag.ProcedureID = new SelectList(db.Procedures, "ProcedureID", "Description", treatment.ProcedureID);
            return View(treatment);
        }

        // GET: Treatments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = await db.Treatments.FindAsync(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // POST: Treatments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Treatment treatment = await db.Treatments.FindAsync(id);
            db.Treatments.Remove(treatment);
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
