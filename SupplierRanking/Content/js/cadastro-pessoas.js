
$(document).ready(function () {
    'use strict';

    var btnVisualizarSenha = $('#btnSenha'),
        btnProximo = $('#btnProximo'),
        btnVoltar = $('#btnVoltar'),
        btnCadastrar = $('#btnCadastrar'),

        inputSenhas = $('.input-senha'),

        divOptions = $('.wrap-options'),
        divFirstPart = $('.form-first-part'),
        divSecondPart = $('.form-second-part'),
        divThridPart = $('.form-third-part'),

        formCadastro = $('.cadastro-form'),
        formTitle = $('.form-title h2'),
        formInputs = $('.wrap-cadastro .cadastro-form input'),
        activeformInputs,
        activeformChechbox;


/********************* *********************  COMPORTAMENTO DOS ELEMENTOS ********************* *********************/

    /*** Botao para visualizar a Senha ***/
    btnVisualizarSenha.on('click', function(e) {
        visualizarSenhas(e);
    });

    /*** Botao para mudar a proxima etapa de Cadastro ***/
    btnProximo.on('click', function () {
        mostraConteudo2Etapa(true);
    });

    /*** Botao para voltar para a etapa anterior de Cadastro ***/
    btnVoltar.on('click', function () {
        mostraConteudo2Etapa(false);
    });

    /*** Botao para cadastrar ***/
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
        return $('.cadastro-form .wrap-input').not('.hide').find('input');
    }

    /** Verifica todos os checkbox visíveis **/
    function verificaCheckBoxVisiveis() {
        return $('.cadastro-form .checkbox-container input[type=checkbox]');
    }

    /** Habilita/desabilita o Botão Cadastro conforme qunado pelo menos 1 item estiver selecionado **/
    function verificaCheckBox() {
        var isEmpty = true;

        activeformChechbox.each(function () { // percorre todos os checkbox 

            if ($(this).is(':checked')) { // se houver pelo menos um checkbox selecionado, entra no if
                isEmpty = false;
                return false; // para o loop, evitando que mais inputs sejam verificados sem necessidade
            }
        });

        if (isEmpty) { // Habilita/desabilita o Botão Entrar
            btnCadastrar.attr('disabled', 'disabled').addClass('disabled');
        } else {
            btnCadastrar.removeAttr('disabled').removeClass('disabled');
        }
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
            btnProximo.attr('disabled', 'disabled').addClass('disabled');
        } else {
            btnProximo.removeAttr('disabled').removeClass('disabled');
        }
    }

    /** Muda o conteúdo entre o primeiro passo de Cadastro e o segundo **/
    function mostraConteudo2Etapa(showThirdPart) {
        if(showThirdPart) {
            divOptions.addClass('hide');
            divFirstPart.addClass('hide');
            divSecondPart.addClass('hide');
            divThridPart.removeClass('hide');
            formCadastro.removeClass('mt-4 pt-4 pr-2 pl-0');
            formTitle.text('O que você procura?');
            return;
        }

        divThridPart.addClass('hide');
        divOptions.removeClass('hide');
        divFirstPart.removeClass('hide');
        divSecondPart.removeClass('hide');
    }

    /** Inicia assim que a pagina e carregada **/
    function init() {
        divThridPart.addClass('hide');
        formInputs.val('');

        activeformInputs = verificaInputsVisiveis();
        activeformInputs.on('input', verificaInputsVazios);

        activeformChechbox = verificaCheckBoxVisiveis();
        activeformChechbox.on('input', verificaCheckBox);
    }

    init();
});