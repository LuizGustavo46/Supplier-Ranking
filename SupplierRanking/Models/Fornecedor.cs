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

        //*==================================================TORNANDO AS VARIAVEIS ACESSIVEIS=======================================================================*
        public String Cnpj
        { get{ return cnpj; }                      set{ cnpj = value; } }

        public string Nome_empresa
        { get{ return nome_empresa;}               set { nome_empresa = value; } }

        public String Email
        { get{ return email; }                     set { email = value; } }

        public String Telefone
        { get{ return telefone;}                   set { telefone = value; } }

        public String Cidade
        { get{ return cidade; }                    set { cidade = value; } }

        public String Bairro
        { get{ return bairro; }                    set { bairro = value; } }

        public String Uf
        { get{ return uf; }                        set { uf = value; } }

        public byte[] Imagem
        { get { return imagem; }                   set { imagem = value; } }

        public String Imagem64
        { get { return imagem64; }                 set { imagem64 = value; } }

        public String Senha
        { get { return senha; }                    set { senha = value; } }

        public String Celular
        { get { return celular; }                  set { celular = value; } }

        public String Endereco
        { get { return endereco; }                 set { endereco = value; } }

        public String Descricao
        { get { return descricao; }                set { descricao = value; } }

        public String Cep
        { get { return cep; }                      set { cep = value; }}

        public float Media
        { get { return media; }                    set { media = value; } }

        public String Slogan
        {get { return slogan; }                    set { slogan = value; } }

        public String Plano
        { get { return plano; }                    set { plano = value; } }

        public String Nome_categoria
        { get { return nome_categoria; }           set { nome_categoria = value; } }

        public int Codigo
        { get { return codigo; }                   set { codigo = value; }}

        public String Nome
        { get { return nome; }                     set { nome = value; }}

        public String Cnpj_fornecedor
        {
            get { return cnpj_fornecedor; }
            set { cnpj_fornecedor = value; }
        }
        //=========================================================================================================================================================*/


        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++INICIO DOS MÉTODOS+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /*RESPONSÁVEL PELA CLASSE: MARCELO LEMOS 4INF- A TURMA - B*/

        /*===================================================================LOGAR COM O FORNECEDOR================================================================*/
        public bool Login() //FEITO
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
                // Caso der erro na inserção
                res = false;
            }

            if (con.State == ConnectionState.Open)
                con.Close();// fecha conexão
            return res;// retorna resposta de confirmação

        }

        /*===========================================================================================================================================================================*/

        /*================================================================CADASTRO FUNCIONARIO FORNECEDOR============================================================================*/
        public string CadastroFuncionario(string cnpj, string senha, string nome) // NAO FEITO
        {
            string res = "Inserido com sucesso!";
            try
            {
                //ABRE A CONEXÃO
                con.Open();

                // Criação de comando para inserção no banco
                SqlCommand query =
                    new SqlCommand("INSERT INTO funcionario VALUES (@cnpj,@nome,@senha)", con);

                //CONDIÇÃO DE CADASTRO (NÃO DEIXA QUE FALTE CAMPOS NECESSARIOS PARA O CADASTRO
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

            // retorna resposta de confirmação
            return res; 
        }
        /*===========================================================================================================================================================================*/

        /*=========================================================================LOGIN FUNCIONARIO FORNECEDOR======================================================================*/
        public bool LoginFuncionario() //NAO FEITO 
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
        /*===========================================================================================================================================================================*/

        /*==============================================================================CADASTRO FORNECEDOR==========================================================================*/
        public string CadastroFornecedor() //FEITO
        {
            string res = "Cadastro realizado.";
            try
            {   
                // abre conexão
                con.Open();
                 
                // Criação de comando para inserção de dados na tabela fornecedor
                SqlCommand query =
                    new SqlCommand("INSERT INTO fornecedor VALUES (@cnpj,@nome_empresa,@email,@telefone,@celular,@endereco,@bairro,@cidade,@uf,@cep,@senha,@slogan,@descricao,@media,@plano,@imagem,@nome_categoria)",
                        con);

                // Compara se todos os campos estao preenchidos corretamente, caso não esteja retorna uma mensagem de erro para o usuario 
                if (email != "" && telefone != "" && celular != "" && endereco != "" && bairro != "" && bairro != "" && cidade != "" && uf != ""
                    && cep != "" && slogan != "" && descricao != "" && descricao != "" && plano != "" && nome_categoria != "")
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
        /*===========================================================================================================================================================================*/

        /*====================================================================EXCLUIR FUNCIONARIO DO FORNECEDOR======================================================================*/
        public bool ExcluirFuncionario(string nome, int codigo, string nomeDigitado, int codigoDigitado) //TRAVADO PELA HOME LOGADA
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
            //tratamento de erro
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

                Fornecedor f = new Fornecedor();

                //prepara o leitor
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

            //tratamento de erro
            catch (Exception ex)
            {
                lista = null;
            }

            if (con.State == ConnectionState.Open)
                con.Close();//fecha a conexao

            return lista;
        }
      
        /*===========================================================================================================================================================================*/

        /*==============================================================================RESTAURAR SENHA==============================================================================*/
        public Boolean RestaurarSenha(string cnpj) //FEITO
        {
            bool res = false;
            try
            {
                con.Open(); // abre conexão
                SqlCommand query =
                        new SqlCommand("SELECT email FROM fornecedor WHERE  cnpj = @cnpj", con);
                    query.Parameters.AddWithValue("@cnpj", cnpj);
                    SqlDataReader leitor = query.ExecuteReader();

                while (leitor.Read())
                {
                    email = leitor["email"].ToString();
                }
                    //CONFIGURANDO A MENSAGEM
                    MailMessage mail = new MailMessage();
                    //ORIGEM
                    mail.From = new MailAddress("marcelolemos7@outlook.com");//supplierranking@hotmail.com
                    //DESTINATÁRIO
                    mail.To.Add(email);
                    //ASSUNTO
                    mail.Subject = nome + "REDEFINIÇÃO DE SENHA - Supplier Ranking";
                    //CORPO DO E-MAIL
                    //ESCREVER AQUI A MENSAGEM COM O LINK PARA A PAGINA DE REDEFINIÇÃO DE SENHA.
                    mail.Body = "Clique no link   http://localhost:16962/Fornecedor/RestaurarSenha   para redefinir sua senha";  


                    //CONFIGURAR O SMTP
                    SmtpClient smtpServer = new SmtpClient("smtp.live.com");
                    //CONFIGURAR PORTA
                    smtpServer.Port = 25;
                    //HABILITAR O TLS
                    smtpServer.EnableSsl = true;
                    //CONFIGURAR USUARIO E SENHA PARA LOGAR
                    smtpServer.Credentials = new System.Net.NetworkCredential("suportesupplierranking@hotmail.com", "SEnai12344");
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



        /*==============================================================================================================================================================================*/

        /*==============================================================================LISTA FUNCIONARIO===============================================================================*/
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

                    f.Codigo = int.Parse(leitor["Codigo"].ToString());
                    f.Nome = leitor["Nome"].ToString();
                    f.Senha = leitor["Senha"].ToString();
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

        /* CASO PRECISE É SO CRIAR A VIEW
        public static List<Fornecedor> ListaFornecedor()
        {
            List<Fornecedor> listaFornecedor = new List<Fornecedor>(); 
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
            //tratamento de erro
            catch (Exception ex)
            {
                listaFornecedor = null;
            }

            if (con.State == ConnectionState.Open)
                con.Close();//fecha a conexao

            return listaFornecedor;
        }


        */



        /*==============================================================================================================================================================================*/


        /*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*MÉTODOS DE UPDATE-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*/


        /*=================================================================================UPDATE SENHA=================================================================================*/
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
                query1.Parameters.AddWithValue("@cnpj", cnpj);//seleciona o perfil do fornecedor no banco através do cnpj
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
                    query.Parameters.AddWithValue("@cnpj", cnpj);
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

        /*==============================================================================================================================================================================*/

        /*==============================================================================UPDATE CADASTRO=================================================================================*/
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
                    query.Parameters.AddWithValue("@cnpj", cnpj);
                    query.Parameters.AddWithValue("@nome_empresa", nome_empresa);
                    query.Parameters.AddWithValue("@email", email);
                    query.Parameters.AddWithValue("@endereco", endereco);
                    query.Parameters.AddWithValue("@bairro", bairro);
                    query.Parameters.AddWithValue("@cidade", cidade);
                    query.Parameters.AddWithValue("@uf", uf);
                    query.Parameters.AddWithValue("@cep", cep);
                    query.Parameters.AddWithValue("@telefone", telefone);
                    query.Parameters.AddWithValue("@celular", celular);
                    query.Parameters.AddWithValue("@descricao", descricao);
                    query.Parameters.AddWithValue("@slogan", slogan);
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


        /*==============================================================================================================================================================================*/

        /*======================================== MÉTODO PARA RETORNAR DADOS DO FORNECEDOR (PERFIL) ===================================================================================*/
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
            //tratamento de erro
            catch (Exception e)
            {
                f = null;
            }
            if (con.State == ConnectionState.Open)
                con.Close(); //FECHA CONEXÃO

            return f;
        }
        /*==============================================================================================================================================================================*/

        /*======================================== EDITAR DADOS DO FUNCIONARIO DO FORNECEDOR ===========================================================================================*/


        public bool  UpdateFuncionarioFornecedor(int codigo, string nome, string senha) //FEITO
        {          
            try
            {
                con.Open(); //ABRE CONEXÃO
                SqlCommand query = new SqlCommand("UPDATE funcionario SET nome = @nome, senha = @senha WHERE codigo = @codigo", con);
                query.Parameters.AddWithValue("@codigo", codigo);
                query.Parameters.AddWithValue("@nome", nome);
                query.Parameters.AddWithValue("@senha", senha);
                query.ExecuteReader();
            }

            catch (Exception e)
            {
                return false;
            }

            if (con.State == System.Data.ConnectionState.Open)
                con.Close();

            return true;
        }


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
                    f.codigo = int.Parse(leitor["codigo"].ToString());
                    f.nome = leitor["nome"].ToString();
                    f.senha = leitor["senha"].ToString();
                 
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



