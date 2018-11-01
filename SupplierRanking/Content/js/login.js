
$(document).ready(function () {
    'use strict';

    var btnComprador = $('#btnComprador'),
        btnFornecedor = $('#btnFornecedor'),
        btnFisica = $('#btnFisica'),
        btnJuridica = $('#btnJuridica'),
        btnFuncionario = $('#btnFuncionario'),

        switchFornecedor = $('#switchFornecedor'),
        switchJuridica = $('#switchJuridica'),
        switchFuncionario = $('#switchFuncionario'),

        sliderFornecedorComprador = $('.slider-fornecedor'),
        sliderFisicaJuridica = $('.slider-juridica'),
        sliderFuncionario = $('.slider-funcionario'),

        wrapInputCpf = $('#inputCpf').closest('.login-form .wrap-input'),
        wrapInputCnpj =  $('#inputCnpj').closest('.login-form .wrap-input'),
        wrapInputFuncionario = $('#inputFuncionario').closest('.login-form .wrap-input'),

        inputHidden = $('#inputHidden'),
        formInputs = $('.login-form input'),
        activeformInputs;


/********************* *********************  COMPORTAMENTO DOS ELEMENTOS ********************* *********************/

    /** Bot�o para selecionar o Comprador **/
    btnComprador.on('click', function () {
        var switchFornecedorFalse = switchFornecedor.prop('checked', false);

        uncheckedSlider(sliderFornecedorComprador);
        switchCompradorFornecedor(switchFornecedorFalse);
    });

    /** Bot�o para selecionar o Fornecedor **/
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

    /** Bot�o para selecionar Pessoa F�sica **/
    btnFisica.on('click', function () {
        var switchJuriciaFalse = switchJuridica.prop('checked', false);

        uncheckedSlider(sliderFisicaJuridica);
        switchTipoDePessoa(switchJuriciaFalse);
    });

    /** Bot�o para selecionar Pessoa Jur�dica **/
    btnJuridica.on('click', function () {
        var switchJuriciaTrue = switchJuridica.prop('checked', true);

        uncheckedSlider(sliderFisicaJuridica);
        switchTipoDePessoa(switchJuriciaTrue);
    });

    /** Switch slider para selecionar Pessoa F�sica ou Pessoa Jur�dica **/
    switchJuridica.change(function () {
        uncheckedSlider(sliderFisicaJuridica);
        switchCompradorFornecedor(switchJuridica);
    });


    /** Bot�o para selecionar pessoa Funcion�rio **/
    btnFuncionario.on('click', function () {
        var switchFuncionarioTrueOrFalse = switchFuncionario.prop('checked', true);

        switchTipoFuncionario(switchFuncionarioTrueOrFalse);
    });

    /** Switch slider para selecionar Pessoa F�sica ou Pessoa Jur�dica **/
    switchFuncionario.change(function () {
        switchTipoFuncionario(switchFuncionario);
    });


/********************* *********************  FUN��ES ********************* *********************/

    /** Tira o slider da posi��o neutra **/
    function uncheckedSlider(slider) {
        slider.removeClass('unchecked');
    }

    /** Reseta o slider e os bot�es **/
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

    /** Habilita o Bot�o Comprador e mostra os Bot�o Pessoa F�sica e Jur�dica **/
    function mostrarComprador(showComprador) {
        if (showComprador) {
            btnComprador.addClass('active-switch');
            $('#wrapperFisicaJuridica').removeClass('hide');
            return;
        }

        btnComprador.removeClass('active-switch');
        $('#wrapperFisicaJuridica').addClass('hide');
    }

    /** Habilita o Bot�o Pessoa F�sica e mostra os campos **/
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

    /** Habilita o Bot�o Pessoa Jur�dica e mostra os campos **/
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

    /** Habilita o Bot�o Fornecedor e mostra o Bot�o Funcion�rio **/
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

    /** Habilita o Bot�o Funcion�rio e mostra os campos **/
    function mostrarFuncionario(showInputs, hideWrap) {

        if (hideWrap) { // Esconde Funcion�rio
            $('#wrapperFuncionario').addClass('hide');
        }

        if (showInputs) { // Mostra os campos e habilita o bot�o
            btnFuncionario.addClass('active-switch');
            switchFuncionario.prop('checked', true);
            wrapInputFuncionario.removeClass('hide');
            $('.wrap-login').addClass('has-employee');
            $('#wrapperFuncionario').removeClass('hide');

            inputHidden.val('3');
            return;
        }

        // Esconde os campos e desabilita o bot�o
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

    /** Troca entre Pessoa F�sica e Pessoa Jur�dica **/
    function switchTipoDePessoa(input) {
        
        if ($(input).is(':checked')) { //Seleciona pessoa Jur�dica
            mostrarPessoaFisica(false);
            mostrarPessoaJuridica(true);

        } else { //Seleciona pessoa F�sica
            mostrarPessoaJuridica(false);
            mostrarPessoaFisica(true);
        }

        formInputs.val('');
        activeformInputs = verificaInputsVisiveis();
        activeformInputs.on('input', verificarInputsVazios);
        $('.login-form').removeClass('hide');
    }

    /** Mostra/esconder o Funcion�rio **/
    function switchTipoFuncionario(input) {

        if ($(input).is(':checked') && btnFuncionario.hasClass('active-switch')) { //Seleciona Fornecedor
            mostrarFuncionario(false, false);

        } else { //Seleciona Funcion�rio
            mostrarFuncionario(true, false);
        }

        formInputs.val('');
        activeformInputs = verificaInputsVisiveis();
        activeformInputs.on('input', verificarInputsVazios);
        $('.login-form').removeClass('hide');
    }

    /** Verifica todos os inputs vis�veis **/
    function verificaInputsVisiveis() {
        return $('.login-form .wrap-input').not('.hide').find('input');
    }

    /** Habilita/desabilita o Bot�o Entrar conforme o valor dos campos inputs **/
    function verificarInputsVazios() {
        var isEmpty = false;

        activeformInputs.each(function () { // percorre todos os inputs 
            
            if ($(this).val() == '') { // se houver pelo menos um campo vazio, entra no if
                isEmpty = true;
                return false; // para o loop, evitando que mais inputs sejam verificados sem necessidade
            }
        });

        if (isEmpty) { // Habilita/desabilita o Bot�o Entrar
            $('.login-form-btn').attr('disabled', 'disabled').addClass('disabled');
        } else {
            $('.login-form-btn').removeAttr('disabled').removeClass('disabled');
        }
    }


    //$('.login-form-btn').on('click', function () {
    //    console.log('foi 1');
    //    $('.login-form').submit();
    //});

    //$('.login-form').on('submit', function (e) {
        
    //    e.preventDefault(); // Impede o envio do formulario

    //    console.log('foi submit');

    //    if (activeformInputs.verificarInputsVazios()) {
    //        $('.error-msg').removeClass('hide');
    //        console.log('foi 2');
    //    } else {
    //        $('.error-msg').addClass('hide');
    //        $('.login-form').submit();
    //        console.log('num foi');
    //    }
    //});


    /** Inicia assim que a pagina e carregada **/
    function init() {
        wrapInputCpf.addClass('hide');
        wrapInputCnpj.addClass('hide');
        wrapInputFuncionario.addClass('hide');
        console.log(activeformInputs);
    }

    init();
});