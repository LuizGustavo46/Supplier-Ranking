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

/** Inicia assim que a pagina e carregada **/
function init() {
    divThridPart.addClass('hide');
    formInputs.val('');

    activeformInputs = verificaInputsVisiveis();
    activeformInputs.on('input', verificaInputsVazios);

    activeformChechbox = verificaCheckBoxVisiveis();
    activeformChechbox.on('input', verificaCheckBox);
}

init();++
