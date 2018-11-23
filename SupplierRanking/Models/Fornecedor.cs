﻿/*RESPONSÁVEL PELA CLASSE: MARCELO LEMOS 4INF- A TURMA - B*/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SupplierRanking.Models
{
    public class Fornecedor
    {
        //CONEXÃO COM O BANCO DE DADOS - SE FOR USAR EM CASA É SÓ TROCAR "SENAI" PARA O SEU NOME
        private static SqlConnection con =
            new SqlConnection(ConfigurationManager.ConnectionStrings["SENAI"].ConnectionString);

        //---* DECLARAÇÃO DE VARIAVEIS *-
        private string cnpj;
        private string nome_empresa;
        private string email;
        private string telefone;
        private string cidade;
        private string bairro;
        private string uf;
        private string senha;
        private string celular;
        private string endereco;
        private string descricao;
        private string cep;
        private float media;
        private string slogan;
        private string plano;
        private byte[] imagem;
        private string imagem64;
        private string nome_categoria;
        private float media_qualidade;
        private float media_atendimento;
        private float media_preco;
        private float media_entrega;
        private float media_satisfacao;

        /*Variaveis do funcionário*/
        private int codigo;
        private string nome;
        private string cnpj_fornecedor;

        private bool fornecedor;
        private bool consumidor;

        /*╔►▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ ♦ TORNANDO AS VARIAVEIS ACESSIVEIS ♦ ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╗*/

        public String Cnpj              { get { return cnpj; }                  set { cnpj = value; } }
        public string Nome_empresa      { get { return nome_empresa;}           set { nome_empresa = value; } }
        public String Email             { get { return email; }                 set { email = value; } }
        public String Telefone          { get { return telefone;}               set { telefone = value; } }
        public String Cidade            { get { return cidade; }                set { cidade = value; } }
        public String Bairro            { get { return bairro; }                set { bairro = value; } }
        public String Uf                { get { return uf; }                    set { uf = value; } }
        public byte[] Imagem            { get { return imagem; }                set { imagem = value; } }
        public String Imagem64          { get { return imagem64; }              set { imagem64 = value; } }
        public String Senha             { get { return senha; }                 set { senha = value; } }
        public String Celular           { get { return celular; }               set { celular = value; } }
        public String Endereco          { get { return endereco; }              set { endereco = value; } }
        public String Descricao         { get { return descricao; }             set { descricao = value; } }
        public String Cep               { get { return cep; }                   set { cep = value; }}
        public float Media              { get { return media; }                 set { media = value; } }
        public String Slogan            { get { return slogan; }                set { slogan = value; } }
        public String Plano             { get { return plano; }                 set { plano = value; } }
        public String Nome_categoria    { get { return nome_categoria; }        set { nome_categoria = value; } }
        public float Media_qualidade    { get { return media_qualidade; }       set { media_qualidade = value; } }
        public float Media_atendimento  { get { return media_atendimento; }     set { media_atendimento = value; } }
        public float Media_entrega      { get { return media_entrega; }         set { media_entrega = value; } }
        public float Media_preco        { get { return media_preco; }           set { media_preco = value; } }
        public float Media_satisfacao   { get { return media_satisfacao; }      set { media_satisfacao = value; } }
        public int Codigo               { get { return codigo; }                set { codigo = value; } }
        public String Nome              { get { return nome; }                  set { nome = value; } }
        public String Cnpj_fornecedor   { get { return cnpj_fornecedor; }       set { cnpj_fornecedor = value; } }

        /*╚▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╝*/


        //███████████████████████████████████████████████████████████████████████▓▒░INICIO DOS MÉTODOS░▒▓████████████████████████████████████████████████████████████████████████████

        /*RESPONSÁVEL PELA CLASSE: MARCELO LEMOS 4INF- A TURMA - B*/

        /*╔►▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ ♦ LOGIN ♦ ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╗*/
        public bool Login() //FEITO
        {
            bool res = false;

            try
            {
                con.Open();
                SqlCommand query =
                    new SqlCommand("SELECT * FROM fornecedor WHERE cnpj = @cnpj AND senha = @senha", con);
                query.Parameters.AddWithValue("@cnpj",    cnpj);
                query.Parameters.AddWithValue("@senha",   senha);
                SqlDataReader leitor = query.ExecuteReader();

                res = leitor.HasRows;
            }
            catch (Exception e)
            {   
                // Caso der erro na inserção
                res = false;
            }
            if (con.State == ConnectionState.Open)
                con.Close();// fecha conexão

            return res;// retorna resposta de confirmação
        }

        /*╚▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╝*/

        /*╔►▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ ♦ CADASTRO DE FUNCIONARIO ♦ ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╗*/
        public string CadastroFuncionario(string cnpj, string senha, string nome) //FEITO
        {
            string res = "Inserido com sucesso!";
            try
            {
                //ABRE A CONEXÃO
                con.Open();
                SqlCommand query =
                    new SqlCommand("SELECT * FROM funcionario WHERE cnpj_fornecedor = @cnpj_fornecedor and nome = @nome", con);
                query.Parameters.AddWithValue("@cnpj_fornecedor", cnpj);
                query.Parameters.AddWithValue("@nome", nome);
                SqlDataReader leitor = query.ExecuteReader();

                if (!leitor.Read())
                {
                    leitor.Close();

                    // Criação de comando para inserção no banco
                    SqlCommand query2 =
                        new SqlCommand("INSERT INTO funcionario VALUES (@cnpj_fornecedor,@nome,@senha)", con);

                    //CONDIÇÃO DE CADASTRO (NÃO DEIXA QUE FALTE CAMPOS NECESSARIOS PARA O CADASTRO
                    if (cnpj != "" && nome != "" && senha != "")
                    {
                        query2.Parameters.AddWithValue("@cnpj_fornecedor", cnpj);
                        query2.Parameters.AddWithValue("@nome", nome);
                        query2.Parameters.AddWithValue("@senha", senha);
                        query2.ExecuteNonQuery();
                    }
                    else
                    {
                        res = "Preencha os campos corretamente";
                    }
                }
                else
                {

                    res = "Funcionario com o mesmo nome encotrado";

                }
            }
            catch (Exception ex)
            {
                res = ex.Message; // Caso der erro na inserção
            }

            if (con.State == ConnectionState.Open)
                con.Close(); // fecha conexão

            // retorna resposta de confirmação
            return res; 
        }
        /*╚▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╝*/

        /*╔►▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ ♦ LOGIN FUNCIONARIO ♦ ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╗*/
        public bool LoginFuncionario() //ESPERANDO A VIEW DE LOGIN
        {
            bool res = false;

            try
            {
                con.Open();
                SqlCommand query =
                    new SqlCommand("SELECT * FROM funcionario WHERE codigo = @codigo AND cnpj_fornecedor = @cnpj_fornecedor AND senha = @senha", con);
                query.Parameters.AddWithValue("@codigo", codigo);
                query.Parameters.AddWithValue("@cnpj_fornecedor", cnpj);              
                query.Parameters.AddWithValue("@senha", senha);
                SqlDataReader leitor = query.ExecuteReader();

                res = leitor.HasRows;
            }
            catch (Exception e)
            {
                // Caso der erro na inserção
                res = false;
            }

            if (con.State == ConnectionState.Open)
                con.Close();// fecha conexão

            return res;// retorna resposta de confirmação

        }
        /*╚▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╝*/

        /*╔►▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ ♦ CADASTRO FORNECEDOR ♦ ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╗*/
        public string CadastroFornecedor() //FEITO
        {
            string res = "Cadastro realizado.";
            try
            {   
                // abre conexão
                con.Open();
                 
                // Criação de comando para inserção de dados na tabela fornecedor
                SqlCommand query =
                    new SqlCommand("INSERT INTO fornecedor VALUES (@cnpj,@nome_empresa,@email,@telefone,@celular,@endereco,@bairro,@cidade,@uf,@cep,@senha,@slogan,@descricao,@media,@plano,@imagem,@nome_categoria,@media_qualidade,@media_atendimento,@media_entrega,@media_preco,@media_satisfacao)",
                        con);

                // Compara se todos os campos estao preenchidos corretamente, caso não esteja retorna uma mensagem de erro para o usuario 
                if (cnpj != "" && cnpj.Length <= 19 && nome_empresa != "" && email != "" && telefone != "" && celular != "" && endereco != "" && bairro != "" && bairro != "" && cidade != "" && uf != ""
                    && cep != "" && cep.Length <=8 && slogan != "" && descricao != "" && descricao != "" && nome_categoria != "")
                {
                    query.Parameters.AddWithValue("@cnpj",              cnpj);
                    query.Parameters.AddWithValue("@nome_empresa",      nome_empresa);
                    query.Parameters.AddWithValue("@email",             email);
                    query.Parameters.AddWithValue("@telefone",          telefone);
                    query.Parameters.AddWithValue("@celular",           celular);
                    query.Parameters.AddWithValue("@endereco",          endereco);
                    query.Parameters.AddWithValue("@bairro",            bairro);
                    query.Parameters.AddWithValue("@cidade",            cidade);
                    query.Parameters.AddWithValue("@uf",                uf);
                    query.Parameters.AddWithValue("@cep",               cep);
                    query.Parameters.AddWithValue("@senha",             senha);
                    query.Parameters.AddWithValue("@slogan",            slogan);
                    query.Parameters.AddWithValue("@descricao",         descricao);
                    query.Parameters.AddWithValue("@media",             media);
                    query.Parameters.AddWithValue("@plano",             plano);
                    query.Parameters.AddWithValue("@imagem",            imagem);
                    query.Parameters.AddWithValue("@nome_categoria",    nome_categoria);
                    query.Parameters.AddWithValue("@media_qualidade",   media_qualidade);
                    query.Parameters.AddWithValue("@media_atendimento", media_atendimento);
                    query.Parameters.AddWithValue("@media_entrega",     media_entrega);
                    query.Parameters.AddWithValue("@media_preco",       media_preco);
                    query.Parameters.AddWithValue("@media_satisfacao",  media_satisfacao);
                    query.ExecuteNonQuery();               
                }

                
                else
                {
                    //Mensagem de erro
                    res = "Preencha os campos corretamente";
                }
            }
            catch (Exception ex)
            {
                res = ex.Message; // Caso der erro na inserção
            }

            if (con.State == ConnectionState.Open)
                con.Close(); // fecha conexão

            // retorna resposta de confirmação
            return res; 
        }
        /*╚▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╝*/

        /*╔►▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ ♦ EXCLUIR FUNCIONARIO ♦ ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╗*/
        public bool ExcluirFuncionario(int codigo) //TRAVADO PELA HOME LOGADA
        {
            try
            {
                con.Open();

                SqlCommand query =
                    new SqlCommand("DELETE FROM Funcionario WHERE codigo = @codigo",con);                       
                query.Parameters.AddWithValue("@codigo",    codigo);            
                query.ExecuteNonQuery();
            }
            //tratamento de erro
            catch (Exception ex)
            {
                return false;
            }

            if (con.State == ConnectionState.Open)
                con.Close();

            return true;
        }
        /*╚▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╝*/

        /*╔►▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ ♦ EXCLUIR CONTA - FORNECEDOR ♦ ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╗*/
        public bool ExcluirContaFornecedor(string cnpj) 
        {
            try
            {
                con.Open();
                SqlCommand query =
                        new SqlCommand("DELETE FROM fornecedor WHERE cnpj = @cnpj", con);
                query.Parameters.AddWithValue("@cnpj", cnpj);
                query.ExecuteNonQuery();


            }//tratamento de erro
            catch (Exception ex)
            {
                return false;
            }

            if (con.State == ConnectionState.Open)
                con.Close();//fecha a conexão

            return true;
        }
   
        /*╚▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╝*/

        /*╔►▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ ♦ PESQUISA DE FORNECEDOR ♦ ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╗*/

        public static List<Fornecedor> PesquisaFornecedor(string pesquisa) //TRAVADO PELA HOME LOGADA
        {
            List<Fornecedor> lista = new List<Fornecedor>();
            try
            {   
                // abre conexão
                con.Open();
                 
                // Criação de comando para selecionar a tabela FORNECEDOR
                SqlCommand query =
                    new SqlCommand("SELECT * FROM fornecedor WHERE nome_empresa like @texto", con);
                query.Parameters.AddWithValue("@texto", pesquisa);
                SqlDataReader leitor = query.ExecuteReader();

                

                //prepara o leitor
                while (leitor.Read())
                {
                    Fornecedor f = new Fornecedor();

                    f.Cnpj          = leitor["Cnpj"].ToString();
                    f.Nome_empresa  = leitor["Nome_empresa"].ToString();
                    f.Email         = leitor["Email"].ToString();
                    f.Telefone      = leitor["Telefone"].ToString();
                    f.Celular       = leitor["Celular"].ToString();
                    f.Endereco      = leitor["Endereco"].ToString();
                    f.Bairro        = leitor["Bairro"].ToString();
                    f.Cidade        = leitor["Cidade"].ToString();
                    f.Uf            = leitor["Uf"].ToString();
                    f.Cep           = leitor["Cep"].ToString();
                    f.Slogan        = leitor["Slogan"].ToString();
                    f.Descricao     = leitor["Descricao"].ToString();
                    f.Media         = float.Parse(leitor["Media"].ToString());
                    f.Plano         = leitor["Plano"].ToString();
                    f.Imagem        = (byte[])leitor["Imagem"];
                    f.Imagem64      = Convert.ToBase64String(f.Imagem);
                    f.Nome_categoria = leitor["Nome_categorias"].ToString();
                    if (f.Plano.Equals("P")) //SE O FORNECEDOR FOR PREMIUM MOSTRA AS MÉDIAS DOS CRITÉRIOS DE AVALIAÇÃO
                    {
                        f.Media_qualidade = float.Parse(leitor["Media_qualidade"].ToString());
                        f.Media_atendimento = float.Parse(leitor["Media_atendimento"].ToString());
                        f.Media_entrega = float.Parse(leitor["Media_entrega"].ToString());
                        f.Media_preco = float.Parse(leitor["Media_preco"].ToString());
                        f.Media_satisfacao = float.Parse(leitor["Media_satisfacao"].ToString());
                    }
                    //colocar campo de posiçõ de ranking
                    lista.Add(f); // adiciona os valores cadastrados no banco à lista
                }
            }

            //tratamento de erro
            catch (Exception ex)
            {
                lista = null;
            }

            if (con.State == ConnectionState.Open)
                con.Close();//fecha a conexao

            return lista;
        }

        /*╚▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╝*/

        /*╔►▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ ♦ RESTAURAR SENHA ♦ ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╗*/
        public Boolean EsqueceuSuaSenha(string cnpj, string email) //FEITO
        {
            bool res = false;
            try
            {
                con.Open(); // abre conexão
                SqlCommand query =
                        new SqlCommand("SELECT email FROM fornecedor WHERE  cnpj = @cnpj", con);
                    query.Parameters.AddWithValue("@cnpj", cnpj);
                    query.Parameters.AddWithValue("@email", email);
                SqlDataReader leitor = query.ExecuteReader();

                while (leitor.Read())
                {
                    email = leitor["email"].ToString();
                }
                    //CONFIGURANDO A MENSAGEM
                    MailMessage mail = new MailMessage();
                    //ORIGEM
                    mail.From = new MailAddress("officialsranking@outlook.com");//supplierranking@hotmail.com
                    //DESTINATÁRIO
                    mail.To.Add(email);
                    //ASSUNTO
                    mail.Subject = nome + "REDEFINIÇÃO DE SENHA - Supplier Ranking";
                    //CORPO DO E-MAIL
                    //ESCREVER AQUI A MENSAGEM COM O LINK PARA A PAGINA DE REDEFINIÇÃO DE SENHA.
                    mail.Body = "USER ID: " + codigo + "\nClique aqui para redefinir sua senha:\n" +
                                                    "http://localhost:16962/Fornecedor/NovaSenha";  


                    //CONFIGURAR O SMTP
                    SmtpClient smtpServer = new SmtpClient("smtp.live.com");
                    //CONFIGURAR PORTA
                    smtpServer.Port = 25;
                    //HABILITAR O TLS
                    smtpServer.EnableSsl = true;
                    //CONFIGURAR USUARIO E SENHA PARA LOGAR
                    smtpServer.Credentials = new System.Net.NetworkCredential("officialsranking@outlook.com", "Senai1234");
                    //ENVIAR
                    smtpServer.Send(mail);
              

            }

            //tratamento de erro
            catch (Exception e)
            {
                string em = e.Message;
                res = false;
            }

            if (con.State == System.Data.ConnectionState.Open)
                con.Close();//fecha a conexao

            return res;

        }

        /*╚▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╝*/

        /*╔►▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ ♦ LISTAR FORNECEDOR ♦ ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╗*/
        public static List<Fornecedor> ListaFornecedor()//EXCLUIR MÉTODO APÓS HOME LOGADA
        {
            List<Fornecedor> listaFornecedor = new List<Fornecedor>(); //TRAVADO PELA HOME LOGADA
            try
            {
                con.Open(); // abre conexão

                // Criação de comando para selecionar a tabela de FUNCIONARIOS
                SqlCommand query =
                    new SqlCommand("SELECT * FROM fornecedor", con);
                SqlDataReader leitor = query.ExecuteReader();

                //prepara o leitor
                while (leitor.Read())
                {
                    Fornecedor f = new Fornecedor();

                    f.Cnpj =             leitor["cnpj"].ToString();
                    f.nome_empresa =     leitor["nome_empresa"].ToString();
                    f.Email =            leitor["email"].ToString();

                    listaFornecedor.Add(f); // adiciona os valores cadastrados no banco à lista

                }
            }
            //tratamento de erro
            catch (Exception ex)
            {
                listaFornecedor = null;
            }

            if (con.State == ConnectionState.Open)
                con.Close();//fecha a conexao

            return listaFornecedor;
        }



        /*╚▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╝*/

        /*╔►▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ ♦ LISTAR FUNCIONARIO ♦ ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╗*/
        public static List<Fornecedor> ListaFuncionario() 
        {
            List<Fornecedor> listaFuncionario = new List<Fornecedor>(); //TRAVADO PELA HOME LOGADA
            try
            {
                con.Open(); // abre conexão

                // Criação de comando para selecionar a tabela de FUNCIONARIOS
                SqlCommand query =
                    new SqlCommand("SELECT * FROM funcionario", con);
                SqlDataReader leitor = query.ExecuteReader();

                //prepara o leitor
                while (leitor.Read())
                {
                    Fornecedor f = new Fornecedor();

                    f.Codigo      = int.Parse(leitor["Codigo"].ToString());
                    f.Nome        = leitor["Nome"].ToString();
                    f.Senha       = leitor["Senha"].ToString();

                    listaFuncionario.Add(f); // adiciona os valores cadastrados no banco à lista
                 
                }
            }
            //tratamento de erro
            catch (Exception ex)
            {
                listaFuncionario = null;
            }

            if (con.State == ConnectionState.Open)
                con.Close();//fecha a conexao

            return listaFuncionario;
        }



        /*╚▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╝*/

        /*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*MÉTODOS DE UPDATE-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*/

        /*╔►▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ ♦ UPDATE SENHA ♦ ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╗*/
        public Boolean UpdateSenha(string senha, string novaSenha, string senhaConfirma, string cnpj) //FEITO
        {
            bool res = false;
                     
            try
            {                              
                /*RESPONSÁVEL PELA CLASSE: MARCELO LEMOS 4INF- A TURMA - B*/
                //ABRE A CONEXAO
                con.Open();  

                //comando para selecionar a tabela de FORNECEDOR
                SqlCommand query1 =
                   new SqlCommand("SELECT * FROM fornecedor WHERE cnpj = @cnpj", con);
                query1.Parameters.AddWithValue("@cnpj",    cnpj);//seleciona o perfil do fornecedor no banco através do cnpj
                SqlDataReader leitor = query1.ExecuteReader(); //executa a leitura

                //prepara o leitor
                if (leitor.Read())
                    senha = leitor["senha"].ToString();//guarda a senha que veio do banco

                leitor.Close();//fecha o leitor

                //se a nova senha for diferente da senha atual e a senhaConfirma for igual a novaSenha executa o update 
                if (novaSenha != senha && senhaConfirma == novaSenha)                 
                {
                    SqlCommand query =
                                new SqlCommand("Update fornecedor SET senha = @senha WHERE cnpj = @cnpj", con);
                    query.Parameters.AddWithValue("@cnpj",  cnpj);
                    query.Parameters.AddWithValue("@senha", novaSenha);
                    query.ExecuteNonQuery();//executa o update
                    res = true;
                }
            }
            //tratamento de erro
            catch (Exception e)
            {
                res = false;
            }

            if (con.State == System.Data.ConnectionState.Open)
                con.Close();//fecha a conexao

            return res;
        }

        /*╚▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╝*/

        /*╔►▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ ♦ UPDATE FORNECEDOR ♦ ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╗*/
        public bool UpdateFornecedor(string cnpj, string nome_empresa, string email, string telefone, string bairro, string cidade, string endereco, string uf,
            string celular, string descricao, string cep, string slogan, string nome_categoria) //FEITO
        {
            
            bool res = true;
            try        
            {
                //ABRE CONEXÃO
                con.Open(); 

                //comando para update na tabela de FORNECEDOR
                SqlCommand query =
                    new SqlCommand("UPDATE fornecedor SET nome_empresa = @nome_empresa, email = @email, endereco = @endereco," +
                    "bairro = @bairro, cidade = @cidade, uf = @uf, cep = @cep, telefone = @telefone," +
                    "celular = @celular, descricao = @descricao, slogan = @slogan WHERE cnpj = @cnpj", con);

                string confirmaSenha ="";

                if (nome_empresa.Length >= 1 && email.Length >= 8 && (telefone.Length == 14 || telefone.Length == 0) &&
                    (celular.Length == 15 || celular.Length == 0) && endereco.Length > 1 && bairro.Length > 1 &&
                    cidade.Length > 1 && uf.Length == 2 && cep.Length == 9 && senha == confirmaSenha)
                {
                    query.Parameters.AddWithValue("@cnpj",            cnpj);
                    query.Parameters.AddWithValue("@nome_empresa",    nome_empresa);
                    query.Parameters.AddWithValue("@email",           email);
                    query.Parameters.AddWithValue("@endereco",        endereco);
                    query.Parameters.AddWithValue("@bairro",          bairro);
                    query.Parameters.AddWithValue("@cidade",          cidade);
                    query.Parameters.AddWithValue("@uf",              uf);
                    query.Parameters.AddWithValue("@cep",             cep);
                    query.Parameters.AddWithValue("@telefone",        telefone);
                    query.Parameters.AddWithValue("@celular",         celular);
                    query.Parameters.AddWithValue("@descricao",       descricao);
                    query.Parameters.AddWithValue("@slogan",          slogan);
                    //query.Parameters.AddWithValue("@nome_categoria", nome_categoria);
                    query.ExecuteNonQuery();
                }

                else
                {
                    res = false;
                }                          
            }

            //tratamento de erro
            catch (Exception e)
            {
                string exception = e.Message;
                res = false;
            }

            if (con.State == System.Data.ConnectionState.Open)
                con.Close();//fecha a conexão

            return res;
        }


        /*╚▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╝*/

        /*╔►▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ ♦ PERFIL DO FORNECEDOR ♦ ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╗*/
        public static Fornecedor Perfil(string cnpj) //FEITO
        {
            Fornecedor f = new Fornecedor();
            try
            {
                con.Open(); //ABRE CONEXÃO

                //comando para selecionar o fornecedor apartir do cnpj
                SqlCommand query = new SqlCommand("SELECT * FROM fornecedor WHERE cnpj = @cnpj", con);
                query.Parameters.AddWithValue("@cnpj", cnpj);
                SqlDataReader leitor = query.ExecuteReader();

                //prepara o leitor para pegar as informações a serem exibidas
                if (leitor.Read())
                {
                    f.cnpj            = leitor["Cnpj"].ToString();
                    f.nome_empresa    = leitor["Nome_empresa"].ToString();
                    f.email           = leitor["Email"].ToString();
                    f.telefone        = leitor["Telefone"].ToString();
                    f.celular         = leitor["Celular"].ToString();
                    f.endereco        = leitor["Endereco"].ToString();
                    f.bairro          = leitor["Bairro"].ToString();
                    f.cidade          = leitor["Cidade"].ToString();
                    f.uf              = leitor["Uf"].ToString();
                    f.cep             = leitor["Cep"].ToString();
                    f.slogan          = leitor["Slogan"].ToString();
                    f.descricao       = leitor["Descricao"].ToString();
                    f.media           = float.Parse(leitor["Media"].ToString());
                    f.plano           = leitor["Plano"].ToString();
                    f.imagem          = (byte[])leitor["Imagem"];
                    f.nome_categoria  = leitor["Nome_categorias"].ToString();
                }

            }
            //tratamento de erro
            catch (Exception e)
            {
                f = null;
            }
            if (con.State == ConnectionState.Open)
                con.Close(); //FECHA CONEXÃO

            return f;
        }
        /*╚▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╝*/

        /*╔►▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ ♦ EDITAR DADOS DO FUNCIONARIO DO FORNECEDOR ♦ ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╗*/


        public bool  UpdateFuncionarioFornecedor(int codigo, string nome, string senha) //FEITO
        {          
            try
            {
                con.Open(); //ABRE CONEXÃO

                //comando para fazer o update na tabela FUNCIONARIOS aonde o codigo estiver cadastrado
                SqlCommand query = new SqlCommand("UPDATE funcionario SET nome = @nome, senha = @senha WHERE codigo = @codigo", con);
                query.Parameters.AddWithValue("@codigo",    codigo);
                query.Parameters.AddWithValue("@nome",      nome);
                query.Parameters.AddWithValue("@senha",     senha);
                query.ExecuteReader();
            }
            //tratamento de erro
            catch (Exception e)
            {
                //retorno caso der erro
                return false;
            }

            if (con.State == System.Data.ConnectionState.Open)
                con.Close();//fecha a conexão

            //retorno caso der certo
            return true;
        }

        /*╚▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╝*/

        /*╔►▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬ ♦ PERFIL DO FUNCIONARIO DO FORNECEDOR ♦ ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╗*/

        public static Fornecedor PerfilFuncionario(int codigo)
        {
            Fornecedor f = new Fornecedor();
            try
            {
                con.Open(); //ABRE CONEXÃO
                SqlCommand query = new SqlCommand("SELECT * FROM funcionario WHERE codigo = @codigo", con);
                query.Parameters.AddWithValue("@codigo", codigo);
                SqlDataReader leitor = query.ExecuteReader();

                if (leitor.Read())
                {
                    f.codigo      = int.Parse(leitor["codigo"].ToString());
                    f.nome        = leitor["nome"].ToString();
                    f.senha       = leitor["senha"].ToString();
                 
                }

            }
            catch (Exception e)
            {
                f = null;
            }
            if (con.State == ConnectionState.Open)
                con.Close(); //FECHA CONEXÃO

            return f;
        }
        /*╚▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬◄╝*/

    }//FIM DA CLASSE
}//FIM DO NAMESPACE



