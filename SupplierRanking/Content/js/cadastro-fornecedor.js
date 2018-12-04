
$(document).ready(function () {
    'use strict';

    var btnComprador = $('.wrap-cadastro-fornecedor #btnComprador'),
        btnVisualizarSenha = $('.wrap-cadastro-fornecedor #btnSenha'),
        btnImagemPerfil = $('.wrap-cadastro-fornecedor #btnImagemPerfil'),
        btnLogin = $('.wrap-cadastro-fornecedor #btnLogin'),
        btnVoltar = $('.wrap-cadastro-fornecedor #btnVoltar'),
        btnProximo = $('.wrap-cadastro-fornecedor #btnProximo'),
        btnImagens = $('.wrap-cadastro-fornecedor #btnImagens'),
        btnPdf = $('.wrap-cadastro-fornecedor #btnPdf'),
        btnCadastrar = $('.wrap-cadastro-fornecedor #btnCadastrar'),

        inputSenhas = $('.wrap-cadastro-fornecedor .input-senha'),

        divOptions = $('.wrap-cadastro-fornecedor .wrap-options'),
        divFirstPart = $('.wrap-cadastro-fornecedor .form-forn-first-part'),
        divSecondPart = $('.wrap-cadastro-fornecedor .form-forn-second-part'),
        divThridPart = $('.wrap-cadastro-fornecedor .form-forn-third-part'),
        divForthPart = $('.wrap-cadastro-fornecedor .form-forn-forth-part'),

        formCadastro = $('.wrap-cadastro-fornecedor .cadastro-form'),
        formTitle = $('.wrap-cadastro-fornecedor .form-title h2'),
        formInputs = $('.wrap-cadastro-fornecedor .cadastro-form input, textarea'),

        imgPerfilCarregada = $('.wrap-cadastro-fornecedor #imagemPerfilCarregada'),
        urlImageDefault = '/Content/images/add-imagem.png',
        activeformInputs,
        activeformChechbox;


    /********************* *********************  COMPORTAMENTO DOS ELEMENTOS ********************* *********************/

    /** Botão para selecionar o Comprador **/
    btnComprador.on('click', function () {
        window.location.href = "/Login/Cadastro";

        btnComprador.addClass('active-switch');
        $('.wrap-cadastro-fornecedor #switchFornecedor').prop('checked', false);
        $('.wrap-cadastro-fornecedor #wrapperFisicaJuridica').removeClass('hide');
    });

    /*** Botao para visualizar a Senha ***/
    btnVisualizarSenha.on('click', function (e) {
        visualizarSenhas(e);
    });

    /*** Botao para carregar a imagem de perfil ***/
    btnImagemPerfil.on('change', function () {
        showImagemPerfilCarregada(this);
    });

    /*** Botao para mudar a proxima etapa de Cadastro ***/
    btnProximo.on('click', function () {
        mostrarProximoConteudo();
    });

    /*** Botao para voltar para a etapa anterior de Cadastro ***/
    btnVoltar.on('click', function () {
        mostrarConteudoAnterior();
    });

    /*** Botao para carregar uma imagem ***/
    btnImagens.on('change', function (ev) {
        showImagensCarregadas(ev);
    });

    /*** Botao para carregar o PDF ***/
    btnPdf.on('change', function (ev) {
        showPdf(ev);
    });

    /*** Botao para cadastrar ***/
    btnCadastrar.on('click', function () {
        formCadastro.submit();
    });


    /********************* *********************  FUNÇÕES ********************* *********************/

    /** Transforma as senhas em text/pass **/
    function visualizarSenhas(e) {
        e.preventDefault();

        if (inputSenhas.hasClass('show-pass')) {
            inputSenhas.removeClass('show-pass').attr('type', 'password');
            return;
        }

        inputSenhas.addClass('show-pass').attr('type', 'text');
    }

    /** Verifica todos os inputs visíveis **/
    function verificaInputsVisiveis() {
        return $('.wrap-cadastro-fornecedor .form-parts').not('.hide').find('.wrap-input input, select');
    }

    /** Habilita/desabilita o Botão Entrar conforme o valor dos campos inputs **/
    function verificaInputsVazios() {
        var isEmpty = false;

        activeformInputs.each(function () { // percorre todos os inputs 

            if ($(this).val() == '') { // se houver pelo menos um campo vazio, entra no if
                isEmpty = true;
                return false; // para o loop, evitando que mais inputs sejam verificados sem necessidade
            }
        });

        if (isEmpty) { // Habilita/desabilita o Botão Entrar
            btnProximo.attr('disabled', 'disabled').addClass('disabled');
        } else {
            btnProximo.removeAttr('disabled').removeClass('disabled');
        }
    }

    /** Verifica todos os checkbox visíveis **/
    function showImagemPerfilCarregada(input) {

        if (input.files && input.files[0]) {
            var imagem = new FileReader();

            imagem.onload = function (e) {
                imgPerfilCarregada.removeClass('input-image-default').addClass('input-image-carregada').attr('src', e.target.result);
            };

            imagem.readAsDataURL(input.files[0]);

        } else {
            imgPerfilCarregada.removeClass('input-image-carregada').addClass('input-image-default').attr('src', urlImageDefault);
        }
    }

    /** Verifica todos os checkbox visíveis **/
    function verificaCheckBoxVisiveis() {
        return $('.wrap-cadastro-fornecedor .cadastro-form .checkbox-container input[type=checkbox]');
    }

    /** Habilita/desabilita o Botão Cadastro conforme qunado pelo menos 1 item estiver selecionado **/
    function verificaCheckBox() {
        var isEmpty = true;

        activeformChechbox.each(function () { // percorre todos os checkbox 

            if ($(this).is(':checked')) { // se houver pelo menos um checkbox selecionado, entra no if
                isEmpty = false;
                return false; // para o loop, evitando que mais inputs sejam verificados sem necessidade
            }
        });

        if (isEmpty) { // Habilita/desabilita o Botão Entrar
            btnCadastrar.attr('disabled', 'disabled').addClass('disabled');
        } else {
            btnCadastrar.removeAttr('disabled').removeClass('disabled');
        }
    }

    /** Exibe todas as imagens carregadas dos produtos do Fornecedor **/
    function showImagensCarregadas(input) {
        var files = input.target.files; //FileList object

        for (var i = 0; i < files.length; i++) {
            var file = files[i],
                picReader = new FileReader();

            // Verifica se é uma imagem
            if (!file.type.match('image'))
                continue;

            picReader.onload = function (event) {
                // Cria cada imagem e insere dentro da div imagensCarregada
                $("<span class=\"image-wrapper\">" +
                    "<img src=\"" + event.target.result + "\" title=\"" + file.name + "\"/>" +
                    "<input name=\"galeriaFotos\" style=\"display: none\" value=\""+ event.target.result +"\"/>" +
                    "<span class=\"remove-image hide\"></span>" +
                  "</span>").appendTo($('#imagensCarregada'));

                // Remove a imagem clicada da visão prévia e do cadastro final
                $(".remove-image").click(function () {
                    $(this).parent(".image-wrapper").remove();
                });

                // Exibe o deletar por cima da imagem quando o mouse estiver em cima da imagem
                $('.image-wrapper').on('mouseenter', function () {
                    $(this).find('.remove-image').removeClass('hide');
                });

                // Esconde o deletar por cima da imagem quando o mouse sair de cima da imagem
                $('.image-wrapper').on('mouseleave', function () {
                    $(this).find('.remove-image').addClass('hide');
                });
            };

            // Lê todas as imagens enviadas e renderiza
            picReader.readAsDataURL(file);
        }
    }

    /** Exibe o PDF do Fornecedor **/
    function showPdf(input) {
        var reader = new FileReader(),
            file = $('#btnPdf')[0],
            fullPath = file.value.split('.')[0],
            filename = fullPath.replace(/^.*[\\\/]/, '');

        // Quando add um PDF muda o ícone e inclui o nome do arquivo embaixo
        if ($('#wrapper-remove-pdf').hasClass('hide')) {
            $('#wrapper-remove-pdf').removeClass('hide');
            $('#wrapper-add-pdf').addClass('hide');
            $("<p id=\"nomePdf\">" + filename + "</p>").appendTo($('#nomePdf'));

        } else { // Troca o ícone para o anterior e deleta o arquivo
            $('#wrapper-remove-pdf').addClass('hide');
            $('#wrapper-add-pdf').removeClass('hide');
            $("#nomePdf").remove();
        }

        reader.readAsDataURL(file.files[0]);
    }

    /** Tira o slider da posição neutra **/
    function uncheckedSlider(slider) {
        slider.removeClass('unchecked');
    }

    /** Seleciona os planos **/
    function switchPlanos(input) {

        if ($(input).is(':checked')) { //Seleciona o Fornecedor
            $('.input-plano').val('P');
            $('#btnPlanoFree').removeClass('active-switch');
            $('#btnPlanoPremium').addClass('active-switch');
            
        } else { //Seleciona o Comprador
            $('.input-plano').val('F');
            $('#btnPlanoFree').addClass('active-switch');
            $('#btnPlanoPremium').removeClass('active-switch');
           
        }
    }

    /** Muda para a próxima tela de conteúdo dos passos de Cadastro ***/
    function mostrarProximoConteudo() {

        if (!divFirstPart.hasClass('hide')) {
            divOptions.addClass('hide');
            divFirstPart.addClass('hide');
            divSecondPart.removeClass('hide');

            btnLogin.parent('.form-forn-btn').addClass('hide');
            btnVoltar.parent('.form-forn-btn').removeClass('hide');
            //btnProximo.attr('disabled', 'disabled').addClass('disabled');

            formCadastro.removeClass('mt-4 pt-4 pr-2 pl-0');
            formTitle.text('Estamos quase lá...');

            activeformInputs = verificaInputsVisiveis();
            activeformInputs.on('input', verificaInputsVazios);
            console.log(activeformInputs);
            return;
        }

        if (!divSecondPart.hasClass('hide')) {
            divSecondPart.addClass('hide');
            divThridPart.removeClass('hide');
            //btnCadastrar.attr('disabled', 'disabled').addClass('disabled');

            formCadastro.removeClass('mt-4 pt-4 pr-2 pl-0');
            formTitle.text('O que você fornece?');

            activeformInputs = verificaInputsVisiveis();
            activeformInputs.on('input', verificaInputsVazios);
            return;
        }

        if (!divThridPart.hasClass('hide')) {
            divThridPart.addClass('hide');
            divForthPart.removeClass('hide');

            btnProximo.parent('.form-forn-btn').addClass('hide');
            btnCadastrar.removeAttr('disabled').removeClass('disabled').parent('.form-forn-btn').removeClass('hide');

            formCadastro.removeClass('mt-4 pt-4 pr-2 pl-0');
            formTitle.text('E para finalizar!');
            return;
        }

        divThridPart.addClass('hide');
        divOptions.removeClass('hide');
        divFirstPart.removeClass('hide');
        divSecondPart.removeClass('hide');
    }

    /** Muda para a tela anterior de conteúdo dos passos de Cadastro ***/
    function mostrarConteudoAnterior() {

        if (!divSecondPart.hasClass('hide')) {
            divOptions.removeClass('hide');
            divFirstPart.removeClass('hide');
            divSecondPart.addClass('hide');

            btnLogin.parent('.form-forn-btn').removeClass('hide');
            btnVoltar.parent('.form-forn-btn').addClass('hide');

            formCadastro.removeClass('mt-4 pt-4 pr-2 pl-0');
            formTitle.text('Crie sua Conta');

            activeformInputs = verificaInputsVisiveis();
            activeformInputs.on('input', verificaInputsVazios);
            return;
        }

        if (!divThridPart.hasClass('hide')) {
            divSecondPart.removeClass('hide');
            divThridPart.addClass('hide');

            formCadastro.removeClass('mt-4 pt-4 pr-2 pl-0');
            formTitle.text('Estamos quase lá...');
            return;
        }

        if (!divForthPart.hasClass('hide')) {
            divThridPart.removeClass('hide');
            divForthPart.addClass('hide');

            formCadastro.removeClass('mt-4 pt-4 pr-2 pl-0');
            formTitle.text('O que você fornece?');
            return;
        }
    }

    /** Inicia assim que a pagina e carregada **/
    function init() {
        formInputs.val('');

        activeformInputs = verificaInputsVisiveis();
        activeformInputs.on('input', verificaInputsVazios);

        //activeformChechbox = verificaCheckBoxVisiveis();
        //activeformChechbox.on('input', verificaCheckBox);
    }

    init();
});