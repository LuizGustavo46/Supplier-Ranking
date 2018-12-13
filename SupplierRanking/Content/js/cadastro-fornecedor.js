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
        btnPlanoFree = $('.wrap-cadastro-fornecedor #btnPlanoFree'),
        btnPlanoPremium = $('.wrap-cadastro-fornecedor #btnPlanoPremium'),
        btnCadastrar = $('.wrap-cadastro-fornecedor #btnCadastrar'),

        inputSenhas = $('.wrap-cadastro-fornecedor .input-senha'),
        imgSenhas = $('.wrap-cadastro-fornecedor #btnSenha img'),

        divOptions = $('.wrap-cadastro-fornecedor .wrap-options'),
        divFirstPart = $('.wrap-cadastro-fornecedor .form-forn-first-part'),
        divSecondPart = $('.wrap-cadastro-fornecedor .form-forn-second-part'),
        divThridPart = $('.wrap-cadastro-fornecedor .form-forn-third-part'),
        divForthPart = $('.wrap-cadastro-fornecedor .form-forn-forth-part'),

        formCadastro = $('.wrap-cadastro-fornecedor .cadastro-form'),
        formTitle = $('.wrap-cadastro-fornecedor .form-title h2'),
        formInputs = $('.wrap-cadastro-fornecedor .cadastro-form input, textarea'),

        switchPlano = $('.wrap-cadastro-fornecedor #switchPlanos'),
        sliderPlano = $('.wrap-cadastro-fornecedor .slider-planos'),

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

    /*** Botão para visualizar a Senha ***/
    btnVisualizarSenha.on('click', function (e) {
        visualizarSenhas(e);
    });

    /*** Botão para carregar a imagem de perfil ***/
    btnImagemPerfil.on('change', function () {
        showImagemPerfilCarregada(this);
    });

    /*** Botão para mudar a proxima etapa de Cadastro ***/
    btnProximo.on('click', function () {
        mostrarProximoConteudo();
    });

    /*** Botão para voltar para a etapa anterior de Cadastro ***/
    btnVoltar.on('click', function () {
        mostrarConteudoAnterior();
    });

    /*** Botão para carregar uma imagem ***/
    btnImagens.on('change', function (ev) {
        showImagensCarregadas(ev);
    });

    /*** Botão para carregar o PDF ***/
    btnPdf.on('change', function (ev) {
        addPdf(ev);
    });

    /*** Botão para carregar o PDF ***/
    btnPdf.on('click', function (ev) {
        removePdf();
    });

    /*** Botão para voltar para a etapa anterior de Cadastro ***/
    btnPlanoFree.on('click', function (e) {
        var switchPlanoPremium = switchPlano.prop('checked', false);

        uncheckedSlider(sliderPlano);
        switchPlanos(e, switchPlanoPremium);
    });
    
    /*** Botão para voltar para a etapa anterior de Cadastro ***/
    btnPlanoPremium.on('click', function (e) {
        var switchPlanoPremium = switchPlano.prop('checked', true);

        uncheckedSlider(sliderPlano);
        switchPlanos(e, switchPlanoPremium);
    });

    /*** Switch slider para selecionar Plano Free ou Plano Premium ***/
    switchPlano.on('click', function (e) {
        uncheckedSlider(sliderPlano);
        switchPlanos(e, switchPlano);
    });

    /*** Botão para cadastrar ***/
    btnCadastrar.on('click', function () {
        formCadastro.submit();
    });


/********************* *********************  FUNÇÕES ********************* *********************/

    /** Transforma as senhas em text/pass **/
    function visualizarSenhas(e) {
        e.preventDefault();

        if (inputSenhas.hasClass('show-pass')) {
            inputSenhas.removeClass('show-pass').attr('type', 'password');
            imgSenhas.attr('src', '/Content/images/ver-senha.png');
            return;
        }

        inputSenhas.addClass('show-pass').attr('type', 'text');
        imgSenhas.attr('src', '/Content/images/esconder-senha.png');
    }

    /** Verifica todos os inputs visíveis **/
    function verificaInputsVisiveis() {
        return $('.wrap-cadastro-fornecedor .form-parts').not('.hide').find('.required-field');
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

    /** Renderiza a imagem de perfil escolhida na tela **/
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
        return $('.wrap-cadastro-fornecedor .cadastro-form .checkbox-container input[type=radio]');
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
            btnProximo.attr('disabled', 'disabled').addClass('disabled');
        } else {
            btnProximo.removeAttr('disabled').removeClass('disabled');
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
                    "<input type=\"file\" name=\"galeriaFotos\" style=\"display: none\" value=\""+ event.target.result +"\"/>" +
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
    function addPdf(input) {
        var reader = new FileReader(),
            file = $('#btnPdf')[0],
            fullPath = file.value.split('.')[0],
            filename = fullPath.replace(/^.*[\\\/]/, '');

        // Quando add um PDF muda o ícone e inclui o nome do arquivo embaixo
            $('#wrapper-remove-pdf').removeClass('hide');
            $('#wrapper-add-pdf').addClass('hide');
            $("<p>" + filename + "</p>").appendTo($('#nomePdf'));

        reader.readAsDataURL(file.files[0]);
    }

    /** Remove o PDF do Fornecedor **/
    function removePdf() {

        // Quando add um PDF muda o ícone e inclui o nome do arquivo embaixo
        $('#wrapper-remove-pdf').addClass('hide');
        $('#wrapper-add-pdf').removeClass('hide');
        $("#nomePdf p").remove();
    }

    /** Tira o slider da posição neutra **/
    function uncheckedSlider(slider) {
        slider.removeClass('unchecked');
    }

    /** Seleciona os planos **/
    function switchPlanos(e, input) {
        e.preventDefault();

        if ($(input).is(':checked')) { //Seleciona o Plano Premium
            $('.input-plano').val('P');
            uncheckedSlider(switchPlano);
            btnPlanoFree.removeClass('active-switch');
            btnPlanoPremium.addClass('active-switch');
            
        } else { //Seleciona o Plano Free
            $('.input-plano').val('F');
            btnPlanoFree.addClass('active-switch');
            btnPlanoPremium.removeClass('active-switch');
        }

        if ($('.input-plano').val().length) {
            btnCadastrar.removeAttr('disabled').removeClass('disabled');
        } else {
            btnCadastrar.attr('disabled', 'disabled').addClass('disabled');
        }
    }

    /** Muda para a próxima tela de conteúdo dos passos de Cadastro ***/
    function mostrarProximoConteudo() {

        if (!divFirstPart.hasClass('hide')) { // Muda para a segunda etapa
            divOptions.addClass('hide');
            divFirstPart.addClass('hide');
            divSecondPart.removeClass('hide');

            btnLogin.parent('.form-forn-btn').addClass('hide');
            btnVoltar.parent('.form-forn-btn').removeClass('hide');
            btnProximo.attr('disabled', 'disabled').addClass('disabled');

            formCadastro.removeClass('mt-4 pt-4 pr-2 pl-0');
            formTitle.text('Estamos quase lá...');

            activeformInputs = verificaInputsVisiveis();
            activeformInputs.on('input', verificaInputsVazios);
            return;
        }

        if (!divSecondPart.hasClass('hide')) { // Muda para a terceira etapa
            divSecondPart.addClass('hide');
            divThridPart.removeClass('hide');
            btnProximo.attr('disabled', 'disabled').addClass('disabled');

            formCadastro.removeClass('mt-4 pt-4 pr-2 pl-0');
            formTitle.text('O que você fornece?');

            activeformChechbox = verificaCheckBoxVisiveis();
            activeformChechbox.on('input', verificaCheckBox);
            return;
        }

        if (!divThridPart.hasClass('hide')) { // Muda para a quarta etapa
            divThridPart.addClass('hide');
            divForthPart.removeClass('hide');

            btnProximo.parent('.form-forn-btn').addClass('hide');
            btnCadastrar.attr('disabled', 'disabled').addClass('disabled').parent('.form-forn-btn').removeClass('hide');

            formCadastro.removeClass('mt-4 pt-4 pr-2 pl-0');
            formTitle.text('E para finalizar!');
            return;
        }
    }

    /** Muda para a tela anterior de conteúdo dos passos de Cadastro ***/
    function mostrarConteudoAnterior() {

        if (!divSecondPart.hasClass('hide')) { // Volta para a primeira etapa
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

        if (!divThridPart.hasClass('hide')) { // Volta para a segunda etapa
            divSecondPart.removeClass('hide');
            divThridPart.addClass('hide');

            formCadastro.removeClass('mt-4 pt-4 pr-2 pl-0');
            formTitle.text('Estamos quase lá...');
            return;
        }

        if (!divForthPart.hasClass('hide')) { // Volta para a terceira etapa
            divThridPart.removeClass('hide');
            divForthPart.addClass('hide');

            formCadastro.removeClass('mt-4 pt-4 pr-2 pl-0');
            formTitle.text('O que você fornece?');
            return;
        }
    }

    /** Inicia assim que a pagina e carregada **/
    function init() {
        //formInputs.val('');

        activeformInputs = verificaInputsVisiveis();
        activeformInputs.on('input', verificaInputsVazios);
    }

    init();
});
