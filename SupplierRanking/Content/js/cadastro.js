
$(document).ready(function () {
    'use strict';

    var btnComprador = $('.cadastro-login #btnComprador'),
        btnFornecedor = $('.cadastro-login #btnFornecedor'),
        btnFisica = $('.cadastro-login #btnFisica'),
        btnJuridica = $('.cadastro-login #btnJuridica'),

        switchFornecedor = $('.cadastro-login #switchFornecedor'),
        switchJuridica = $('.cadastro-login #switchJuridica'),

        sliderFornecedorComprador = $('.cadastro-login .slider-fornecedor'),
        sliderFisicaJuridica = $('.cadastro-login .slider-juridica');


    /********************* *********************  COMPORTAMENTO DOS ELEMENTOS ********************* *********************/

    /** Botão para selecionar o Comprador **/
    btnComprador.on('click', function () {
        var switchFornecedorFalse = switchFornecedor.prop('checked', false);

        uncheckedSlider(sliderFornecedorComprador);
        switchCompradorFornecedor(switchFornecedorFalse);
    });

    /** Botão para selecionar o Fornecedor **/
    btnFornecedor.on('click', function () {
        var switchFornecedorTrue = switchFornecedor.prop('checked', true);

        uncheckedSlider(sliderFornecedorComprador);
        switchCompradorFornecedor(switchFornecedorTrue);
    });

    /** Switch slider para selecionar Comprador ou Fornecedor **/
    switchFornecedor.change(function () {
        uncheckedSlider(sliderFornecedorComprador);
        switchCompradorFornecedor(switchFornecedor);
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

        //switchFuncionario.prop('checked', false);
        //btnFuncionario.removeClass('active-switch');
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
            //wrapInputCpf.removeClass('hide');
            return;
        }

        btnFisica.removeClass('active-switch');
        //wrapInputCpf.addClass('hide');
    }

    /** Habilita o Botão Pessoa Jurídica e mostra os campos **/
    function mostrarPessoaJuridica(showPessoaJuridica) {
        if (showPessoaJuridica) {
            btnJuridica.addClass('active-switch');
            //wrapInputCnpj.removeClass('hide');
            return;
        }

        btnJuridica.removeClass('active-switch');
        //wrapInputCnpj.addClass('hide');
    }

    /** Habilita o Botão Fornecedor e mostra o Botão Funcionário **/
    function mostrarFornecedor(showFornecedor) {
        if (showFornecedor) {
            btnFornecedor.addClass('active-switch');
            //wrapInputCnpj.removeClass('hide');
            //$('#wrapperFuncionario').removeClass('hide');
            //$('.login-form').removeClass('hide');
            return;
        }

        btnFornecedor.removeClass('active-switch');
    }

    /** Troca entre Fornecedor e Comprador **/
    function switchCompradorFornecedor(input) {
        //$('.cadastro-login .cadastro-form').addClass('hide');

        if ($(input).is(':checked')) { //Seleciona o Fornecedor
            mostrarComprador(false);
            uncheckSliderAndButtons(switchJuridica);
            mostrarFornecedor(true);

        } else { //Seleciona o Comprador
            mostrarFornecedor(false);
            mostrarComprador(true);
        }
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
    }

    //// Funcao para aparecer / esconder entre Fornecedor ou Comprador
    //function switchCompradorFornecedor(input) {
    //    $('.cadastro-login .cadastro-form').hide();

    //    if ($(input).is(':checked')) { //Seleciona o Fornecedor

    //        btnComprador.removeClass('active-switch');
    //        $('.cadastro-login #wrapperFisicaJuridica').hide();
    //        uncheckSliderAndButtons(switchJuridica);

    //        btnFornecedor.addClass('active-switch');
    //        $('.cadastro-login #wrapperFuncionario').show();
    //        $('.cadastro-login .cadastro-form').show();
    //        inputCpf.hide();
    //        inputCnpj.mask('00.000.000/0000-00').show();
    //        inputHidden.val('2');

    //    } else { //Seleciona o Comprador

    //        btnFornecedor.removeClass('active-switch');
    //        uncheckSliderAndButtons(switchFuncionario);
    //        $('.cadastro-login #wrapperFuncionario').hide();

    //        btnComprador.addClass('active-switch');
    //        $('.cadastro-login #wrapperFisicaJuridica').show();
    //        inputFuncionario.hide();
    //    }
    //}

    // Funcao para aparecer / esconder entre pessoa Fisica ou Juridica
    //function switchTipoDePessoa(input) {

    //    if ($(input).is(':checked')) { //Seleciona pessoa Juridica

    //        btnFisica.removeClass('active-switch');
    //        inputCpf.hide();

    //        btnJuridica.addClass('active-switch');
    //        inputCnpj.show();
    //        inputHidden.val('1');

    //    } else { //Seleciona pessoa Fisica

    //        btnJuridica.removeClass('active-switch');
    //        inputCnpj.hide();

    //        btnFisica.addClass('active-switch');
    //        inputCpf.show();
    //        inputHidden.val('0');
    //    }

    //    $('.cadastro-login .cadastro-form').show();
    //}

    //// Funcao para aparecer / esconder Funcionario
    //function switchTipoFuncionario(input) {

    //    if ($(input).is(':checked') && btnFuncionario.hasClass('active-switch')) {
    //        btnFuncionario.removeClass('active-switch');
    //        switchFuncionario.prop('checked', false);
    //        inputFuncionario.hide();
    //        $('.cadastro-login .wrap-login').removeClass('has-employee');
    //        inputHidden.val('2');

    //    } else {
    //        btnFuncionario.addClass('active-switch');
    //        switchFuncionario.prop('checked', true);
    //        $('.cadastro-login .wrap-login').addClass('has-employee');
    //        inputFuncionario.show();
    //        inputHidden.val('3');
    //    }

    //    $('.cadastro-login .cadastro-form').show();
    //}

    /** Inicia assim que a pagina e carregada **/
    function init() {
        $('.cadastro-login #wrapperFisicaJuridica').addClass('hide');
    }

    init();
});