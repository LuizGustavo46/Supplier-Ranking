
$(document).ready(function () {
    "use strict";

    var btnComprador = $('#btnComprador'),
        btnFornecedor = $('#btnFornecedor'),
        btnFisica = $('#btnFisica'),
        btnJuridica = $('#btnJuridica'),

        switchFornecedor = $('#switchFornecedor'),
        switchJuridica = $('#switchJuridica'),

        sliderFornecedorComprador = $('.slider-fornecedor'),
        sliderFisicaJuridica = $('.slider-juridica'),
        sliderFuncionario = $('.slider-funcionario');

    //$('#funcionario').hide();
    //$('#switchFuncionario').hide();
    //$('#labelCheckFuncionario').hide();
    //$('#inputFornecedor').hide();

    $('#wrapperFisicaJuridica').hide();
    $('#wrapperFuncionario').hide();


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
        var switchJuriciaTrue = switchJuridica.prop('checked', false);

        uncheckedSlider(sliderFisicaJuridica);
        switchTipoDePessoa(switchJuriciaTrue);
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

    // Funçao para tirar o slider da posiçao neutra  
    function uncheckedSlider(slider) {
        slider.removeClass('unchecked');

        if (slider.checked) {
            //sliderFisicaJuridica.removeClass('unchecked');
        }
    }

    // Funçao para aparecer / esconder entre Fornecedor ou Comprador
    function switchCompradorFornecedor(input) {

        if ($(input).is(":checked")) { //Seleciona o Fornecedor

            btnComprador.removeClass('active-switch');
            $('#wrapperFisicaJuridica').hide();

            btnFornecedor.addClass('active-switch');
            $('#wrapperFuncionario').show();

        } else { //Seleciona o Comprador

            btnFornecedor.removeClass('active-switch');
            $('#wrapperFuncionario').hide();

            btnComprador.addClass('active-switch');
            $('#wrapperFisicaJuridica').show();
        }
    }

    
    // Funçao para aparecer / esconder entre pessoa Fisica ou Juridica
    function switchTipoDePessoa(input) {

        if ($(input).is(":checked")) { //Seleciona pessoa Juridica

            btnFisica.removeClass('active-switch-pessoa');
            //$('#wrapperFisicaJuridica').hide();

            btnJuridica.addClass('active-switch-pessoa');
            //$('#wrapperFuncionario').show();

        } else { //Seleciona pessoa Fisica

            btnJuridica.removeClass('active-switch-pessoa');
            //$('#wrapperFisicaJuridica').hide();

            btnFisica.addClass('active-switch-pessoa');
            //$('#wrapperFuncionario').show();

        }
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
    
});