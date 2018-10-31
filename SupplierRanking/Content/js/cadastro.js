
$(document).ready(function () {
    'use strict';

    var btnComprador = $('.cadastro-login #btnComprador'),
        btnFornecedor = $('.cadastro-login #btnFornecedor'),
        btnFisica = $('.cadastro-login #btnFisica'),
        btnJuridica = $('.cadastro-login #btnJuridica'),
        btnFuncionario = $('.cadastro-login #btnFuncionario'),

        switchFornecedor = $('.cadastro-login #switchFornecedor'),
        switchJuridica = $('.cadastro-login #switchJuridica'),
        switchFuncionario = $('.cadastro-login #switchFuncionario'),

        sliderFornecedorComprador = $('.cadastro-login .slider-fornecedor'),
        sliderFisicaJuridica = $('.cadastro-login .slider-juridica'),
        sliderFuncionario = $('.cadastro-login .slider-funcionario'),

        inputCpf = $('.cadastro-login #inputCpf'),
        inputCnpj = $('.cadastro-login #inputCnpj'),
        inputFuncionario = $('.cadastro-login #inputFuncionario'),
        inputHidden = $('.cadastro-login #inputHidden');


    /*** Botao para selecionar o Comprador ***/
    btnComprador.on('click', function () {
        var switchFornecedorFalse = switchFornecedor.prop('checked', false);

        uncheckedSlider(sliderFornecedorComprador);
        switchCompradorFornecedor(switchFornecedorFalse);
    });

    /*** Botao para selecionar o Fornecedor ***/
    btnFornecedor.on('click', function () {
        var switchFornecedorTrue = switchFornecedor.prop('checked', true);

        uncheckedSlider(sliderFornecedorComprador);
        switchCompradorFornecedor(switchFornecedorTrue);
    });

    /*** Switch slider para selecionar Comprador ou Fornecedor ***/
    switchFornecedor.change(function () {
        uncheckedSlider(sliderFornecedorComprador);
        switchCompradorFornecedor(switchFornecedor);
    });

    /*** Botao para selecionar pessoa Fisica ***/
    btnFisica.on('click', function () {
        var switchJuriciaFalse = switchJuridica.prop('checked', false);

        uncheckedSlider(sliderFisicaJuridica);
        switchTipoDePessoa(switchJuriciaFalse);
    });

    /*** Botao para selecionar pessoa Juridica ***/
    btnJuridica.on('click', function () {
        var switchJuriciaTrue = switchJuridica.prop('checked', true);

        uncheckedSlider(sliderFisicaJuridica);
        switchTipoDePessoa(switchJuriciaTrue);
    });

    /*** Switch slider para selecionar pessoa Fisica ou Juridica ***/
    switchJuridica.change(function () {
        uncheckedSlider(sliderFisicaJuridica);
        switchCompradorFornecedor(switchJuridica);
    });


    /*** Botao para selecionar pessoa Funcionario ***/
    btnFuncionario.on('click', function () {
        var switchFuncionarioTrueOrFalse = switchFuncionario.prop('checked', true);

        switchTipoFuncionario(switchFuncionarioTrueOrFalse);
    });

    /*** Switch slider para selecionar pessoa Fisica ou Juridica ***/
    switchFuncionario.change(function () {
        switchTipoFuncionario(switchFuncionario);
    });

    // Primeira funcao a ser executada
    function init() {
        $('.cadastro-login #wrapperFisicaJuridica').hide();
        $('.cadastro-login #wrapperFuncionario').hide();
        $('.cadastro-login .form-thrid-part').hide();
        inputFuncionario.hide();
    }

    // Funcao para tirar o slider da posicao neutra  
    function uncheckedSlider(slider) {
        slider.removeClass('unchecked');
    }

    // Funcao para resetar o slider e os botoes
    function uncheckSliderAndButtons(slider) {
        if (slider === switchJuridica) {
            switchJuridica.addClass('unchecked').prop('checked', false);
            sliderFisicaJuridica.addClass('unchecked');
            btnFisica.removeClass('active-switch');
            btnJuridica.removeClass('active-switch');

        } else {
            switchFuncionario.prop('checked', false);
            btnFuncionario.removeClass('active-switch');
        }
    }

    // Funcao para aparecer / esconder entre Fornecedor ou Comprador
    function switchCompradorFornecedor(input) {
        $('.cadastro-login .cadastro-form').hide();

        if ($(input).is(':checked')) { //Seleciona o Fornecedor

            btnComprador.removeClass('active-switch');
            $('.cadastro-login #wrapperFisicaJuridica').hide();
            uncheckSliderAndButtons(switchJuridica);

            btnFornecedor.addClass('active-switch');
            $('.cadastro-login #wrapperFuncionario').show();
            $('.cadastro-login .cadastro-form').show();
            inputCpf.hide();
            inputCnpj.mask('00.000.000/0000-00').show();
            inputHidden.val('2');

        } else { //Seleciona o Comprador

            btnFornecedor.removeClass('active-switch');
            uncheckSliderAndButtons(switchFuncionario);
            $('.cadastro-login #wrapperFuncionario').hide();

            btnComprador.addClass('active-switch');
            $('.cadastro-login #wrapperFisicaJuridica').show();
            inputFuncionario.hide();
        }
    }

    // Funcao para aparecer / esconder entre pessoa Fisica ou Juridica
    function switchTipoDePessoa(input) {

        if ($(input).is(':checked')) { //Seleciona pessoa Juridica

            btnFisica.removeClass('active-switch');
            inputCpf.hide();

            btnJuridica.addClass('active-switch');
            inputCnpj.show();
            inputHidden.val('1');

        } else { //Seleciona pessoa Fisica

            btnJuridica.removeClass('active-switch');
            inputCnpj.hide();

            btnFisica.addClass('active-switch');
            inputCpf.show();
            inputHidden.val('0');
        }

        $('.cadastro-login .cadastro-form').show();
    }

    // Funcao para aparecer / esconder Funcionario
    function switchTipoFuncionario(input) {

        if ($(input).is(':checked') && btnFuncionario.hasClass('active-switch')) {
            btnFuncionario.removeClass('active-switch');
            switchFuncionario.prop('checked', false);
            inputFuncionario.hide();
            $('.cadastro-login .wrap-login').removeClass('has-employee');
            inputHidden.val('2');

        } else {
            btnFuncionario.addClass('active-switch');
            switchFuncionario.prop('checked', true);
            $('.cadastro-login .wrap-login').addClass('has-employee');
            inputFuncionario.show();
            inputHidden.val('3');
        }

        $('.cadastro-login .cadastro-form').show();
    }

    init();
});