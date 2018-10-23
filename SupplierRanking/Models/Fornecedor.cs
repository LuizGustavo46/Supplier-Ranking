﻿/*RESPONSÁVEL PELA CLASSE: MARCELO LEMOS 4INF- A TURMA - B*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SupplierRanking.Models
{
    public class Fornecedor
    {

        //---* SETA O CAMINHO COM O BANCO DE DADOS *---
        private static SqlConnection con =
               new SqlConnection("Server=ESN509VMSSQL;Database=TCC_Laressa_Luiz_Marcelo_Valmir;User id=Aluno;Password=Senai1234");

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

        /*Variavel do funcionário*/
        private int codigo;
        private string nome;
        private string cnpj_fornecedor;

        private bool fornecedor;
        private bool consumidor;

        //---* PEGANDO E RETORNANDO VALORES INSERIDOS NAS VARIAVEIS *---
        public String Cnpj
        {
            get { return cnpj; }
            set { cnpj = value; }
        }

        public string Nome_empresa
        {
            get { return nome_empresa; }
            set { nome_empresa = value; }
        }

        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        public String Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }

        public String Cidade
        {
            get { return cidade; }
            set { cidade = value; }
        }

        public String Bairro
        {
            get { return bairro; }
            set { bairro = value; }

        }

        public String Uf
        {
            get { return uf; }
            set { uf = value; }
        }

        public byte[] Imagem
        {
            get { return imagem; }
            set { imagem = value; }
        }

        public String Imagem64
        {
            get { return imagem64; }
            set { imagem64 = value; }
        }

        public String Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        public String Celular
        {
            get { return celular; }
            set { celular = value; }
        }

        public String Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }

        public String Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public String Cep
        {
            get { return cep; }
            set { cep = value; }
        }

        public float Media
        {
            get { return media; }
            set { media = value; }
        }

        public String Slogan
        {
            get { return slogan; }
            set { slogan = value; }
        }

        public String Plano
        {
            get { return plano; }
            set { plano = value; }
        }

        public String Nome_categoria
        {
            get { return nome_categoria; }
            set { nome_categoria = value; }
        }

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public String Cnpj_fornecedor
        {
            get { return cnpj_fornecedor; }
            set { cnpj_fornecedor = value; }
        }

        //----------------------------INICIO DOS MÉTODOS--------------------------------

        /*RESPONSÁVEL PELA CLASSE: MARCELO LEMOS 4INF- A TURMA - B*/

        /*==============================================================================LOGAR COM O FORNECEDOR==============================================================================*/
        public bool Login()
        {
            bool res = false;

            try
            {
                con.Open();
                SqlCommand query =
                    new SqlCommand("SELECT * FROM fornecedor WHERE cnpj = @cnpj AND senha = @senha", con);
                query.Parameters.AddWithValue("@cnpj", cnpj);
                query.Parameters.AddWithValue("@senha", senha);
                SqlDataReader leitor = query.ExecuteReader();

                res = leitor.HasRows;
            }
            catch (Exception e)
            {
                res = false;// Caso der erro na inserção
            }

            if (con.State == ConnectionState.Open)
                con.Close();// fecha conexão
            return res;// retorna resposta de confirmação

        }

        /*=================================================================================================================================================================================*/

        /*=========================================================================CADASTRO FUNCIONARIO FORNECEDOR=========================================================================*/
        public string CadastroFuncionario(string cnpj, string senha, string nome)
        {
            string res = "Inserido com sucesso!";
            try
            {

                con.Open(); // abre conexão
                // Criação de comando
                SqlCommand query =
                    new SqlCommand("INSERT INTO funcionario VALUES (@cnpj,@nome,@senha)", con);
                // Adiciona os parâmetros

                if (cnpj != "" && nome != "" && senha != "")
                {
                    query.Parameters.AddWithValue("@cnpj", cnpj);
                    query.Parameters.AddWithValue("@nome", nome);
                    query.Parameters.AddWithValue("@senha", senha);
                    query.ExecuteNonQuery();
                }
                else
                {
                    res = "Preencha os campos corretamente";
                }

            }
            catch (Exception ex)
            {
                res = ex.Message; // Caso der erro na inserção
            }

            if (con.State == ConnectionState.Open)
                con.Close(); // fecha conexão

            return res; // retorna resposta de confirmação
        }

        public static Fornecedor ContadorCodigoFuncionario()
        {

            Fornecedor cf = new Fornecedor();
            try
            {
                con.Open();
                SqlCommand query =
                    new SqlCommand("select max(codigo) AS valor from funcionario", con);

                int codigo = 0;

                try
                {
                    codigo = Convert.ToInt32(query.ExecuteScalar());
                }
                catch (Exception exc) { }

                cf.codigo = codigo + 1;//método para contar o codigo do funcionario automaticamente
                cf.cnpj_fornecedor = "";
                cf.nome = "";               
                cf.senha = "";
              

            }
            catch (Exception e)
            {
                cf = null;
            }

            if (con.State == ConnectionState.Open)
                con.Close();

            return cf;
        }

        /*==============================================================================================================================================================================*/

        /*=========================================================================CADASTRO FUNCIONARIO FORNECEDOR======================================================================*/
        public bool LoginFuncionario()
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
                res = false;// Caso der erro na inserção
            }

            if (con.State == ConnectionState.Open)
                con.Close();// fecha conexão
            return res;// retorna resposta de confirmação

        }
        /*==============================================================================================================================================================================*/

        /*==============================================================================CADASTRO FORNECEDOR========================================================================*/
        public string CadastroFornecedor()
        {
            string res = "Cadastro realizado.";
            try
            {
                con.Open(); // abre conexão
                // Criação de comando
                SqlCommand query =
                    new SqlCommand("INSERT INTO fornecedor VALUES (@cnpj,@nome_empresa,@email,@telefone@celular,@endereco,@bairro,@cidade,@uf,@cep,@senha,@slogan,@descricao,@media,@plano,@imagem,@nome_categoria)",
                        con);
                // Compara se todos os campos estao preenchidos corretamente, caso não esteja retorna uma mensagem de erro para o usuario 
                if (email != "" && telefone != "" && celular != "" && endereco != "" && bairro != "" && bairro != "" && cidade != "" && uf != ""
                    && cep != "" && slogan != "" && descricao != "" && descricao != "" && plano != "" && imagem != null && nome_categoria != "")
                {
                    query.Parameters.AddWithValue("@cnpj", cnpj);
                    query.Parameters.AddWithValue("@nome_empresa", nome_empresa);
                    query.Parameters.AddWithValue("@email", email);
                    query.Parameters.AddWithValue("@telefone", telefone);
                    query.Parameters.AddWithValue("@celular", celular);
                    query.Parameters.AddWithValue("@endereco", endereco);
                    query.Parameters.AddWithValue("@bairro", bairro);
                    query.Parameters.AddWithValue("@cidade", cidade);
                    query.Parameters.AddWithValue("@uf", uf);
                    query.Parameters.AddWithValue("@cep", cep);
                    query.Parameters.AddWithValue("@senha", senha);
                    query.Parameters.AddWithValue("@slogan", slogan);
                    query.Parameters.AddWithValue("@descricao", descricao);
                    query.Parameters.AddWithValue("@media", media);
                    query.Parameters.AddWithValue("@plano", plano);
                    query.Parameters.AddWithValue("@imagem", imagem);
                    query.Parameters.AddWithValue("@nome_categoria", nome_categoria);            
                    query.ExecuteNonQuery();               
                }
                //Mensagem de erro
                else
                {
                    res = "Preencha os campos corretamente";
                }
            }
            catch (Exception ex)
            {
                res = ex.Message; // Caso der erro na inserção
            }

            if (con.State == ConnectionState.Open)
                con.Close(); // fecha conexão

            return res; // retorna resposta de confirmação
        }
        /*==============================================================================================================================================================================*/

        /*====================================================================EXCLUIR FUNCIONARIO DO FORNECEDOR=========================================================================*/
        public bool ExcluirFuncionario(string nome, int codigo, string nomeDigitado, int codigoDigitado)
        {
            try
            {
                con.Open();

                SqlCommand query =
                    new SqlCommand("DELETE FROM Funcionario WHERE codigo = @codigo AND nome = @nome",
                        con);
                //Comparando se o código digitado pelo FORNECEDOR é igual ao do funcionario correspondente no banco, se sim ele exclui
                if(codigo == codigoDigitado && nomeDigitado == nome)
                query.Parameters.AddWithValue("@codigo", codigo);
                query.Parameters.AddWithValue("@nome", nome);
                query.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }

            if (con.State == ConnectionState.Open)
                con.Close();

            return true;
        }
        /*==============================================================================================================================================================================*/

        /*==============================================================================BUSCA PESSOA JURIDICA===========================================================================*/
        public static List<Fornecedor> PesquisaFornecedor(string pesquisa)
        {
            List<Fornecedor> lista = new List<Fornecedor>();
            try
            {
                con.Open(); // abre conexão
                // Criação de comando

                SqlCommand query =
                    new SqlCommand("SELECT * FROM fornecedor WHERE nome_empresa like @texto", con);
                query.Parameters.AddWithValue("@texto", pesquisa);

                SqlDataReader leitor = query.ExecuteReader();

                Fornecedor f = new Fornecedor();
                while (leitor.Read())
                {

                    f.nome_empresa = leitor["nome_empresa"].ToString();
                    f.nome_categoria = leitor["nome_categoria"].ToString();
                    //colocar campo de posiçõ de ranking

                    lista.Add(f); // adiciona os valores cadastrados no banco à lista

                    //Compara se o nome digitado na barra de pesquisa é igual a alguma empresa (pessoa juridica) cadastrada no banco
                    if (pesquisa == f.nome_empresa)
                    {

                        f.Nome_empresa = f.nome_empresa;
                        f.Nome_categoria = f.nome_categoria;
                        lista.Add(f); // adiciona os valores cadastrados no banco à lista
                        
                    }
                }
            }
            catch (Exception ex)
            {
                lista = null;
            }

            if (con.State == ConnectionState.Open)
                con.Close();

            return lista;
        }
        /*==============================================================================================================================================================================*/

        /*==============================================================================RESTAURAR SENHA=================================================================================*/
        public Boolean RestaurarSenha(string novaSenha, string confirmarSenha, string cnpjDigitado)
        {
            bool res = false;
            try
            {
                //se a nova senha for igual ao campo com a nova senha e o cnpj digitado for igual ao cnpj do banco
                //a restauração é efetuada
                if (novaSenha == confirmarSenha && cnpj == cnpjDigitado)
                {
                    SqlCommand query =
                        new SqlCommand("SELECT email FROM fornecedor WHERE  cnpj = @cnpj", con);
                    query.Parameters.AddWithValue("@senha", senha);
                    SqlDataReader leitor = query.ExecuteReader();
                    while(leitor.Read())
                    {

                        email = leitor["email"].ToString();
                                
                    }
                    //CONFIGURANDO A MENSAGEM
                    MailMessage mail = new MailMessage();
                    //ORIGEM
                    mail.From = new MailAddress("supplierranking@hotmail.com");
                    //DESTINATÁRIO
                    mail.To.Add(email);
                    //ASSUNTO
                    mail.Subject = nome + "REDEFINIÇÃO DE SENHA - Supplier Ranking";
                    //CORPO DO E-MAIL
                    mail.Body = "NADA";//ESCREVER AQUI A MENSAGEM COM O LINK PARA A PAGINA DE REDEFINIÇÃO DE SENHA;


                    //CONFIGURAR O SMTP
                    SmtpClient smtpServer = new SmtpClient("smtp.live.com");
                    //CONFIGURAR PORTA
                    smtpServer.Port = 25;
                    //HABILITAR O TLS
                    smtpServer.EnableSsl = true;
                    //CONFIGURAR USUARIO E SENHA PARA LOGAR
                    smtpServer.Credentials = new System.Net.NetworkCredential("suportesupplierranking@hotmail.com", "Senai1234");
                    //ENVIAR
                    smtpServer.Send(mail);


                }

            }
            catch (Exception e)
            {
                res = false;
            }

            if (con.State == System.Data.ConnectionState.Open)
                con.Close();

            return res;

        }
        /*==============================================================================================================================================================================*/

        /*==============================================================================LISTA FUNCIONARIO===============================================================================*/
        public static List<Fornecedor> ListaFuncionario()
        {
            List<Fornecedor> listaFuncionario = new List<Fornecedor>();
            try
            {
                con.Open(); // abre conexão

                // Criação de comando
                SqlCommand query =
                    new SqlCommand("SELECT * FROM funcionario", con);
                SqlDataReader leitor = query.ExecuteReader();

                while (leitor.Read())
                {
                    Fornecedor f = new Fornecedor();

                    f.Nome = leitor["Nome"].ToString();
                    f.Codigo = int.Parse(leitor["Codigo"].ToString());
                    f.Senha = leitor["Senha"].ToString();
                    listaFuncionario.Add(f); // adiciona os valores cadastrados no banco à lista
                 
                }
            }
            catch (Exception ex)
            {
                listaFuncionario = null;
            }

            if (con.State == ConnectionState.Open)
                con.Close();

            return listaFuncionario;
        }
        /*==============================================================================================================================================================================*/


        /*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*MÉTODOS DE UPDATE-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*/
        

        /*=================================================================================UPDATE SENHA=================================================================================*/
        public Boolean UpdateSenha(string senha, string novaSenha, string senhaDigitada)
        {
            bool res = false;
            try
            {
                con.Open();

                /*--------------------------------------------------------------------------------------------------- 
                 SE A NOVA SENHA FOR DIFERENTE DA SENHA ANTIGA E A SENHA DIGITADA FOR IGUAL A SENHA ANTIGA 
                 ENTRA NO UPDATE DE SENHA
                 PARA TER UM CONTROLE MAIOR DE SEGURANÇA TER UMA FORMA DE VALIDAR SE QUEM ESTA ALTERANDO A SENHA É
                 O DONO DA CONTA MESMO
                 --------------------------------------------------------------------------------------------------*/
                /*RESPONSÁVEL PELA CLASSE: MARCELO LEMOS 4INF- A TURMA - B*/

                if (novaSenha != senha && senhaDigitada == senha)
                {
                    SqlCommand query =
                        new SqlCommand("UPDATE fornecedor SET  senha = @senha Where cnpj = @cnpj", con);
                    query.Parameters.AddWithValue("@senha", senha);
                    query.Parameters.AddWithValue("@cnpj", cnpj);
                    SqlDataReader leitor = query.ExecuteReader();
                    res = true;

                }

            }
            catch (Exception e)
            {
                res = false;
            }

            if (con.State == System.Data.ConnectionState.Open)
                con.Close();

            return res;
        }

        /*==============================================================================================================================================================================*/

        /*==============================================================================UPDATE CADASTRO=================================================================================*/
        internal string UpdateFornecedor() //NÃO PRECISA DE PARÂMETROS
        
        {
            string res = "Salvo com sucesso.";

            /*PEDE-SE UMA CONFIRMAÇÃO DE SENHA PARA EDITAR AS INFORMAÇÕES DO FORNCEDOR
            PARA QUE TENHA UMA SEGURANÇA MAIOR*/

            //if (confirmaSenha == senha)
                try
                {
                    con.Open();
                    SqlCommand query =
                        new SqlCommand("UPDATE fornecedor SET email = @email, telefone = @telefone, celular = @celular, endereco = @endereco, " +
                        "bairro = @bairro, cidade = @cidade, uf = @uf, cep = @cep, slogan = @slogan, descricao = @descricao, imagem = @imagem, " +
                        "nome_categorias = @nome_categorias WHERE cnpj = @cnpj", con);

                    /*SE ALGUMA INFORMAÇÃO ESTIVER VAZIA O SISTEMA RETORNA UMA MENSAGEM DE AVISO PARA PREENCHER OS CAMPOS
                    CORRETAMENTE*/

                    if (email != "" && telefone != "" && celular != "" && endereco != "" && bairro != "" && bairro != "" && cidade != "" && uf != ""
                    && cep != "" && slogan != "" && descricao != "" && descricao != "" && imagem != null && nome_categoria != "")
                    {
                        query.Parameters.AddWithValue("@email", email);
                        query.Parameters.AddWithValue("@telefone", telefone);
                        query.Parameters.AddWithValue("@celular", celular);
                        query.Parameters.AddWithValue("@endereco", endereco);
                        query.Parameters.AddWithValue("@bairro", bairro);
                        query.Parameters.AddWithValue("@cidade", cidade);
                        query.Parameters.AddWithValue("@uf", uf);
                        query.Parameters.AddWithValue("@cep", cep);
                        query.Parameters.AddWithValue("@slogan", slogan);
                        query.Parameters.AddWithValue("@descricao", descricao);
                        query.Parameters.AddWithValue("@plano", plano);
                        query.Parameters.AddWithValue("@imagem", imagem);
                        query.Parameters.AddWithValue("@nome_categoria", nome_categoria);
                        query.ExecuteNonQuery();
                    }
                    else
                    {
                        return "Preencha as informações corretamente.";
                    }
                }

                catch (Exception e)
                {
                    res = e.Message;
                }

            if (con.State == System.Data.ConnectionState.Open)
                con.Close();

            return res;
        }


        /*==============================================================================================================================================================================*/

        /*======================================== MÉTODO PARA RETORNAR DADOS DO FORNECEDOR (PERFIL) =====================================================================================================*/
        public static Fornecedor Perfil(string cnpj)
        {
            Fornecedor f = new Fornecedor();
            try
            {
                con.Open(); //ABRE CONEXÃO
                SqlCommand query = new SqlCommand("SELECT * FROM fornecedor WHERE cnpj = @cnpj", con);
                query.Parameters.AddWithValue("@cnpj", cnpj);
                SqlDataReader leitor = query.ExecuteReader();

                if (leitor.Read())
                {
                    f.cnpj = leitor["Cnpj"].ToString();
                    f.nome_empresa = leitor["Nome_empresa"].ToString();
                    f.email = leitor["Email"].ToString();
                    f.telefone = leitor["Telefone"].ToString();
                    f.celular = leitor["Celular"].ToString();
                    f.endereco = leitor["Endereco"].ToString();
                    f.bairro = leitor["Bairro"].ToString();
                    f.cidade = leitor["Cidade"].ToString();
                    f.uf = leitor["Uf"].ToString();
                    f.cep = leitor["Cep"].ToString();
                    f.slogan = leitor["Slogan"].ToString();
                    f.descricao = leitor["Descricao"].ToString();
                    f.media = float.Parse(leitor["Media"].ToString());
                    f.plano = leitor["Plano"].ToString();
                    f.imagem = (byte[])leitor["Imagem"];
                    f.nome_categoria = leitor["Nome_categorias"].ToString();
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

    }//FIM DA CLASSE
}//FIM DO NAMESPACE



