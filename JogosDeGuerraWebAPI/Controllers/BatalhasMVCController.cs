using System.Net;
using System.Linq;
using System.Web.Mvc;
using JogosDeGuerraModel;
using System.Data.Entity;

namespace JogosDeGuerraWebAPI.Controllers
{
    /// <summary>
    /// MVC controlador dos dados das batalhas
    /// </summary>
    public class BatalhasMVCController : Controller
    {
        #region Private Fields

        /// <summary>
        /// Referencia do contexto do EntityFramework com o banco de dados
        /// </summary>
        private ModelJogosDeGuerra db = new ModelJogosDeGuerra();

        #endregion

        #region Get's


        // GET: BatalhasMVC
        public ActionResult Index()
        {
            var batalhas = db.Batalhas
                .Include(b => b.ExercitoBranco)
                .Include(b => b.ExercitoPreto)
                .Include(b => b.ExercitoBranco.Usuario)
                .Include(b => b.ExercitoPreto.Usuario)
                .Include(b => b.Tabuleiro)
                .Include(b => b.Turno)
                .Include(b => b.Vencedor);
            return View(batalhas.ToList());
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


        [Route("Tabuleiro/{id}")]
        public ActionResult Tabuleiro(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            Batalha batalha = db.Batalhas.Find(id);
            if (batalha == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = batalha.Id;
            return View(batalha);
        }


        #endregion

        #region Create's


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
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
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


        #endregion

        #region Edit's


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
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
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


        #endregion

        #region Delete's


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


        #endregion

        #region Helpers

        /// <summary>
        /// Limpa a referencia do banco de dados
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}