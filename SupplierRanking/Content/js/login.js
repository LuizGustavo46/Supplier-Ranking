
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

        $('#inputCNPJ').hide();
        $('#inputNomeEmpresa').hide();
      

        //Fun��o para o switch Fornecedor
        $('#switchFornecedor').change(function () {

            if (this.checked) {
                //input hide
                $('#inputCPF').hide();
                $('#inputNome').hide();
                $('#inputSobrenome').hide();
               
                $('#switchJuridica').hide();
               
                //input show
                $('#inputCNPJFornecedor').show();
                $('#inputNomeEmpresaFornecedor').show();
                
          


                
               
          


            } else {
               
                
            
                //input show
                $('#inputCPF').show();
                $('#inputNome').show();
                $('#inputSobrenome').show();
               

                //input hide
                $('#inputFuncionario').hide();
            


            }
        })


        $('#inputFuncionario').hide();

       



    })
   
    //Fun��o para switch funcionario
    $('#switchJuridica').change(function () {

        if (this.checked) {

            //input show
            $('#inputCNPJ').show();
            $('#inputNomeEmpresa').show();
           

            //input hide
            $('#inputCPF').hide();
            $('#inputNome').hide();
            $('#inputSobrenome').hide();
            



        } else {
            //input show         
            $('#inputCPF').show();
            $('#inputNome').show();
            $('#inputSobrenome').show();
            //input hide
            $('#inputCNPJ').hide();
            $('#inputNomeEmpresa').hide();
           

        }
    });



});
    
   