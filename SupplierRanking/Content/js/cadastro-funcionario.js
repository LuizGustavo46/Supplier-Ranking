$(document).ready(function () {
    'use strict';

    var btnVisualizarSenha = $('.wrap-cadastro-funcionario #btnSenha'),
        btnCadastrar = $('.wrap-cadastro-funcionario #btnCadastrar'),

        inputSenhas = $('.wrap-cadastro-funcionario .input-senha'),

        formCadastro = $('.wrap-cadastro-funcionario .cadastro-form'),
        formInputs = $('.wrap-cadastro-fornecedor .cadastro-form input'),
        activeformInputs;


    /********************* *********************  COMPORTAMENTO DOS ELEMENTOS ********************* *********************/

    /*** Botão para visualizar a Senha ***/
    btnVisualizarSenha.on('click', function (e) {
        visualizarSenhas(e);
    });

    /*** Botão para cadastrar ***/
    btnCadastrar.on('click', function () {
        formCadastro.submit();
    });


    /********************* *********************  FUNÇÕES ********************* *********************/

    /** Transforma as senhas em text/pass **/
    function visualizarSenhas(e) {
        e.preventDefault();

        if (inputSenhas.hasClass('show-pass')) {
            inputSenhas.removeClass('show-pass').attr('type', 'password');
            return;
        }

        inputSenhas.addClass('show-pass').attr('type', 'text');
    }

    /** Verifica todos os inputs visíveis **/
    function verificaInputsVisiveis() {
        return formCadastro.not('.hide').find('.required-field');
    }

    /** Habilita/desabilita o Botão Entrar conforme o valor dos campos inputs **/
    function verificaInputsVazios() {
        var isEmpty = false;

        activeformInputs.each(function () { // percorre todos os inputs 

            if ($(this).val() == '') { // se houver pelo menos um campo vazio, entra no if
                isEmpty = true;
                return false; // para o loop, evitando que mais inputs sejam verificados sem necessidade
            }
        });

        if (isEmpty) { // Habilita/desabilita o Botão Entrar
            btnCadastrar.attr('disabled', 'disabled').addClass('disabled');
        } else {
            btnCadastrar.removeAttr('disabled').removeClass('disabled');
        }
    }

    /** Inicia assim que a pagina e carregada **/
    function init() {
        formInputs.val('');

        activeformInputs = verificaInputsVisiveis();
        activeformInputs.on('input', verificaInputsVazios);
    }

    init();
});
