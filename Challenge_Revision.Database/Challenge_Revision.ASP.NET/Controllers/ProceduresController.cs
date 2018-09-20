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
    public class ProceduresController : Controller
    {
        private Entities db = new Entities();

        // GET: Procedures
        public async Task<ActionResult> Index()
        {
            return View(await db.Procedures.ToListAsync());
        }

        // GET: Procedures/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedure procedure = await db.Procedures.FindAsync(id);
            if (procedure == null)
            {
                return HttpNotFound();
            }
            return View(procedure);
        }

        // GET: Procedures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Procedures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProcedureID,Description")] Procedure procedure)
        {
            if (ModelState.IsValid)
            {
                db.Procedures.Add(procedure);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(procedure);
        }

        // GET: Procedures/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedure procedure = await db.Procedures.FindAsync(id);
            if (procedure == null)
            {
                return HttpNotFound();
            }
            return View(procedure);
        }

        // POST: Procedures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProcedureID,Description")] Procedure procedure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(procedure).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(procedure);
        }

        // GET: Procedures/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Procedure procedure = await db.Procedures.FindAsync(id);
            if (procedure == null)
            {
                return HttpNotFound();
            }
            return View(procedure);
        }

        // POST: Procedures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Procedure procedure = await db.Procedures.FindAsync(id);
            db.Procedures.Remove(procedure);
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
