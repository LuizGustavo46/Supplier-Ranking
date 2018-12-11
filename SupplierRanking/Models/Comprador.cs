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
    public class Comprador
    {
        //CONEXÃO COM O BANCO DE DADOS - SE FOR USAR EM CASA É SÓ TROCAR "SENAI" PARA O SEU NOME
        private static SqlConnection con =
            new SqlConnection(ConfigurationManager.ConnectionStrings["VALMIR"].ConnectionString);

        //CAMPOS DO BANCO DE DADOS (TODOS OS DADOS DE CADASTRO)
        private int     codigo;
        private string  cpf;
        private string  nome;
        private string  sobrenome;
        private string  email;
        private string  tipo_pessoa;
        private string  senha;
        private string  cnpj;
        private string  nome_empresa;   
        private string  uf;
        private string  telefone;
        private string  celular;

        //Varáveis úteis
        int codigoEmail;

        public int Codigo               { get { return codigo; }          set { codigo = value; }}
        public string Cpf               { get { return cpf; }             set { cpf = value; }}
        public string Nome              { get { return nome; }            set { nome = value; }}
        public string Sobrenome         { get { return sobrenome; }       set { sobrenome = value; }}
        public string Email             { get { return email; }           set { email = value; }}
        public string Tipo_pessoa       { get { return tipo_pessoa; }     set { tipo_pessoa = value; }}
        public string Senha             { get { return senha; }           set { senha = value; }}
        public string Cnpj              { get { return cnpj; }            set { cnpj = value; }}
        public string Nome_empresa      { get { return nome_empresa; }    set { nome_empresa = value; }}
        public string Uf                { get { return uf; }              set { uf = value; }}
        public string Telefone          { get { return telefone; }        set { telefone = value; }}
        public string Celular           { get { return celular; }         set { celular = value; }}

        public object ViewBag { get; private set; }

        /******************************************************** LOGIN PESSOA FISICA ***********************************************/

        public bool LoginPessoaFisica()
        {
            bool res = false;
            try
            {
                con.Open();
                SqlCommand query =
                    new SqlCommand("SELECT * FROM comprador WHERE cpf = @cpf AND senha = @senha", con);
                query.Parameters.AddWithValue("@cpf", cpf);
                query.Parameters.AddWithValue("@senha", senha);
                SqlDataReader leitor = query.ExecuteReader();
                res = leitor.HasRows;

            }catch (Exception e) { res = false; }

            if (con.State == ConnectionState.Open)
                con.Close();
            return res; //RETORNA TRUE OR FALSE
        }

        /********************************************************** LOGIN PESSOA JURIDICA ******************************************/

        public bool LoginPessoaJuridica()
        {
            bool res = false;
            try
            {
                con.Open();
                SqlCommand query =
                    new SqlCommand("SELECT * FROM comprador WHERE cnpj = @cnpj AND senha = @senha", con);
                query.Parameters.AddWithValue("@cnpj", cnpj);
                query.Parameters.AddWithValue("@senha", senha);
                SqlDataReader leitor = query.ExecuteReader();
                res = leitor.HasRows;

            }catch (Exception e) { res = false; }

            if (con.State == ConnectionState.Open)
                con.Close();
            return res; //RETORNA TRUE OR FALSE
        }

        /***************************************************** CADASTRO PESSOA FISICCA ******************************************/

        public bool CadastroPessoaFisica()
        {
            try
            {
                con.Open(); //ABRE CONEXÃO
                //COMANDO PARA TESTAR SE JA EXISTE O CPF CADASTRADO
                SqlCommand query =
                    new SqlCommand("SELECT cpf, email FROM comprador WHERE cpf = @cpf OR email = @email;", con);
                query.Parameters.AddWithValue("@cpf", cpf);
                query.Parameters.AddWithValue("@email", email);
                SqlDataReader leitor = query.ExecuteReader();

                if (!leitor.Read())
                {
                    leitor.Close();
                    //INSERIR CADASTRO NO BANCO
                    SqlCommand queryInsert =
                    new SqlCommand("INSERT INTO comprador VALUES (@cpf,@nome,@sobrenome,@email,@senha,@tipo_pessoa," +
                    "@cnpj,@nome_empresa,@uf,@telefone,@celular)", con);
                    //CONDIÇÃO PARA EFETUAR O CADASTRO
                    if (cpf.Length == 14 && senha.Length >= 5 && nome.Length >= 3 && email.Length >= 8 &&
                    (telefone.Length == 14 || telefone.Length == 0) && (celular.Length == 15 || celular.Length == 0)) 
                    {
                        //ADICIONA OS PARÂMETROS --- NÃO PRECISA PASSAR O CAMPO CODIGO, ELE GERA AUTOMATICAMENTE NO BANCO
                        queryInsert.Parameters.AddWithValue("@cpf",             cpf);
                        queryInsert.Parameters.AddWithValue("@nome",            nome);
                        queryInsert.Parameters.AddWithValue("@sobrenome",       sobrenome);
                        queryInsert.Parameters.AddWithValue("@email",           email);
                        queryInsert.Parameters.AddWithValue("@tipo_pessoa",     tipo_pessoa);
                        queryInsert.Parameters.AddWithValue("@senha",           senha);
                        queryInsert.Parameters.AddWithValue("@cnpj",            cnpj);
                        queryInsert.Parameters.AddWithValue("@nome_empresa",    nome_empresa);
                        queryInsert.Parameters.AddWithValue("@uf",              uf);
                        queryInsert.Parameters.AddWithValue("@telefone",        telefone);
                        queryInsert.Parameters.AddWithValue("@celular",         celular);
                        queryInsert.ExecuteNonQuery();
                    }else{ return false; }
                }else { return false; }
                
            }catch (Exception ex) { return false; } 
       
            if (con.State == ConnectionState.Open)
                con.Close(); //FECHA CONEXÃO
            return true; //RETORNA RESPOSTA DE CONFIRMAÇÃO
        }
    
        /******************************************************* CADASTRO PESSOA JURIDICA *******************************************/

        public bool CadastroPessoaJuridica()
        {
            bool res = true;
            try
            {
                con.Open(); //ABRE CONEXÃO
                //COMANDO PARA TESTAR SE JA EXISTE O CNPJ CADASTRADO
                SqlCommand query =
                    new SqlCommand("SELECT cnpj, email FROM comprador WHERE cnpj = @cnpj OR email = @email;", con);
                query.Parameters.AddWithValue("@cnpj", cnpj);
                query.Parameters.AddWithValue("@email", email);
                SqlDataReader leitor = query.ExecuteReader();

                if (!leitor.Read())
                {
                    leitor.Close();
                    //CRIAÇÃO DE COMANDO
                    SqlCommand queryInsert =
                        new SqlCommand("INSERT INTO comprador VALUES (@cpf,@nome,@sobrenome,@email,@senha,@tipo_pessoa," +
                        "@cnpj,@nome_empresa,@uf,@telefone,@celular)", con);
                        //CONDIÇÃO PARA EFETUAR O CADASTRO
                    if (cnpj.Length == 18 && senha.Length >= 5 && nome_empresa.Length >= 1 && email.Length >= 4 &&
                        (telefone.Length == 14 || telefone.Length == 0) && (celular.Length == 15 || celular.Length == 0) && uf.Length == 2 )
                    {
                        //ADICIONA OS PARÂMETROS --- NÃO PRECISA PASSAR O CAMPO CODIGO, ELE GERA AUTOMATICAMENTE NO BANCO
                        queryInsert.Parameters.AddWithValue("@cpf",           cpf);
                        queryInsert.Parameters.AddWithValue("@nome",          nome);
                        queryInsert.Parameters.AddWithValue("@sobrenome",     sobrenome);
                        queryInsert.Parameters.AddWithValue("@email",         email);
                        queryInsert.Parameters.AddWithValue("@senha",         senha);
                        queryInsert.Parameters.AddWithValue("@tipo_pessoa",   tipo_pessoa);
                        queryInsert.Parameters.AddWithValue("@cnpj",          cnpj);
                        queryInsert.Parameters.AddWithValue("@nome_empresa",  nome_empresa);
                        queryInsert.Parameters.AddWithValue("@uf",            uf);
                        queryInsert.Parameters.AddWithValue("@telefone",      telefone);
                        queryInsert.Parameters.AddWithValue("@celular",       celular);
                        queryInsert.ExecuteNonQuery();
                    }else{ res = false; }
                }else { res = false; }
            } catch (Exception ex) { res = false; }

            if (con.State == ConnectionState.Open)
                con.Close(); //FECHA CONEXÃO
            return res; //RETORNA RESPOSTA DE CONFIRMAÇÃO
        }

        /***************************************** CADASTRAR CATEGORIAS DE INTERESSE ********************************************/
        public bool CadastrarInteresses(List<Categorias> listaCaterogias)
        {
            try
            {
                con.Open();
                if (cpf != "")
                {
                    SqlCommand query =
                        new SqlCommand("SELECT codigo FROM comprador WHERE cpf = @cpf;", con);
                    query.Parameters.AddWithValue("@cpf", cpf);
                    SqlDataReader leitor = query.ExecuteReader();

                    if (leitor.Read())           
                        codigo = int.Parse(leitor["codigo"].ToString());
                    leitor.Close();         
                }
                else if(cnpj != "")
                {
                    SqlCommand query =
                        new SqlCommand("SELECT codigo FROM comprador WHERE cnpj = @cnpj;", con);
                    query.Parameters.AddWithValue("@cnpj", cnpj);
                    SqlDataReader leitor = query.ExecuteReader();
                    if (leitor.Read())
                        codigo = int.Parse(leitor["codigo"].ToString());
                    leitor.Close();
                }

                for(int i = 0; i < listaCaterogias.Count; i++) //ARRUMAR O INDICE DE PERCORRER A LISTA
                {
                    SqlCommand queryInsert =
                    new SqlCommand("INSERT INTO categorias_comprador VALUES (@nome_categorias,@codigo_comprador)", con);
                    queryInsert.Parameters.AddWithValue("@nome_categorias", listaCaterogias[i].Categoria);
                    queryInsert.Parameters.AddWithValue("@codigo_comprador", codigo);
                    queryInsert.ExecuteNonQuery();
                }

                

            } catch (Exception ex) { return false; }

            if (con.State == ConnectionState.Open)
                con.Close(); //FECHA CONEXÃO
            return true;
        }

        /********************************************************** BUSCA PESSOA ************************************************/

        public static Comprador BuscaPessoa(int codigo) //BUSCAR USUARIO PARA MOSTRAR NA PAGINA DE UPDATE
        {
            Comprador bp = new Comprador();
            try
            {
                con.Open(); //ABRE CONEXÃO
                SqlCommand query =
                    new SqlCommand("SELECT * FROM comprador WHERE codigo = @codigo", con);
                query.Parameters.AddWithValue("@codigo", codigo);
                SqlDataReader leitor = query.ExecuteReader();

                while (leitor.Read()) //ENQUANTO O LEITOR LER
                {
                    bp.codigo       = int.Parse(leitor["codigo"].ToString());
                    bp.cpf          = leitor["cpf"].ToString();
                    bp.nome         = leitor["nome"].ToString();
                    bp.sobrenome    = leitor["sobrenome"].ToString();
                    bp.email        = leitor["email"].ToString();
                    bp.senha        = leitor["senha"].ToString();
                    bp.tipo_pessoa  = leitor["tipo_pessoa"].ToString();
                    bp.cnpj         = leitor["cnpj"].ToString();
                    bp.nome_empresa = leitor["nome_empresa"].ToString();
                    bp.uf           = leitor["uf"].ToString();
                    bp.telefone     = leitor["telefone"].ToString();
                    bp.celular      = leitor["celular"].ToString();
                }
            }catch (Exception e) { bp = null; }
 
            if (con.State == ConnectionState.Open)
                con.Close();
            return bp;
        }

        /******************************************************** UPDATE PESSOA FISICA **********************************************/

        internal bool UpdatePessoaFisica()
        {
            bool res = true;
            try
            {
                con.Open();
                SqlCommand query =
                    new SqlCommand("UPDATE comprador SET nome = @nome, sobrenome = @sobrenome, email = @email," +
                    "uf = @uf, telefone = @telefone, celular = @celular WHERE codigo = @codigo", con);
                if (nome.Length >= 1 && sobrenome.Length >= 1 && email.Length >= 4 && (telefone.Length == 14 || telefone.Length == 0)
                    && (celular.Length == 15 || celular.Length == 0))
                {
                    query.Parameters.AddWithValue("@codigo",        codigo);
                    query.Parameters.AddWithValue("@nome",          nome);
                    query.Parameters.AddWithValue("@sobrenome",     sobrenome);
                    query.Parameters.AddWithValue("@email",         email);
                    query.Parameters.AddWithValue("@uf",            uf);
                    query.Parameters.AddWithValue("@telefone",      telefone);
                    query.Parameters.AddWithValue("@celular",       celular);
                    query.ExecuteNonQuery(); //EXECUTA

                }else{ res = false; }
   
            }catch (Exception e) { res = false; }

            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            return res;
        }

        /******************************************************** UPDATE PESSOA JURIDICA ********************************************/

        internal bool UpdatePessoaJuridica()
        {
            bool res = true;
            try
            {
                con.Open(); //ABRE CONEXÃO
                //CRIAÇÃO DE COMANDO
                SqlCommand query =
                    new SqlCommand("UPDATE comprador SET nome_empresa = @nome_empresa, email = @email, uf = @uf, telefone = @telefone," + 
                    "celular = @celular WHERE codigo = @codigo", con);
                if (nome_empresa.Length >= 1 && email.Length >= 8 && (telefone.Length == 14 || telefone.Length == 0) &&
                    (celular.Length == 15 || celular.Length == 0) && uf.Length == 2)
                {
                    query.Parameters.AddWithValue("@codigo",        codigo);
                    query.Parameters.AddWithValue("@nome_empresa",  nome_empresa);
                    query.Parameters.AddWithValue("@email",         email);
                    query.Parameters.AddWithValue("@uf",            uf);
                    query.Parameters.AddWithValue("@telefone",      telefone);
                    query.Parameters.AddWithValue("@celular",       celular);
                    query.ExecuteNonQuery(); //EXECUTA

                }else{ res = false; }
    
            }catch (Exception e) { res = false; }

            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            return res;
        }

        /******************************************************** EXCLUIR CONTA **************************************************/    
        
        public bool ExcluirConta(int codigo, string senha) //(COMPRADOR EXCLUIR SUA PRÓPRIA CONTA) - FALTA TESTAR
        {
            try
            {
                con.Open(); //ABRE CONEXÃO
                //CRIAÇÃO DE COMANDO
                SqlCommand query =
                    new SqlCommand("DELETE FROM comprador WHERE codigo = @codigo AND senha = @senha",
                        con);
                query.Parameters.AddWithValue("@codigo", codigo);
                query.Parameters.AddWithValue("@senha", senha);
                query.ExecuteNonQuery();

            }catch (Exception ex) { return false; }

            if (con.State == ConnectionState.Open)
                con.Close();
            return true;
        }

        /******************************************************** UPDATE SENHA ***************************************************/

        public bool UpdateSenha(string senhaAtual, string senhaNova, string confirmaSenhaNova) //ALTERAR SENHA
        {
            string senhaUsada = ""; //VAR PARA GUARDAR A SENHA QUE VAI VIR DO BANCO
            try
            {
                con.Open(); //ABRE CONEXÃO
                SqlCommand query =
                    new SqlCommand("SELECT senha FROM comprador WHERE codigo = @codigo", con);
                query.Parameters.AddWithValue("@codigo", codigo);
                SqlDataReader leitor = query.ExecuteReader(); //EXECUTA O COMANDO COM UM READER

                if(leitor.Read()) //ENQUANTO O LEITOR CONSEGUIR LER 
                {
                    senhaUsada = leitor["senha"].ToString(); //GUARDA A SENHA QUE VEIO DO BANCO
                    leitor.Close();
                }

                //SE O COMPRADOR QUISER APENAS TROCAR A SENHA (UPDATE SENHA)
                //CONDIÇÃO PARA EFETUAR O UPDATE SENHA
                if (senhaAtual == senhaUsada && senhaNova == confirmaSenhaNova && senhaNova != senhaAtual)
                {   
                    SqlCommand querySenha =
                    new SqlCommand("UPDATE comprador SET senha = @senha WHERE codigo = @codigo", con);
                    querySenha.Parameters.AddWithValue("@senha", senhaNova);
                    querySenha.Parameters.AddWithValue("@codigo", codigo);
                    querySenha.ExecuteNonQuery(); //EXECUTE
                }

                //SE O COMPRADOR ESQUECEU A SENHA (RESTAURAR SENHA)
                if(senhaAtual== null && senhaNova == confirmaSenhaNova && senhaNova != senhaUsada)
                {
                    SqlCommand querySenha =
                    new SqlCommand("UPDATE comprador SET senha = @senha WHERE codigo = @codigo", con);
                    querySenha.Parameters.AddWithValue("@senha", senhaNova);
                    querySenha.Parameters.AddWithValue("@codigo", codigo);
                    querySenha.ExecuteNonQuery(); //EXECUTE
                }

            }catch (Exception ex) { return false; }

            if (con.State == ConnectionState.Open)
                con.Close();
            return true; 
        }

        /******************************************************* RESTAURAR SENHA **************************************************/

        public bool EsqueceuSuaSenha(string cnpj, string cpf, string email) //RESTAURAR (ESQUECEU SUA SENHA
        {
            try
            {
                con.Open(); //ABRE CONEXÃO

                if (cpf != "") //SE CPF FOR DIFERENTE DE NULL É PESSOA FISICA
                { 
                    SqlCommand query =
                        new SqlCommand("SELECT email, codigo FROM comprador WHERE cpf = @cpf", con);
                    query.Parameters.AddWithValue("@cpf", cpf);
                    SqlDataReader leitor = query.ExecuteReader();
                    while(leitor.Read()) //ENQUANTO O LEITOR CONSEGUIR LER
                    {
                        email   =   leitor["email"].ToString();
                        codigo  =   int.Parse(leitor["codigo"].ToString());
                    }
                }
                else if(cnpj != "") //SENÃO FOR DIFERENTE É PESSOA JURIDICA
                {
                    SqlCommand query =
                        new SqlCommand("SELECT email, codigo FROM comprador WHERE cnpj = @cnpj", con);
                    query.Parameters.AddWithValue("@cnpj", cnpj);
                    SqlDataReader leitor = query.ExecuteReader();
                    while(leitor.Read())
                    {
                        email   =   leitor["email"].ToString();
                        codigo  =   int.Parse(leitor["codigo"].ToString());
                    }
                }
                else if(email != "") //SE O CPF E O CNPJ NÃO FOREM DIGITADOS, SERÁ O PRÓPRIO EMAIL
                {
                    SqlCommand query =
                        new SqlCommand("SELECT email, codigo FROM comprador WHERE email = @email", con);
                    query.Parameters.AddWithValue("@email", email);
                    SqlDataReader leitor = query.ExecuteReader();
                    while (leitor.Read())
                    {
                        email = leitor["email"].ToString();
                        codigo = int.Parse(leitor["codigo"].ToString());
                    }
                }

                //CONFIGURANDO A MENSAGEM
                MailMessage mail = new MailMessage();
                //ORIGEM
                mail.From = new MailAddress("officialsranking@outlook.com");
                //DESTINATÁRIO
                mail.To.Add(email);
                //ASSUNTO
                mail.Subject = "REDEFINIÇÃO DE SENHA - Supplier Ranking";
                //CORPO DO E-MAIL
                mail.Body = "USER ID: " + codigo + "\nClique aqui para redefinir sua senha:\n" +
                                                    "http://localhost:16962/Comprador/NovaSenha";
               
                
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

            }catch (Exception ex) { return false; }
 
            if (con.State == ConnectionState.Open)
                con.Close();
            return true;
        }

        /******************************************************* PEGAR NOME COMPRADOR **************************************************/

        public Comprador DadosPerfil(string cpf_cnpj) //FEITO
        {
            Comprador c = new Comprador();
            try
            {
                con.Open(); //ABRE CONEXÃO

                    SqlCommand query = new SqlCommand("SELECT nome, codigo FROM comprador WHERE cpf = @cpf", con);
                query.Parameters.AddWithValue("@cpf", cpf_cnpj);
                SqlDataReader leitor = query.ExecuteReader();

                if (leitor.Read())
                {
                    c.Nome = leitor["Nome"].ToString();
                    c.Codigo = int.Parse(leitor["Codigo"].ToString());
                }
                else
                {
                    leitor.Close();

                    SqlCommand query2 = new SqlCommand("SELECT nome_empresa, codigo FROM comprador WHERE cnpj = @cnpj", con);
                    query2.Parameters.AddWithValue("@cnpj", cpf_cnpj);
                    leitor = query2.ExecuteReader();
                    if (leitor.Read())
                    {
                        c.Nome_empresa = leitor["Nome_empresa"].ToString();
                        c.Codigo = int.Parse(leitor["Codigo"].ToString());
                    }
                }
            }
            catch (Exception e) { c = null; }

            if (con.State == ConnectionState.Open)
                con.Close(); //FECHA CONEXÃO

            return c;
        }

















    }//FINAL DA CLASSE
}//NAMESPACE