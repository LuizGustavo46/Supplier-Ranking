$(document).ready(function () {
    "use strict";

    $('#wrapperCompradorFisico').hide();
    $('#wrapperCompradorJuridico').hide();
    $('#wrapperFornecedor').hide();
    $('#wrapperFuncionario').hide();
    $('#btnUpdateCadastro').hide();


    //Função para aparecer os campos do Comprador Físico
    $('#btnUpdateCompradorFisico').on('click', function () {
        $('form').attr('action', '../Comprador/UpdatePessoaFisica');
        $('#wrapperCompradorFisico').show();
        $('#wrapperCompradorJuridico').hide();
        $('#wrapperFornecedor').hide();
        $('#wrapperFuncionario').hide();
        $('#btnUpdateCadastro').show();
    });

    //Função para aparecer os campos do Comprador Jurídico
    $('#btnUpdateCompradorJuridico').on('click', function () {
        $('form').attr('action', '../Comprador/UpdatePessoaJuridica');
        $('#wrapperCompradorFisico').hide();
        $('#wrapperCompradorJuridico').show();
        $('#wrapperFornecedor').hide();
        $('#wrapperFuncionario').hide();
        $('#btnUpdateCadastro').show();
    });

    //Função para aparecer os campos do Fornecedor
    $('#btnUpdateFornecedor').on('click', function () {
        $('form').attr('action', '../Fornecedor/UpdateFornecedor');
        $('#wrapperCompradorFisico').hide();
        $('#wrapperCompradorJuridico').hide();
        $('#wrapperFornecedor').show();
        $('#wrapperFuncionario').hide();
        $('#btnUpdateCadastro').show();
    });

    //Função para aparecer os campos do Funcionario
    $('#btnUpdateFuncionario').on('click', function () {
        $('form').attr('action', '../Fornecedor/UpdateFuncionarioFornecedor');
        $('#wrapperCompradorFisico').hide();
        $('#wrapperCompradorJuridico').hide();
        $('#wrapperFornecedor').hide();
        $('#wrapperFuncionario').show();
        $('#btnUpdateCadastro').show();
    });

});