
$(document).ready(function () {
    'use strict';

    var btnComprador = $('.wrap-restaurar-senha-comprador #btnComprador'),
        btnFornecedor = $('.wrap-restaurar-senha-comprador #btnFornecedor'),
        btnFisica = $('.wrap-restaurar-senha-comprador #btnFisica'),
        btnJuridica = $('.wrap-restaurar-senha-comprador #btnJuridica'),
        btnFuncionario = $('.wrap-restaurar-senha-comprador #btnFuncionario'),
       btnEnviarEmail = $('.wrap-restaurar-senha-comprador #btnEnviarEmail'),

      // btnConfirmaSenha = $('.wrap-restaurar-senha-comprador #btnConfirmaSenha'),

        switchFornecedor = $('.wrap-restaurar-senha-comprador #switchFornecedor'),
        switchJuridica = $('.wrap-restaurar-senha-comprador #switchJuridica'),
        switchFuncionario = $('.wrap-restaurar-senha-comprador #switchFuncionario'),

        sliderFornecedorComprador = $('.wrap-restaurar-senha-comprador .slider-fornecedor'),
        sliderFisicaJuridica = $('.wrap-restaurar-senha-comprador .slider-juridica'),
        sliderFuncionario = $('.wrap-restaurar-senha-comprador .slider-funcionario'),

        wrapInputCpf = $('.wrap-restaurar-senha-comprador #inputCpf').closest('.restaurar-senha-comprador-form  .wrap-input'),
        wrapInputCnpj = $('.wrap-restaurar-senha-comprador #inputCnpj').closest('.restaurar-senha-comprador-form  .wrap-input'),
        wrapInputFuncionario = $('.wrap-restaurar-senha-comprador #inputFuncionario').closest('.restaurar-senha-comprador-form  .wrap-input'),
        wrapInputEmail = $('.wrap-restaurar-senha-comprador #inputEmail'),

       // wrapInputSenha = $('.wrap-restaurar-senha-comprador #inputSenha'),
       // wrapInputConfirmaSenha = $('.wrap-restaurar-senha-comprador #inputConfirmaSenha').closet('.confirma-senha-btn .wrap-input'),


        inputHidden = $('.wrap-restaurar-senha-comprador #inputHidden'),
        formInputs = $('.wrap-restaurar-senha-comprador .restaurar-senha-comprador-form  input'),
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

    /** Habilita o Botão Fornecedor e mostra o Botão Funcionário **/
    function mostrarFornecedor(showFornecedor) {
        if (showFornecedor) {
            btnFornecedor.addClass('active-switch');
            wrapInputCpf.addClass('hide');
            wrapInputCnpj.removeClass('hide');
            $('#wrapperFuncionario').removeClass('hide');
            $('.restaurar-senha-comprador-form ').removeClass('hide');
            wrapInputEmail.removeClass('hide');

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
            wrapInputEmail.addClass('hide');
            $('.wrap-restaurar-senha').addClass('has-employee');
            $('#wrapperFuncionario').removeClass('hide');

            inputHidden.val('3');
            return;
        }

        // Esconde os campos e desabilita o botão
        btnFuncionario.removeClass('active-switch');
        switchFuncionario.prop('checked', false);
        wrapInputFuncionario.addClass('hide');
        $('.wrap-restaurar-senha').removeClass('has-employee');

        inputHidden.val('2');
    }

    /** Troca entre Fornecedor e Comprador **/
    function switchCompradorFornecedor(input) {
        $('.restaurar-senha-comprador-form ').addClass('hide');

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
        $('.restaurar-senha-comprador-form ').removeClass('hide');
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
        $('.restaurar-senha-comprador-form ').removeClass('hide');
    }

    ///** Verifica todos os inputs visíveis **/
    //function verificaInputsVisiveis() {
    //    return $('restaura-senha-form .wrap-input').not('.hide').find('input');
    //}

    ///** Habilita/desabilita o Botão Entrar conforme o valor dos campos inputs **/
    //function verificarInputsVazios() {
    //    var isEmpty = false;

    //    activeformInputs.each(function () { // percorre todos os inputs 

    //        if ($(this).val() == '') { // se houver pelo menos um campo vazio, entra no if
    //            isEmpty = true;
    //            return false; // para o loop, evitando que mais inputs sejam verificados sem necessidade
    //        }
    //    });

    //    if (isEmpty) { // Habilita/desabilita o Botão Entrar
    //        $('restaura-senha-form-btn').attr('disabled', 'disabled').addClass('disabled');
    //    } else {
    //        $('restaura-senha-form-btn').removeAttr('disabled').removeClass('disabled');
    //    }
    //}


    $('.restaurar-senha-comprador-btn').on('click', function () {
        //submitForm();
        //console.log('foi');

        $('.restaurar-senha-comprador-form ').submit();
    });

    function submitForm() {
        if (!verificarInputsVazios()) {
            $('.error-msg').removeClass('hide');


        } else {
            $('.error-msg').addClass('hide');
            $('.restaurar-senha-comprador-form ').submit();   
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