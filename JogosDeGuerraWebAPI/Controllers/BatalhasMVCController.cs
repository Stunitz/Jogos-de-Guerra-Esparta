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