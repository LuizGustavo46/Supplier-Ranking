
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
            
        inputCpf = $('#inputCpf'),
        inputCnpj = $('#inputCnpj'),
        inputFuncionario = $('#inputFuncionario'),
        inputHidden = $('#inputHidden');


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

    function init() {
        $('#wrapperFisicaJuridica').hide();
        $('#wrapperFuncionario').hide();
        $('.login-form').hide();
        inputFuncionario.hide();
        //inputHidden.val(0);
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
        //slider.removeClass('unchecked');
    }

    // Funcao para aparecer / esconder entre Fornecedor ou Comprador
    function switchCompradorFornecedor(input) {
        $('.login-form').hide();

        if ($(input).is(':checked')) { //Seleciona o Fornecedor

            btnComprador.removeClass('active-switch');
            $('#wrapperFisicaJuridica').hide();
            uncheckSliderAndButtons(switchJuridica);

            btnFornecedor.addClass('active-switch');
            $('#wrapperFuncionario').show();
            $('.login-form').show();
            inputHidden.val('2');
            inputCpf.hide();
            inputCnpj.show();

        } else { //Seleciona o Comprador

            btnFornecedor.removeClass('active-switch');
            uncheckSliderAndButtons(switchFuncionario);
            $('#wrapperFuncionario').hide();

            btnComprador.addClass('active-switch');
            $('#wrapperFisicaJuridica').show();
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

        $('.login-form').show();
    }

    // Funcao para aparecer / esconder Funcionario
    function switchTipoFuncionario(input) {

        if ($(input).is(':checked') && btnFuncionario.hasClass('active-switch')) {
            btnFuncionario.removeClass('active-switch');
            switchFuncionario.prop('checked', false);
            inputFuncionario.hide();

        } else {
            btnFuncionario.addClass('active-switch');
            switchFuncionario.prop('checked', true);
            inputFuncionario.show();
            inputHidden.val('3');
        }

        $('.login-form').show();
    }

    /*==================================================================
    [ Validate ]*/
    //var input = $('.validate-input .input100');

    //$('.validate-form').on('submit',function(){
    //    var check = true;

    //    for(var i=0; i<input.length; i++) {
    //        if(validate(input[i]) == false){
    //            showValidate(input[i]);
    //            check=false;
    //        }
    //    }

    //    return check;
    //});


    //$('.validate-form .input100').each(function(){
    //    $(this).focus(function(){
    //       hideValidate(this);
    //    });
    //});

    //function validate (input) {
    //    if($(input).attr('type') == 'email' || $(input).attr('name') == 'email') {
    //        if($(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
    //            return false;
    //        }
    //    }
    //    else {
    //        if($(input).val().trim() == ''){
    //            return false;
    //        }
    //    }
    //}

    //function showValidate(input) {
    //    var thisAlert = $(input).parent();

    //    $(thisAlert).addClass('alert-validate');
    //}

    //function hideValidate(input) {
    //    var thisAlert = $(input).parent();

    //    $(thisAlert).removeClass('alert-validate');
    //}
    
    init();
});