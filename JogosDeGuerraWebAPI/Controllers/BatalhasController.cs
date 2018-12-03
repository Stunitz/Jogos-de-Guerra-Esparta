using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using JogosDeGuerraModel;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace JogosDeGuerraWebAPI.Controllers
{
    /// <summary>
    /// WebAPI controladora dos dados das batalhas
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Batalhas")]
    public class BatalhasController : ApiController
    {
        #region Private Fields

        /// <summary>
        /// Referencia do contexto do EntityFramework com o banco de dados
        /// </summary>
        private ModelJogosDeGuerra db = new ModelJogosDeGuerra();

        #endregion

        #region Get's

        // GET: api/Batalhas
        /// <summary>
        /// Procura todas as batalhas finalizadas ou nao finalizadas no banco de dados
        /// </summary>
        /// <param name="Finalizada">Valor que define se eh para retornar apenas as batalhas finalizadas</param>
        /// <returns>Um enumerador com as batalhas encontradas</returns>
        public IEnumerable<Batalha> Get(bool Finalizada = true)
        {
            IEnumerable<Batalha> batalhas;
            if (Finalizada)
            {
                batalhas = db.Batalhas.Where(b => b.Vencedor != null).ToList();
            }
            else
            {
                batalhas = db.Batalhas.ToList();
            }
            return batalhas;
        }

        // GET: api/Batalhas/5
        /// <summary>
        /// Busca uma batalha no banco de dados a partir de um identificador
        /// </summary>
        /// <param name="id">O identificador</param>
        /// <returns>A batalha encontrada</returns>
        [HttpGet]
        public Batalha Get(int id)
        {           
            // return ctx.Batalhas.Find(id);
            var batalha = db.Batalhas.Include(b => b.ExercitoPreto)
                .Include(b => b.ExercitoPreto.Usuario)
                .Include(b => b.ExercitoBranco)
                .Include(b => b.ExercitoBranco.Usuario)
                .Include(b => b.Tabuleiro)
                .Include(b => b.Tabuleiro.ElementosDoExercito)
                .Include(b => b.Turno)
                .Include(b => b.Turno.Usuario).Where(b => b.Id == id).FirstOrDefault();

            return batalha;
        }

        /// <summary>
        /// Inicia uma batalha que contenha dois jogares
        /// </summary>
        /// <param name="id">O id do jogador que iniciou a batalha</param>
        /// <returns>Os dados da batalha iniciada</returns>
        [Route("Iniciar")]
        [HttpGet]
        public Batalha IniciarBatalha(int id)
        {
            var usuario = Utils.Utils.ObterUsuarioLogado(db);

            var batalha = db.Batalhas
                .Include(b => b.ExercitoPreto)
                .Include(b => b.ExercitoBranco)
                .Include(b => b.Tabuleiro)
                .Include(b => b.Turno)
                .Include(b => b.Turno.Usuario)
                .Where(b =>
                    (b.ExercitoBranco.Usuario.Email == usuario.Email
                    || b.ExercitoPreto.Usuario.Email == usuario.Email)
                    && (b.ExercitoBranco != null && b.ExercitoPreto != null)
                    && b.Id == id).FirstOrDefault();

            if (batalha == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(String.Format("Não foi possível carregar a Batalha.")),
                    ReasonPhrase = "Não foi possível carregar a batalha."
                };
                throw new HttpResponseException(resp);
            }

            if (batalha.Tabuleiro == null)
            {
                batalha.Tabuleiro = new Tabuleiro
                {
                    Altura = 8,
                    Largura = 8
                };
            }


            if (batalha.Estado == Batalha.EstadoBatalhaEnum.NaoIniciado)
            {
                batalha.Tabuleiro.IniciarJogo(batalha.ExercitoBranco, batalha.ExercitoPreto);

                Random r = new Random();
                batalha.Turno = r.Next(100) < 50 ?
                    batalha.ExercitoPreto : batalha.ExercitoBranco;

                batalha.Estado = Batalha.EstadoBatalhaEnum.Iniciado;
            }

            db.SaveChanges();

            return batalha;
        }

        /// <summary>
        /// Cria uma nova batalha ou adiciona o jogador em um batalha sem dois jogadores
        /// </summary>
        /// <param name="Nacao">A nacao selecionada para o jogador</param>
        /// <returns>A batalha criada ou uma batalha atualizada</returns>
        [Route("CriarNovaBatalha")]
        [HttpGet]
        public Batalha CriarNovaBatalha(AbstractFactoryExercito.Nacao Nacao)
        {

            //Obter usuário LOgado
            var usuarioLogado = Utils.Utils.ObterUsuarioLogado(db);

            //Verificar se existe uma batalha cujo exercito branco esteja definido
            //E exercito Preto esteja em branco
            var batalha = db.Batalhas.Include(x => x.ExercitoBranco.Usuario)
                .Where(b => b.ExercitoPreto == null && b.ExercitoBranco != null && b.ExercitoBranco.Usuario.Email != usuarioLogado.Email)
                .FirstOrDefault();

            if (batalha == null)
            {
                batalha = new Batalha();

                db.Batalhas.AddOrUpdate(batalha);
                db.SaveChanges();
            }

            batalha.CriarBatalha(Nacao, usuarioLogado);
            db.Batalhas.AddOrUpdate(batalha);

            //Não iria conseguir os Ids Corretos;
            //ctx.SaveChangesAsync();

            db.SaveChanges();
            return batalha;
        }

        /// <summary>
        /// Verifica se o usuario logado esta em alguma batalha
        /// </summary>
        /// <param name="id">O identificador do usuario logado</param>
        /// <returns>O resultado da verificacao</returns>
        [Route("VerificaUsuarioEmBatalha")]
        [HttpGet]
        public Boolean JogadorLogadoParticipaBatalha(int id)
        {
            var usuario = Utils.Utils.ObterUsuarioLogado(db);
            var batalha = this.db.Batalhas.Find(id);

            if(batalha.ExercitoBranco != null && batalha.ExercitoPreto != null &&
                (batalha.ExercitoBranco.UsuarioId == usuario.Id || batalha.ExercitoPreto.UsuarioId == usuario.Id))
                return true;

            else
                return false;
            
        }
        

        #endregion

        #region Post's


        /// <summary>
        /// Realiza a jogada de um jogador e altera o turno do jogo
        /// </summary>
        /// <param name="movimento">O movimento realizado na jogada</param>
        /// <returns>A nova versao da batalha com o movimento realizado</returns>
        [Route("Jogar")]
        [HttpPost]
        public Batalha Jogar(Movimento movimento)
        {
            movimento.Elemento = db.ElementosDoExercitos.Find(movimento.ElementoId);

            if (movimento.Elemento == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(String.Format("O Elemento não existe.")),
                    ReasonPhrase = "O elemento informado para movimento não existe."
                };
                throw new HttpResponseException(resp);
            }

            movimento.Batalha = 
                db.Batalhas.Find(movimento.BatalhaId);
            var usuario = Utils.Utils.ObterUsuarioLogado(db);

            if (usuario.Id != movimento.AutorId)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(
                        String
                        .Format(
                            "O usuário tentou executar uma ação como se fosse outro usuário.")),
                    ReasonPhrase =
                    "Você não tem permissão para executar esta ação."
                };
                throw new HttpResponseException(resp);
            }

            Batalha batalha = Get(movimento.BatalhaId);

            if (movimento.AutorId != movimento.Elemento.Exercito.UsuarioId)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(String.Format("A peça não pertence ao usuário.")),
                    ReasonPhrase = "Não foi possível executar o movimento."
                };
                throw new HttpResponseException(resp);
            }

            if (movimento.AutorId != batalha.Turno.UsuarioId)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(
                        String
                        .Format("O turno atual é do adversário.")),
                    ReasonPhrase = "Você não tem permissão para executar esta ação."
                };
                throw new HttpResponseException(resp);
            }

            if (!batalha.JogarMovimento(movimento))
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(String.Format("Não foi possível executar o movimento.")),
                    ReasonPhrase = "Não foi possível executar o movimento."
                };
                throw new HttpResponseException(resp);
            }


           // batalha.Tabuleiro = movimento.Batalha.Tabuleiro;
            batalha.Turno = null;
            batalha.TurnoId = batalha.TurnoId == batalha.ExercitoBrancoId ?
                batalha.ExercitoPretoId : batalha.ExercitoBrancoId;

            if (movimento.TipoMovimento == Movimento.EnumTipoMovimento.Atacar)
            {
                var oponente = movimento.Batalha.Tabuleiro.ObterElemento(movimento.Posicao);
                var oponenteDb = db.ElementosDoExercitos.Find(oponente.Id);

                oponenteDb.Saude -= movimento.Elemento.Ataque;

                if (oponenteDb.Saude < 0) oponenteDb.Saude = 0;

                db.Entry(oponenteDb).State = EntityState.Modified;
            }


            db.SaveChanges();

            return batalha;
        }



        // POST: api/Batalhas
        public void Post([FromBody]Batalha value)
        {

        }


        #endregion

        #region Put's


        // PUT: api/Batalhas/5
        public void Put(int id, [FromBody]Batalha value)
        {
        }

        #endregion

        #region Delete's

        // DELETE: api/Batalhas/5
        public void Delete(int id)
        {
        }

        #endregion

        #region Helpers Methods
        

        public Boolean VerificaDoisJogadores(int idBatalha)
        {
            var batalha = this.db.Batalhas.Find(idBatalha);
            return batalha.ExercitoBranco != null && batalha.ExercitoPreto != null;
        }


        #endregion
    }
}
