
$(document).ready(function () {
    'use strict';

    var btnComprador = $('.wrap-restaurar-senha-comprador #btnComprador'),
        btnFisica = $('.wrap-restaurar-senha-comprador #btnFisica'),
        btnJuridica = $('.wrap-restaurar-senha-comprador #btnJuridica'),
        btnEnviarEmail = $('.wrap-restaurar-senha-comprador #btnEnviarEmail'),

        switchFornecedor = $('.wrap-restaurar-senha-comprador #switchFornecedor'),
        switchJuridica = $('.wrap-restaurar-senha-comprador #switchJuridica'),

        sliderFornecedorComprador = $('.wrap-restaurar-senha-comprador .slider-fornecedor'),
        sliderFisicaJuridica = $('.wrap-restaurar-senha-comprador .slider-juridica'),

        wrapInputCpf = $('.wrap-restaurar-senha-comprador #inputCpf').closest('.restaurar-senha-comprador-form  .wrap-input'),
        wrapInputCnpj = $('.wrap-restaurar-senha-comprador #inputCnpj').closest('.restaurar-senha-comprador-form  .wrap-input'),
        wrapInputEmail = $('.wrap-restaurar-senha-comprador #inputEmail'),

        inputHidden = $('.wrap-restaurar-senha-comprador #inputHidden'),
        formRestaurarSenha = $('.wrap-restaurar-senha-comprador .restaurar-senha-comprador-form'),
        formInputs = $('.wrap-restaurar-senha-comprador .restaurar-senha-comprador-form  input'),
        activeformInputs;


/********************* *********************  COMPORTAMENTO DOS ELEMENTOS ********************* *********************/

    /** Botão para selecionar o Comprador **/
    btnComprador.on('click', function () {
        var switchFornecedorFalse = switchFornecedor.prop('checked', false);

        uncheckedSlider(sliderFornecedorComprador);
        switchCompradorFornecedor(switchFornecedorFalse);
    });

    /** Botão para selecionar Pessoa Física **/
    btnFisica.on('click', function () {
        var switchJuriciaFalse = switchJuridica.prop('checked', false);

        uncheckedSlider(sliderFisicaJuridica);
        switchTipoDePessoa(switchJuriciaFalse);
    });

    /** Botão para selecionar Pessoa Jurídica **/
    btnJuridica.on('click', function () {
        var switchJuriciaTrue = switchJuridica.prop('checked', true);

        uncheckedSlider(sliderFisicaJuridica);
        switchTipoDePessoa(switchJuriciaTrue);
    });

    /** Switch slider para selecionar Pessoa Física ou Pessoa Jurídica **/
    switchJuridica.change(function () {
        uncheckedSlider(sliderFisicaJuridica);
        switchTipoDePessoa(switchJuridica);
    });

    btnEnviarEmail.on('click', function () {
        formRestaurarSenha.submit();
    });

/********************* *********************  FUNÇÕES ********************* *********************/

    /** Tira o slider da posição neutra **/
    function uncheckedSlider(slider) {
        slider.removeClass('unchecked');
    }

    /** Reseta o slider e os botões **/
    function uncheckSliderAndButtons(slider) {
        if (slider === switchJuridica) {
            switchJuridica.addClass('unchecked').prop('checked', false);
            sliderFisicaJuridica.addClass('unchecked');
            btnFisica.removeClass('active-switch');
            btnJuridica.removeClass('active-switch');
            return;
        }

        switchFuncionario.prop('checked', false);
        btnFuncionario.removeClass('active-switch');
    }

    /** Habilita o Botão Comprador e mostra os Botão Pessoa Física e Jurídica **/
    function mostrarComprador(showComprador) {
        if (showComprador) {
            btnComprador.addClass('active-switch');
            $('#wrapperFisicaJuridica').removeClass('hide');
            return;
        }

        btnComprador.removeClass('active-switch');
        $('#wrapperFisicaJuridica').addClass('hide');
    }

    /** Habilita o Botão Pessoa Física e mostra os campos **/
    function mostrarPessoaFisica(showPessoaFisica) {
        if (showPessoaFisica) {
            btnFisica.addClass('active-switch');
            wrapInputCpf.removeClass('hide');
            wrapInputEmail.removeClass('hide');

            inputHidden.val('0');
            return;
        }

        btnFisica.removeClass('active-switch');
        wrapInputCpf.addClass('hide');
    }

    /** Habilita o Botão Pessoa Jurídica e mostra os campos **/
    function mostrarPessoaJuridica(showPessoaJuridica) {
        if (showPessoaJuridica) {
            btnJuridica.addClass('active-switch');
            wrapInputCnpj.removeClass('hide');
            wrapInputEmail.removeClass('hide');

            inputHidden.val('1');
            return;
        }

        btnJuridica.removeClass('active-switch');
        wrapInputCnpj.addClass('hide');
    }

    /** Troca entre Pessoa Física e Pessoa Jurídica **/
    function switchTipoDePessoa(input) {

        if ($(input).is(':checked')) { //Seleciona pessoa Jurídica
            mostrarPessoaFisica(false);
            mostrarPessoaJuridica(true);

        } else { //Seleciona pessoa Física
            mostrarPessoaJuridica(false);
            mostrarPessoaFisica(true);
        }

        $('.alert-msg').remove();
        formInputs.val('');
        activeformInputs = verificaInputsVisiveis();
        activeformInputs.on('input', verificarInputsVazios);
        $('.restaurar-senha-comprador-form').removeClass('hide');
    }

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
            
            if (isEmpty) { // Habilita/desabilita o Botão Entrar
                btnEnviarEmail.removeAttr('disabled').removeClass('disabled');
            } else {
                btnEnviarEmail.attr('disabled', 'disabled').addClass('disabled');
            }
        });
    }

    /** Inicia assim que a pagina e carregada **/
    function init() {
        wrapInputCpf.addClass('hide');
        wrapInputCnpj.addClass('hide');
    }

    init();
});
