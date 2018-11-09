
$(document).ready(function () {
    'use strict';

    var btnComprador = $('.row-cadastro #btnComprador'),
        btnFornecedor = $('.row-cadastro #btnFornecedor'),
        btnFisica = $('.row-cadastro #btnFisica'),
        btnJuridica = $('.row-cadastro #btnJuridica'),

        switchFornecedor = $('.row-cadastro #switchFornecedor'),
        switchJuridica = $('.row-cadastro #switchJuridica'),

        sliderFornecedorComprador = $('.row-cadastro .slider-fornecedor'),
        sliderFisicaJuridica = $('.row-cadastro .slider-juridica');


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
            return;
        }

        btnFisica.removeClass('active-switch');
    }

    /** Habilita o Botão Pessoa Jurídica e mostra os campos **/
    function mostrarPessoaJuridica(showPessoaJuridica) {
        if (showPessoaJuridica) {
            btnJuridica.addClass('active-switch');
            return;
        }

        btnJuridica.removeClass('active-switch');
    }

    /** Habilita o Botão Fornecedor e mostra o Botão Funcionário **/
    function mostrarFornecedor(showFornecedor) {
        if (showFornecedor) {
            btnFornecedor.addClass('active-switch');
            return;
        }

        btnFornecedor.removeClass('active-switch');
    }

    /** Troca entre Fornecedor e Comprador **/
    function switchCompradorFornecedor(input) {

        if ($(input).is(':checked')) { //Seleciona o Fornecedor
            mostrarComprador(false);
            uncheckSliderAndButtons(switchJuridica);
            mostrarFornecedor(true);

        } else { //Seleciona o Comprador
            console.log('s');
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

    /** Inicia assim que a pagina e carregada **/
    function init() {
        $('.row-cadastro #wrapperFisicaJuridica').addClass('hide');
    }

    init();
});