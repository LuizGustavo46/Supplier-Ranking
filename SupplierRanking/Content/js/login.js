
$(document).ready(function () {
    'use strict';

    var btnComprador = $('.wrap-login #btnComprador'),
        btnFornecedor = $('.wrap-login #btnFornecedor'),
        btnFisica = $('.wrap-login #btnFisica'),
        btnJuridica = $('.wrap-login #btnJuridica'),
        btnFuncionario = $('.wrap-login #btnFuncionario'),
        btnLogin = $('.wrap-login .login-form-btn'),

        switchFornecedor = $('.wrap-login #switchFornecedor'),
        switchJuridica = $('.wrap-login #switchJuridica'),
        switchFuncionario = $('.wrap-login #switchFuncionario'),

        sliderFornecedorComprador = $('.wrap-login .slider-fornecedor'),
        sliderFisicaJuridica = $('.wrap-login .slider-juridica'),
        sliderFuncionario = $('.wrap-login .slider-funcionario'),

        wrapInputCpf = $('.wrap-login #inputCpf').closest('.login-form .wrap-input'),
        wrapInputCnpj = $('.wrap-login #inputCnpj').closest('.login-form .wrap-input'),
        wrapInputFuncionario = $('.wrap-login #inputFuncionario').closest('.login-form .wrap-input'),

        inputHidden = $('#inputHidden'),
        formInputs = $('.wrap-login .login-form input').not('#inputHidden'),
        activeformInputs;


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

    /** Botão para selecionar Funcionário **/
    btnFuncionario.on('click', function () {
        var switchFuncionarioTrueOrFalse = switchFuncionario.prop('checked', true);

        switchTipoFuncionario(switchFuncionarioTrueOrFalse);
    });

    /** Switch slider para selecionar Funcionário **/
    switchFuncionario.change(function () {
        var switchFuncionarioTrueOrFalse = switchFuncionario.prop('checked') === true ? switchFuncionario.prop('checked', false) : switchFuncionario.prop('checked', true);
        
        switchTipoFuncionario(switchFuncionarioTrueOrFalse);
    });

    /** Botão Login **/
    $('.login-form-btn').on('click', function () {
        $('.login-form').submit();
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

            inputHidden.val('1');
            return;
        }

        btnJuridica.removeClass('active-switch');
        wrapInputCnpj.addClass('hide');
    }

    /** Habilita o Botão Fornecedor e mostra o Botão Funcionário **/
    function mostrarFornecedor(showFornecedor) {
        if (showFornecedor) { 
            btnFornecedor.addClass('active-switch');
            wrapInputCnpj.removeClass('hide');
            $('#wrapperFuncionario').removeClass('hide');
            $('.login-form').removeClass('hide');

            inputHidden.val('2');
            return;
        }

        btnFornecedor.removeClass('active-switch');
    }

    /** Habilita o Botão Funcionário e mostra os campos **/
    function mostrarFuncionario(showInputs, hideWrap) {

        if (hideWrap) { // Esconde Funcionário
            $('#wrapperFuncionario').addClass('hide');
        }

        if (showInputs) { // Mostra os campos e habilita o botão
            btnFuncionario.addClass('active-switch');
            switchFuncionario.prop('checked', true);
            wrapInputFuncionario.removeClass('hide');
            $('.wrap-login').addClass('has-employee');
            $('#wrapperFuncionario').removeClass('hide');

            inputHidden.val('3');
            return;
        }

        // Esconde os campos e desabilita o botão
        btnFuncionario.removeClass('active-switch');
        switchFuncionario.prop('checked', false);
        wrapInputFuncionario.addClass('hide');
        $('.wrap-login').removeClass('has-employee');

        inputHidden.val('2');
    }

    /** Troca entre Fornecedor e Comprador **/
    function switchCompradorFornecedor(input) {
        $('.login-form').addClass('hide');

        if ($(input).is(':checked')) { //Seleciona o Fornecedor
            mostrarComprador(false);
            uncheckSliderAndButtons(switchJuridica);
            mostrarFornecedor(true);


        } else { //Seleciona o Comprador
            mostrarFornecedor(false);
            mostrarFuncionario(false, true);
            mostrarComprador(true);
        }

        formInputs.val('');
        activeformInputs = verificaInputsVisiveis();
        activeformInputs.on('input', verificarInputsVazios);
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

        formInputs.val('');
        activeformInputs = verificaInputsVisiveis();
        activeformInputs.on('input', verificarInputsVazios);
        $('.login-form').removeClass('hide');
    }

    /** Mostra/esconder o Funcionário **/
    function switchTipoFuncionario(input) {

        if ($(input).is(':checked') && btnFuncionario.hasClass('active-switch')) { //Seleciona Fornecedor
            mostrarFuncionario(false, false);

        } else { //Seleciona Funcionário
            mostrarFuncionario(true, false);
        }

        formInputs.val('');
        activeformInputs = verificaInputsVisiveis();
        activeformInputs.on('input', verificarInputsVazios);
        $('.login-form').removeClass('hide');
    }

    /** Verifica todos os inputs visíveis **/
    function verificaInputsVisiveis() {
        return $('.login-form .wrap-input').not('.hide').find('input');
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
            $('.login-form-btn').attr('disabled', 'disabled').addClass('disabled');
        } else {
            $('.login-form-btn').removeAttr('disabled').removeClass('disabled');
        }
    }

    /** Inicia assim que a pagina e carregada **/
    function init() {
        wrapInputCpf.addClass('hide');
        wrapInputCnpj.addClass('hide');
        wrapInputFuncionario.addClass('hide');
    }

    init();
});