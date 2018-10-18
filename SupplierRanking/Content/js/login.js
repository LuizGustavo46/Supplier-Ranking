
$(document).ready(function () {
    "use strict";
    $('#inputHidden').val('0');
    $('#funcionario').hide();
    $('#switchFuncionario').hide();
    $('#labelCheckFuncionario').hide();
    $('#inputFornecedor').hide();


    //Função para o switch Fornecedor
    $('#switchFornecedor').change(function () {

        if (this.checked) {
            
            $('#inputHidden').val('2');
            //input
            $('#inputFornecedor').show();
            $('#inputFisica').hide();
            $('#inputJuridica').hide();



            //switch
            $('#switchFuncionario').show();
            $("#funcionario").show();
            $('#labelCheckFuncionario').show();
            $('#switchJuridica').hide();
            $('#switchJuridica').prop('checked', false);
            $("#fisica").hide();
            $('#juridica').hide();
            $('#labelCheckJuri').hide();

            

        } else {
            $('#inputHidden').val('0');
            //Switch
            //document.getElementById('#switchJuridica').checked = false;
            //document.getElementById('#switchFuncionario').checked = false;
            $('#switchFuncionario').prop('checked', false);
          
            $('#funcionario').hide();
            $('#switchFuncionario').hide();
            $('#labelCheckFuncionario').hide();
            $('#switchJuridica').show();
            $("#fisica").show();
            $('#juridica').show();
            $('#labelCheckJuri').show();

            //input
            $('#inputFuncionario').hide();
            $('#inputFornecedor').hide();
            $('#inputFisica').show();
            $('#inputJuridica').hide();

            

        }
    });

    //Função para o switch Juridica ou física
    $('#switchJuridica').change(function () {

        if (this.checked) {
            $('#inputHidden').val('1');
            //input
            $('#inputJuridica').show();
            $('#inputFisica').hide();

        } else {
            $('#inputHidden').val('0');
            //input
            $('#inputJuridica').hide();
            $('#inputFisica').show();


        }
    });

    $('#switchFuncionario').change(function () {

        if (this.checked) {
            $('#inputHidden').val('3');
            //input
            $('#inputFuncionario').show();

        } else {
            $('#inputHidden').val('2');
            //input          
            $('#inputFuncionario').hide();
            $('#inputFornecedor').show();

        }
    });


    $('#inputFuncionario').hide();
    $('#inputFornecedor').hide();
    //Função para switch funcionario
    

    $('#inputJuridica').hide();
    $('#inputFisica').show();

    

    /*==================================================================
    [ Validate ]*/
//    var input = $('.validate-input .input100');

//    $('.validate-form').on('submit',function(){
//        var check = true;

//        for(var i=0; i<input.length; i++) {
//            if(validate(input[i]) == false){
//                showValidate(input[i]);
//                check=false;
//            }
//        }

//        return check;
//    });


//    $('.validate-form .input100').each(function(){
//        $(this).focus(function(){
//           hideValidate(this);
//        });
//    });

//    function validate (input) {
//        if($(input).attr('type') == 'email' || $(input).attr('name') == 'email') {
//            if($(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
//                return false;
//            }
//        }
//        else {
//            if($(input).val().trim() == ''){
//                return false;
//            }
//        }
//    }

//    function showValidate(input) {
//        var thisAlert = $(input).parent();

//        $(thisAlert).addClass('alert-validate');
//    }

//    function hideValidate(input) {
//        var thisAlert = $(input).parent();

//        $(thisAlert).removeClass('alert-validate');
//    }
    
});