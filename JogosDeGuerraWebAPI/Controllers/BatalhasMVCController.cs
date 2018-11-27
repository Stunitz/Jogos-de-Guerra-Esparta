using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JogosDeGuerraModel;

namespace JogosDeGuerraWebAPI.Controllers
{
    public class BatalhasMVCController : Controller
    {
        private ModelJogosDeGuerra db = new ModelJogosDeGuerra();

        // GET: BatalhasMVC
        public ActionResult Index()
        {
            var batalhas = 
                db.Batalhas
                .Include(b => b.ExercitoBranco)
                .Include(b => b.ExercitoBranco.Usuario)
                .Include(b => b.ExercitoPreto)
                .Include(b => b.ExercitoPreto.Usuario)
                .Include(b => b.Tabuleiro)
                .Include(b => b.Turno)
                .Include(b => b.Turno.Usuario)
                .Include(b => b.Vencedor)
                .ToList();
            return View(batalhas);
        }

        // GET: BatalhasMVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batalha batalha = db.Batalhas.Find(id);
            if (batalha == null)
            {
                return HttpNotFound();
            }
            return View(batalha);
        }

        public ActionResult Iniciar(int id)
        {
            var batalhaController = new BatalhasController();
            var batalha = batalhaController.IniciarBatalha(id);
            //TODO: verificar se é a tela correta
            return RedirectToAction("Tabuleiro", new { id = id });
        }

        public ActionResult Tabuleiro(int id)
        {
            var batalha = db.Batalhas.Find(id);
            return View(batalha);
        }

        // GET: BatalhasMVC/Create
        public ActionResult Create()
        {
            ViewBag.ExercitoBrancoId = new SelectList(db.Exercitos, "Id", "Id");
            ViewBag.ExercitoPretoId = new SelectList(db.Exercitos, "Id", "Id");
            ViewBag.TabuleiroId = new SelectList(db.Tabuleiroes, "Id", "Id");
            ViewBag.TurnoId = new SelectList(db.Exercitos, "Id", "Id");
            ViewBag.VencedorId = new SelectList(db.Exercitos, "Id", "Id");
            return View();
        }

        // POST: BatalhasMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TabuleiroId,ExercitoBrancoId,ExercitoPretoId,VencedorId,TurnoId,Estado")] Batalha batalha)
        {
            if (ModelState.IsValid)
            {
                db.Batalhas.Add(batalha);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExercitoBrancoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoBrancoId);
            ViewBag.ExercitoPretoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoPretoId);
            ViewBag.TabuleiroId = new SelectList(db.Tabuleiroes, "Id", "Id", batalha.TabuleiroId);
            ViewBag.TurnoId = new SelectList(db.Exercitos, "Id", "Id", batalha.TurnoId);
            ViewBag.VencedorId = new SelectList(db.Exercitos, "Id", "Id", batalha.VencedorId);
            return View(batalha);
        }

        // GET: BatalhasMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batalha batalha = db.Batalhas.Find(id);
            if (batalha == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExercitoBrancoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoBrancoId);
            ViewBag.ExercitoPretoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoPretoId);
            ViewBag.TabuleiroId = new SelectList(db.Tabuleiroes, "Id", "Id", batalha.TabuleiroId);
            ViewBag.TurnoId = new SelectList(db.Exercitos, "Id", "Id", batalha.TurnoId);
            ViewBag.VencedorId = new SelectList(db.Exercitos, "Id", "Id", batalha.VencedorId);
            return View(batalha);
        }

        // POST: BatalhasMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TabuleiroId,ExercitoBrancoId,ExercitoPretoId,VencedorId,TurnoId,Estado")] Batalha batalha)
        {
            if (ModelState.IsValid)
            {
                db.Entry(batalha).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExercitoBrancoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoBrancoId);
            ViewBag.ExercitoPretoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoPretoId);
            ViewBag.TabuleiroId = new SelectList(db.Tabuleiroes, "Id", "Id", batalha.TabuleiroId);
            ViewBag.TurnoId = new SelectList(db.Exercitos, "Id", "Id", batalha.TurnoId);
            ViewBag.VencedorId = new SelectList(db.Exercitos, "Id", "Id", batalha.VencedorId);
            return View(batalha);
        }

        // GET: BatalhasMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batalha batalha = db.Batalhas.Find(id);
            if (batalha == null)
            {
                return HttpNotFound();
            }
            return View(batalha);
        }

        // POST: BatalhasMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Batalha batalha = db.Batalhas.Find(id);
            db.Batalhas.Remove(batalha);
            db.SaveChanges();
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
