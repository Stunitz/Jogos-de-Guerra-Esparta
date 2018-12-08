using System.Linq;
using System.Web.Mvc;

namespace JogosDeGuerraWebAPI.Controllers
{
    public class HomeController : Controller
    {
        #region Private Field's


        /// <summary>
        /// Referencia do contexto do EntityFramework com o banco de dados
        /// </summary>
        private JogosDeGuerraModel.ModelJogosDeGuerra ctx = new JogosDeGuerraModel.ModelJogosDeGuerra();


        #endregion

        #region ActionResult's


        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            
            bool usuarioAutenticado = 
                Utils.Utils.ObterUsuarioLogado(ctx) != null;

            if (usuarioAutenticado)
                return RedirectToAction("Index", "BatalhasMVC", null);

            return View();
        }

        public ActionResult Tabuleiro(int BatalhaId = -1)
        {
            ViewBag.Title = "Tabuleiro";

            var batalha = ctx.Batalhas.Where(b => b.Id == BatalhaId).FirstOrDefault();

            if (batalha != null)
                return View(batalha);

            return View();
        }

        public ActionResult Login(string usuario, string password, string rememberme, string returnurl)
        {
            return View();
        }
        
        // TODO: Registrar o usuario no model 
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Batalhas()
        {
            return View();
        }


        #endregion
    }
}
