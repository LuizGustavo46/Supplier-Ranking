
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
        activeformInputs;

/********************* *********************  COMPORTAMENTO DOS ELEMENTOS ********************* *********************/

    /*** Botao para visualizar a Senha ***/
    btnVisualizarSenha.on('click', function (e) {
        e.preventDefault();
        if (inputSenhas.hasClass('show-pass')) {
            inputSenhas.removeClass('show-pass').attr('type', 'password');
            return;
        }

        inputSenhas.addClass('show-pass').attr('type', 'text');
    });

    /*** Botao para mudar a proxima etapa de Cadastro ***/
    btnProximo.on('click', function () {
        //e.preventDefault();
        //verificaSenhasIguais();
        mostraConteudo2Etapa(true);
    });

    /*** Botao para voltar para a etapa anterior de Cadastro ***/
    btnVoltar.on('click', function () {
        mostraConteudo2Etapa(false);
    });

    /*** Botao para cadastrar ***/
    btnCadastrar.on('click', function () {
        //e.preventDefault();
        verificaCheckBox();
    });

    $('.input-senha').on('input', verificaSenhasIguais);

/********************* *********************  FUNÇÕES ********************* *********************/

    /** Verifica todos os inputs visíveis **/
    function verificaInputsVisiveis() {
        return $('.cadastro-form .wrap-input').not('.hide').find('input');
    }

    /** Habilita/desabilita o Botão Entrar conforme o valor dos campos inputs **/
    function verificarInputsVazios() {
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

    /** Verifica senhas iguais **/
    //function verificaSenhasIguais() {
    //    var inputsSenha = $('.input-senha');

    //    inputSenhas.each(function(index) {
    //        inputSenhas[index] = inputSenhas.val();
    //        console.log(inputSenhas[index]);
    //    });


    //    //return $('.cadastro-form .wrap-input').not('.hide').find('input');
    //}

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
        activeformInputs.on('input', verificarInputsVazios);
    }

    init();
});