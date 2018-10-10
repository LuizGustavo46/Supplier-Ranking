/*RESPONSÁVEL PELA CLASSE: MARCELO LEMOS 4INF- A TURMA - B*/
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
        private string rua;
        private string uf;
        private string imagem;
        private string senha;
        private string celular;
        private string endereco;
        private string posicao;
        private string descricao;
        private string cep;
        private float media;
        private string slogan;
        private string plano;
        private string nome_categoria;

        /*Variavel do funcionário*/
        private string codigo;
        private string nome;

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

        public String Rua
        {
            get { return rua; }
            set { rua = value; }
        }

        public String Uf
        {
            get { return uf; }
            set { uf = value; }
        }

        public String Imagem
        {
            get { return imagem; }
            set { imagem = value; }
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
        public String Posicao
        {
            get { return posicao; }
            set { posicao = value; }
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

        public String Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public String Nome
        {
            get { return nome; }
            set { nome = value; }
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
                    new SqlCommand("SELECT * FROM fornecedor WHERE Login = @cnpj AND Senha = @senha", con);
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
        public string CadastroFuncionario(string cnpj, string senha, string nome, int codigo)
        {
            string res = "Inserido com sucesso!";
            try
            {

                con.Open(); // abre conexão
                // Criação de comando
                SqlCommand query =
                    new SqlCommand("INSERT INTO funcionario VALUES (@nome,@codigo,@senha)", con);
                // Adiciona os parâmetros

                if (nome != "" && codigo.ToString() != "" && senha != "")
                {
                    query.Parameters.AddWithValue("@nome", nome);
                    query.Parameters.AddWithValue("@codigo", codigo);
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
        /*==============================================================================================================================================================================*/

        /*==============================================================================CADASTRO PESSOA JURIDICA========================================================================*/
        public string CadastroPessoaJuridica()
        {
            string res = "Inserido com sucesso!";
            try
            {
                con.Open(); // abre conexão
                // Criação de comando
                SqlCommand query =
                    new SqlCommand("INSERT INTO fornecedor VALUES (@cnpj,@nome_empresa,@email,@telefone@celular,@endereco,@bairro,@cidade,@uf,@cep,@senha,@slogan,@descricao,@media,@plano,@imagem,@nome_categoria)",
                        con);
                // Compara se todos os campos estao preenchidos corretamente, caso não esteja retorna uma mensagem de erro para o usuario 
                if (email != "" && telefone != "" && celular != "" && endereco != "" && bairro != "" && bairro != "" && cidade != "" && uf != ""
                    && cep != "" && slogan != "" && descricao != "" && descricao != "" && plano != "" && imagem != "" && nome_categoria != "")
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
                    new SqlCommand("SELECT * FROM fornecedor WHERE nome_empresa = @texto", con);
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
                    new SqlCommand("SELECT * FROM fornecedor", con);
                SqlDataReader leitor = query.ExecuteReader();

                while (leitor.Read())
                {
                    Fornecedor f = new Fornecedor();

                    f.Nome = leitor["Nome"].ToString();
                    f.Codigo = leitor["Codigio"].ToString();
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
                        new SqlCommand("UPDATE fornecedor SET  SENHA = @senha Where LOGIN = @login", con);
                    query.Parameters.AddWithValue("@senha", senha);
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
        internal string UpdateCadastroPessoaJuridica(string cnpj, string nome_empresa, string email, string telefone, string bairro, string cidade, string rua, string uf,
            string imagem, string senha, string celular, string endereco, string posicao, string descricao, string cep, float media, string slogan, string plano, string nome_categoria)
        
        {
            string res = "Salvo com sucesso!";

            /*PEDE-SE UMA CONFIRMAÇÃO DE SENHA PARA EDITAR AS INFORMAÇÕES DO FORNCEDOR
            PARA QUE TENHA UMA SEGURANÇA MAIOR*/

            //if (confirmaSenha == senha)
                try
                {
                    con.Open();
                    SqlCommand query =
                        new SqlCommand("UPDATE fornecedor SET " +
                        "@email,@telefone@celular,@endereco,@bairro,@cidade,@uf,@cep,@slogan,@descricao,@plano,@imagem,@nome_categoria)", con);

                    /*SE ALGUMA INFORMAÇÃO ESTIVER VAZIA O SISTEMA RETORNA UMA MENSAGEM DE AVISO PARA PREENCHER OS CAMPOS
                    CORRETAMENTE*/

                    if (email != "" && telefone != "" && celular != "" && endereco != "" && bairro != "" && bairro != "" && cidade != "" && uf != ""
                    && cep != "" && slogan != "" && descricao != "" && descricao != "" && plano != "" && imagem != "" && nome_categoria != "")
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
                        return "Preencha as informações corretamente ";
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

    }
}


    
