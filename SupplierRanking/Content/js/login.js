
$(document).ready(function () {
    "use strict";

    $('#funcionario').hide();
    $('#switchFuncionario').hide();
    $('#labelCheckFuncionario').hide();
    $('#inputFornecedor').hide();


    //Fun��o para o switch Fornecedor
    $('#switchFornecedor').change(function () {

        if (this.checked) {
            //input
            $('#inputFornecedor').show();
            $('#inputFisica').hide();


            //switch
            $('#switchFuncionario').show();
            $("#funcionario").show();
            $('#labelCheckFuncionario').show();
            $('#switchJuridica').hide();
            $("#fisica").hide();
            $('#juridica').hide();
            $('#labelCheckJuri').hide();


        } else {
            //Switch
            $('#funcionario').hide();
            $('#swtichFuncionario').hide();
            $('#labelCheckFuncionario').hide();
            $('#switchJuridica').show();
            $("#fisica").show();
            $('#juridica').show();
            $('#labelCheckJuri').show();

            //input
            $('#inputFuncionario').hide();
            $('#inputFornecedor').hide();
            $('#inputFisica').show();


        }
    });

    $('#inputFuncionario').hide();

    //Fun��o para switch funcionario
    $('#switchFuncionario').change(function () {

        if (this.checked) {

            //input
            $('#inputFuncionario').show();
            $('#inputFornecedor').hide();

        } else {
            //input          
            $('#inputFuncionario').hide();
            $('#inputFornecedor').show();

        }
    });

    $('#inputJuridica').hide();
    $('#inputFisica').show();

    //Fun��o para o switch Juridica ou f�sica
    $('#switchJuridica').change(function () {

        if (this.checked) {

            //input
            $('#inputJuridica').show();
            $('#inputFisica').hide();

        } else {
            //input
            $('#inputJuridica').hide();
            $('#inputFisica').show();


        }
    });

    


//----------------FUN��O PARA O CADASTRO ---------------------------


$(document).ready(function () {
    "use strict";

    $('#inputCPF').show();
    $('#inputNome').show();
    $('#inputSobrenome').show();
    $('#inputCPF').show();
    $('#inputCPF').show();
    $('#inputCPF').show();
    


    //Fun��o para o switch Fornecedor
    $('#switchFornecedor').change(function () {

        if (this.checked) {
            //input
            $('#inputFornecedor').show();
          


            //switch
            $('#switchFuncionario').show();
          


        } else {
            //Switch
            $('#funcionario').hide();
            
            //input
            $('#inputFuncionario').hide();
            


        }
    })







});
    
   