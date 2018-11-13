
$(document).ready(function () {
    'use strict';

    var btnComprador = $('.wrap-cadastro-fornecedor #btnComprador'),
        btnVisualizarSenha = $('.wrap-cadastro-fornecedor #btnSenha'),
        btnImagem = $('.wrap-cadastro-fornecedor #btnImagem'),
        btnProximo = $('.wrap-cadastro-fornecedor #btnProximo'),
        btnVoltar = $('.wrap-cadastro-fornecedor #btnVoltar'),
        btnCadastrar = $('.wrap-cadastro-fornecedor #btnCadastrar'),

        inputSenhas = $('.wrap-cadastro-fornecedor .input-senha'),

        divOptions = $('.wrap-cadastro-fornecedor .wrap-options'),
        divFirstPart = $('.wrap-cadastro-fornecedor .form-pj-first-part'),
        divSecondPart = $('.wrap-cadastro-fornecedor .form-pj-second-part'),
        divThridPart = $('.wrap-cadastro-fornecedor .form-pj-third-part'),

        formCadastro = $('.wrap-cadastro-fornecedor .cadastro-form'),
        formTitle = $('.wrap-cadastro-fornecedor .form-title h2'),
        formInputs = $('.wrap-cadastro-fornecedor .cadastro-form input'),

        imgCarregada = $('.wrap-cadastro-fornecedor #imagemCarregada'),
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

    /*** Botao para carregar uma imagem ***/
    btnImagem.on('change', function () {
        showImagemCarregada(this);
    });

    /*** Botao para mudar a proxima etapa de Cadastro ***/
    btnProximo.on('click', function () {
        mostraConteudo2Etapa(true);
    });

    /*** Botao para voltar para a etapa anterior de Cadastro ***/
    btnVoltar.on('click', function () {
        mostraConteudo2Etapa(false);
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
        return $('.wrap-cadastro-fornecedor .cadastro-form .wrap-input').not('.hide').find('input');
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
    function showImagemCarregada(input) {

        if (input.files && input.files[0]) {
            var imagem = new FileReader();

            imagem.onload = function (e) {
                imgCarregada.removeClass('input-image-default').addClass('input-image-carregada').attr('src', e.target.result);
            };

            imagem.readAsDataURL(input.files[0]);

        } else {
            imgCarregada.removeClass('input-image-carregada').addClass('input-image-default').attr('src', urlImageDefault);
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

    /** Muda o conteúdo entre o primeiro passo de Cadastro e o segundo* **/
    function mostraConteudo2Etapa(showThirdPart) {
        if (showThirdPart) {
            divOptions.addClass('hide');
            divFirstPart.addClass('hide');
            divSecondPart.addClass('hide');
            divThridPart.removeClass('hide');
            formCadastro.removeClass('mt-4 pt-4 pr-2 pl-0');
            formTitle.text('O que você procura?');
            return;
        }

        divThridPart.addClass('hide');
        divOptions.removeClass('hide');
        divFirstPart.removeClass('hide');
        divSecondPart.removeClass('hide');
    }

    /** Inicia assim que a pagina e carregada **/
    function init() {
        divThridPart.addClass('hide');
        formInputs.val('');

        //activeformInputs = verificaInputsVisiveis();
        //activeformInputs.on('input', verificaInputsVazios);

        //activeformChechbox = verificaCheckBoxVisiveis();
        //activeformChechbox.on('input', verificaCheckBox);
    }

    init();
});