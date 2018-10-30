
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
        formTitle = $('.form-title h2');


    /*** Botao para visualizar a Senha ***/
    btnVisualizarSenha.on('click', function () {

        if (inputSenhas.hasClass('show-pass')) {
            inputSenhas.removeClass('show-pass').attr('type', 'password');
            return;
        }

        inputSenhas.addClass('show-pass').attr('type', 'text');
    });

    /*** Botao para mudar a proxima etapa de Cadastro ***/
    btnProximo.on('click', function () {
        divOptions.hide();
        divFirstPart.hide();
        divSecondPart.hide();
        divThridPart.show();
        formCadastro.removeClass('mt-4 pt-4 pr-2 pl-0');
        formTitle.text('O que você procura?');
        
    });

    /*** Botao para voltar para a etapa anterior de Cadastro ***/
    btnVoltar.on('click', function () {
        divThridPart.hide();
        divOptions.show();
        divFirstPart.show();
        divSecondPart.show();
    });


    // Primeira funcao a ser executada
    function init() {
        divThridPart.hide();

        
    }


    init();
});