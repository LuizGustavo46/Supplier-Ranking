
$(document).ready(function () {
    'use strict';

    var btnEnviarEmail = $('.wrap-restaurar-senha-fornecedor #btnEnviarEmail'),

        formRestaurarSenha = $('.wrap-restaurar-senha-fornecedor .restaurar-senha-fornecedor-form'),
        activeformInputs;


    /********************* *********************  COMPORTAMENTO DOS ELEMENTOS ********************* *********************/

    btnEnviarEmail.on('click', function () {
        formRestaurarSenha.submit();
    });

    /********************* *********************  FUNÇÕES ********************* *********************/

    /** Verifica todos os inputs visíveis **/
    function verificaInputsVisiveis() {
        return $('.restaurar-senha-comprador-form .wrap-input').not('.hide').find('input');
    }

    /** Habilita/desabilita o Botão Entrar conforme o valor dos campos inputs **/
    function verificarInputsVazios() {
        var isEmpty, prevInput;

        activeformInputs.filter(function () {
            if (!prevInput) { // Verifica se o input anterior já não está mais vazio
                isEmpty = $.trim($(this).val()).length > 0;
                prevInput = isEmpty;
            }

            console.log('prevInput',prevInput, isEmpty)
            if (isEmpty) { // Habilita/desabilita o Botão Entrar
                btnEnviarEmail.removeAttr('disabled').removeClass('disabled');
            } else {
                btnEnviarEmail.attr('disabled', 'disabled').addClass('disabled');
            }
        });
    }

    /** Inicia assim que a pagina e carregada **/
    function init() {
        activeformInputs = verificaInputsVisiveis();
        activeformInputs.on('input', verificarInputsVazios);
    }

    init();
});
