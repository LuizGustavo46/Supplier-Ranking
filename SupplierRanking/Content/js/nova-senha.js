$(document).ready(function () {
    'use strict';

    var btnVisualizarSenhaComprador = $('.wrap-nova-senha-comprador #btnSenha'),
        btnCadastrarNovaSenhaComprador = $('.wrap-nova-senha-comprador #btnCadastrarNovaSenha'),

        btnVisualizarSenhaFornecedor = $('.wrap-nova-senha-fornecedor #btnSenha'),
        btnCadastrarNovaSenhaFornecedor = $('.wrap-nova-senha-fornecedor #btnCadastrarNovaSenha'),

        inputSenhasComprador = $('.wrap-nova-senha-comprador .input-senha'),
        inputSenhasFornecedor = $('.wrap-nova-senha-fornecedor .input-senha'),
            
        formComprador = $('.wrap-nova-senha-comprador .nova-senha-comprador-form'),
        formFornecedor = $('.wrap-nova-senha-fornecedor .nova-senha-fornecedor-form');


/********************* *********************  COMPORTAMENTO DOS ELEMENTOS ********************* *********************/

    /*** Botão para visualizar a senha do Comprador ***/
    btnVisualizarSenhaComprador.on('click', function (e) {
        visualizarSenhas(e, inputSenhasComprador);
    });

    /*** Botão para cadastrar a nova senha do Comprador ***/
    btnCadastrarNovaSenhaComprador.on('click', function () {

        console.log('auqi');
        formComprador.submit();
    });

    /*** Botão para visualizar a senha do Fornecedor ***/
    btnVisualizarSenhaFornecedor.on('click', function (e) {
        visualizarSenhas(e, inputSenhasFornecedor);
    });

    /*** Botão para cadastrar a nova senha do Comprador ***/
    btnCadastrarNovaSenhaFornecedor.on('click', function () {
        console.log('auqi');
        formFornecedor.submit();
    });


/********************* *********************  FUNÇÃO ********************* *********************/

    /** Transforma as senhas em text/pass **/
    function visualizarSenhas(e, inputs) {
        e.preventDefault();

        if (inputs.hasClass('show-pass')) {
            inputs.removeClass('show-pass').attr('type', 'password');
            return;
        }

        inputs.addClass('show-pass').attr('type', 'text');
    }

});