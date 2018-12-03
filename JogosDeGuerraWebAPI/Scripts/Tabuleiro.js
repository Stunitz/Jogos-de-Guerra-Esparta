

var ObterBatalha;
var CriarNovaBatalha;
var IniciarBatalha;
var MontarTabuleiro;


$(function () {

    var baseUrl = window.location.protocol + "//" +
        window.location.hostname + (window.location.port ? ':' + window.location.port : '');

    var casa_selecionada = null;
    var batalha = null;
    var peca_selecionadaId = null;
    var pecasNoTabuleiro = null;
    var pecaSelecionadaObj = null;
    var pecaElem = null;
    var elementos = null;

    //1 CriarNovaBatalha, 2 RetomarBatalha    
    var token = sessionStorage.getItem("accessToken");

    ObterBatalha = function (BatalhaId) {

        var urlObterBatalha = baseUrl + "/api/Batalhas/" + BatalhaId;
        var headers = {};

        if (token) {
            headers.Authorization = token;
        }

        $.ajax({
            type: 'GET',
            url: urlObterBatalha,
            headers: headers
        })
            .done(function (data) {
                VerificarBatalha(data);
            })
            .fail(function (jqXHR, textStatus) {
                alert("Código de Erro: " + jqXHR.status + "\n\n" + jqXHR.responseText);
            });
    }

    CriarNovaBatalha = function (NacaoId) {
        var urlCriarNovaBatalha = baseUrl + "/api/Batalhas/CriarNovaBatalha?Nacao=" + NacaoId;
        var headers = {};

        if (token) {
            headers.Authorization = token;
        }

        $.ajax({
            type: 'GET',
            url: urlCriarNovaBatalha,
            headers: headers
        })
            .done(function (data) {
                window.location.reload();
            })
            .fail(function (jqXHR, textStatus) {
                alert("Código de Erro: " + jqXHR.status + "\n\n" + jqXHR.responseText);
            });
    }

    IniciarBatalha = function (BatalhaId) {

        var urlIniciarBatalha = baseUrl + "/api/Batalhas/Iniciar?Id=" + BatalhaId;
        var headers = {};

        if (token) {
            headers.Authorization = token;
        }

        $.ajax({
            type: 'GET',
            url: urlIniciarBatalha,
            headers: headers
        })
            .done(function (data) {
                VerificarBatalha(data);
            })
            .fail(function (data) {
                alert("Código de Erro: " + jqXHR.status + "\n\n" + jqXHR.responseText);
            });
    }

    MontarTabuleiro = function (batalhaParam) {

        pecasNoTabuleiro = [];
        var batalha = batalhaParam;
        var pecas = batalha.Tabuleiro.ElementosDoExercito
        var ExercitoBrancoId = batalha.ExercitoBrancoId;
        var ExercitoPretoId = batalha.ExercitoPretoId;
        var altura, largura;
        
        $('#tabuleiro').empty();

        for (altura = 0; altura < batalha.Tabuleiro.Altura; altura++) {

            $("#tabuleiro").append("<div id='linha_" + altura.toString() + "' class='linha' >");
            pecasNoTabuleiro[altura] = [];

            for (largura = 0; largura < batalha.Tabuleiro.Largura; largura++) {

                var nome_casa = "casa_" + altura.toString() + "_" + largura.toString();
                var classe = (altura % 2 == 0 ? (largura % 2 == 0 ? "casa_branca" : "casa_preta") : (largura % 2 != 0 ? "casa_branca" : "casa_preta"));
                $("#linha_" + altura.toString()).append("<div id='" + nome_casa + "' class='casa " + classe + "' />");

                for (x = 0; x < pecas.length; x++) {

                    if (pecas[x].Saude <= 0) {
                        continue;
                    }

                    if (pecas[x].Posicao.Largura == largura && pecas[x].Posicao.Altura == altura) {

                        pecasNoTabuleiro[altura][largura] = pecas[x];
                        
                        img = pecas[x].UriImagem;
                        
                        $("#" + nome_casa).append("<img src='" + img + "' class='peca' id='" +
                            nome_casa.replace("casa", pecas[x].ExercitoId == ExercitoBrancoId ? "peca_branca" : "peca_preta") + "'/>");
                    }
                }
            }
        }
        $(".casa").click(function () {

            //Retirando a seleção da casa antiga.
            $("#" + casa_selecionada).removeClass("casa_selecionada");

            //Obtendo o Id.
            casa_selecionada = $(this).attr("id");

            //Adicionando Vermelho na Casa nova.
            $("#" + casa_selecionada).addClass("casa_selecionada");

            //Legenda que mostra informações da casa selecionada.
            $("#info_casa_selecionada").text(casa_selecionada);

            var altura = casa_selecionada.split("_")[1]
            var largura = casa_selecionada.split("_")[2]

            if (pecaElem == null) {
                //Obter o id da imagem selecionada.
                peca_selecionadaId = ObterPecaIdNaCasa(casa_selecionada);

                //Se for nulo
                if (peca_selecionadaId == null) {
                    pecaElem = null;
                    peca_selecionadaId = "NENHUMA PECA SELECIONADA";
                } else {
                    //Guardar a peça selecionada.
                    pecaElem = document.getElementById(peca_selecionadaId);
                    pecaSelecionadaObj = pecasNoTabuleiro[altura][largura];
                }
                //Legenda que mostra informações da peça selecionada.
                $("#info_peca_selecionada").text(peca_selecionadaId.toString());

                return;
            }

            var posicaoPeca = {
                Altura: altura,
                Largura: largura
            };

            var ExercitoTurno = (batalha.TurnoId == batalha.ExercitoBrancoId) ? batalha.ExercitoBranco : batalha.ExercitoPreto;

            if (ObterPecaIdNaCasa(casa_selecionada) == null)
                ataque = false;
            else
                ataque = true;

            var movimento = {
                Posicao: posicaoPeca,
                AutorId: ExercitoTurno.UsuarioId,
                BatalhaId: batalha.Id,
                ElementoId: pecaSelecionadaObj.Id,
                TipoMovimento: ataque ? "1" : "0"
            };

            var EmailUsuario = sessionStorage.getItem("emailUsuario");

            if (ExercitoTurno.Usuario.Email == EmailUsuario && ExercitoTurno.Id == pecaSelecionadaObj.ExercitoId) {
                Mover(movimento, pecaElem.parentNode, document.getElementById(casa_selecionada), pecaElem);
            }
            else if (ExercitoTurno.Usuario.Email != EmailUsuario) {
                alert("Não é sua vez!");
            }
            else if (ExercitoTurno.Id != pecaSelecionadaObj.ExercitoId) {
                alert("Não é o seu exercito!");
            }

            pecaElem = null;
            $("#" + casa_selecionada).removeClass("casa_selecionada");

        });


        function ObterPecaIdNaCasa(casa_selecionada) {
            return $("#" + casa_selecionada).children("img:first").attr("id");
        }

        function Mover(movimento, posAntiga, posNova, peca) {

            var token = sessionStorage.getItem("accessToken");
            var headers = {};

            if (token) {
                headers.Authorization = token;
            }

            $.ajax({
                type: 'POST',
                url: baseUrl + "/api/Batalhas/Jogar",
                headers: headers,
                data: movimento
            })
                .done(function (data) {
                    MontarTabuleiro(data);
                                        /*
                    if (movimento.TipoMovimento == "Mover") {
                        MoverPeca(posAntiga, posNova, peca)
                    } else {
                        window.reload();
                    }
                    */
                })
                .fail(function (jqXHR, textStatus) {
                    alert("Código de Erro: " + jqXHR.status + "\n\n" + jqXHR.responseText);
                });
        }

        function MoverPeca(posAntiga, posNova, peca) {
            //            var casaElem = document.getElementById(casa_selecionada);
            //Remover a peça da casa antiga.
            posAntiga.removeChild(peca);

            //Colocar a peça na nova casa.
            posNova.appendChild(peca);

            //pecaElem = null para não mover a peça no novo clique.
            posNova.classList.remove("casa_selecionada")
        }

    }  

    function VerificarBatalha(Batalha) {

        if (Batalha.Estado != 0) {
            MontarTabuleiro(Batalha);
            if (Batalha.Estado == 10 || Batalha.Estado == 99) {
                // TODO: Ainda nao foi implementado
            }
            return;
        }

        // cond1 = A batalha esta faltando algum jogador?
        var cond1 = (Batalha.ExercitoPretoId == null || Batalha.ExercitoBrancoId == null);

        // cond2 = O usuario da sessao que ja esta na batalha tentou iniciar esta batalha?
        var cond2 = ((Batalha.ExercitoPreto != null && Batalha.ExercitoPreto.Usuario.Email == sessionStorage.getItem("EmailUsuario")) ||
            (Batalha.ExercitoBranco != null && Batalha.ExercitoBranco.Usuario.Email == sessionStorage.getItem("EmailUsuario")))

        // Se sim para as duas, informe que ele precisa esperar
        if (cond1 && cond2) {
            alert("Espere. Ainda não existe jogador disponível");
            return;
        } 

        // Inicie a batalha
        IniciarBatalha(Batalha.Id);

    }

    function verificarSejogadorestaNaBatalha(batalha) {

        if (batalha) {

            if (batalha.ExercitoBranco) {
                if (batalha.ExercitoBranco.Usuario.Email == sessionStorage.getItem("EmailUsuario")) {
                    return true;
                }
            }
            if (batalha.ExercitoPreto) {
                if (batalha.ExercitoPreto.Usuario.Email == sessionStorage.getItem("EmailUsuario")) {
                    return true;
                }
            }
        }
        return false;
    }

    function ObterbatalhaId(idBatalha) {
        $.ajax({
            type: 'GET',
            url: urlIniciarBatalha,
            headers: headers
        }
        ).done(function (data) {
            if (!verificarSejogadorestaNaBatalha(data)) {
                if (BatalhaTemDoisJogadores(data)) {
                    VisualizarBatalha(data);
                } else {
                    PerguntarUsuario(data);
                }
            } else {
                if (BatalhaTemDoisJogadores(data)) {
                    Jogar(data);
                } else {
                    AvisarJogador();
                }
            }
        }
        ).fail(
            function (jqXHR, textStatus) {
                alert("Código de Erro: " + jqXHR.status + "\n\n" + jqXHR.responseText);
            });   
    }

    function BatalhaTemDoisJogadores(batalha) {
        return batalha.ExercitoBranco != null && batalha.ExercitoPreto != null;
    }

    function PerguntarUsuario(batalha) {

    }

    function realizaRequisicao(url) {
        var token = sessionStorage.getItem("accessToken");
        var headers = {};

        if (token) {
            headers.Authorization = token;
        }
        $.ajax({
            type: 'GET',
            url: url,
            headers: headers
        }
        ).done(function (data) {
            return data;
        }
        ).fail(
            function (jqXHR, textStatus) {
                alert("Código de Erro: " + jqXHR.status + "\n\n" + jqXHR.responseText);
                return null;
            });
    }


    
});
