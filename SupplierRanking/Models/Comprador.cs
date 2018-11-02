using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SupplierRanking.Models
{
    public class Comprador
    {
        private static SqlConnection con =
                new SqlConnection("Server=ESN509VMSSQL;Database=TCC_Laressa_Luiz_Marcelo_Valmir;User id=Aluno;Password=Senai1234");
        //CAMPOS DO BANCO DE DADOS (TODOS OS DADOS DE CADASTRO)
        private int codigo;
        private string cpf;
        private string nome;
        private string sobrenome;
        private string email;
        private string tipo_pessoa;
        private string senha;
        private string cnpj;
        private string nome_empresa;
        private string endereco;
        private string bairro;
        private string cidade;
        private string uf;
        private string cep;
        private string telefone;
        private string celular;

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
        public string Endereco          { get { return endereco; }        set { endereco = value; }}
        public string Bairro            {get { return bairro; }           set { bairro = value; }}
        public string Cidade            { get { return cidade; }          set { cidade = value; }}
        public string Uf                { get { return uf; }              set { uf = value; }}
        public string Cep               { get { return cep; }             set { cep = value; }}
        public string Telefone          { get { return telefone; }        set { telefone = value; }}
        public string Celular           { get { return celular; }         set { celular = value; }}

        /******************************************************** LOGIN PESSOA FISICA ***********************************************************/

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

        /********************************************************** LOGIN PESSOA JURIDICA **********************************************************/

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

        /********************************************************** CADASTRO PESSOA FISICCA **********************************************************/

        public bool CadastroPessoaFisica()
        {
            bool res = true;
            try
            {
                con.Open(); //ABRE CONEXÃO
                //COMANDO PARA TESTAR SE JA EXISTE O CPF CADASTRADO
                SqlCommand query =
                    new SqlCommand("SELECT cpf FROM comprador WHERE cpf = @cpf)", con);
                query.Parameters.AddWithValue("@cpf", cpf);
                SqlDataReader leitor = query.ExecuteReader();

                if (!leitor.Read())
                {
                    //INSERIR CADASTRO NO BANCO
                    SqlCommand queryInsert =
                    new SqlCommand("INSERT INTO comprador VALUES (@cpf,@nome,@sobrenome,@email,@senha,@tipo_pessoa," +
                    "@cnpj,@nome_empresa,@endereco,@bairro,@cidade,@uf,@cep,@telefone,@celular)", con);
                
                    if (cpf.Length == 14 && senha.Length >= 5 && nome.Length >= 3 && email.Length >= 8 &&
                    (telefone.Length == 14 || telefone.Length == 0) && (celular.Length == 15 || celular.Length == 0)) //CONDIÇÃO PARA EFETUAR O CADASTRO
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
                        queryInsert.Parameters.AddWithValue("endereco",         endereco);
                        queryInsert.Parameters.AddWithValue("@bairro",          bairro);
                        queryInsert.Parameters.AddWithValue("@cidade",          cidade);
                        queryInsert.Parameters.AddWithValue("@uf",              uf);
                        queryInsert.Parameters.AddWithValue("@cep",             cep);
                        queryInsert.Parameters.AddWithValue("@telefone",        telefone);
                        queryInsert.Parameters.AddWithValue("@celular",         celular);
                        queryInsert.ExecuteNonQuery();

                    }else{ res = false; }         
                }     
            }catch (Exception ex) { return res; } 
       
            if (con.State == ConnectionState.Open)
                con.Close(); //FECHA CONEXÃO
            return res; //RETORNA RESPOSTA DE CONFIRMAÇÃO
        }
    
        /******************************************************* CADASTRO PESSOA JURIDICA *******************************************************/

        public bool CadastroPessoaJuridica()
        {
            bool res = true;
            try
            {
                con.Open(); //ABRE CONEXÃO
                //CRIAÇÃO DE COMANDO
                SqlCommand query =
                    new SqlCommand("INSERT INTO comprador VALUES (@cpf,@nome,@sobrenome,@email,@senha,@tipo_pessoa," +
                    "@cnpj,@nome_empresa,@endereco,@bairro,@cidade,@uf,@cep,@telefone,@celular)",
                        con);
                if (cnpj.Length == 19 && senha.Length >= 5 && nome_empresa.Length >= 1 && email.Length >= 4 && cep.Length == 9 &&
                    (telefone.Length == 14 || telefone.Length == 0) && (celular.Length == 15 || celular.Length == 0) && uf.Length == 2 &&
                    endereco.Length > 1 && bairro.Length > 1 && cidade.Length > 1) //CONDIÇÃO PARA EFETUAR O CADASTRO
                {
                    //ADICIONA OS PARÂMETROS --- NÃO PRECISA PASSAR O CAMPO CODIGO, ELE GERA AUTOMATICAMENTE NO BANCO
                    query.Parameters.AddWithValue("@cpf",           cpf);
                    query.Parameters.AddWithValue("@nome",          nome);
                    query.Parameters.AddWithValue("@sobrenome",     sobrenome);
                    query.Parameters.AddWithValue("@email",         email);
                    query.Parameters.AddWithValue("@senha",         senha);
                    query.Parameters.AddWithValue("@tipo_pessoa",   tipo_pessoa);
                    query.Parameters.AddWithValue("@cnpj",          cnpj);
                    query.Parameters.AddWithValue("@nome_empresa",  nome_empresa);
                    query.Parameters.AddWithValue("endereco",       endereco);
                    query.Parameters.AddWithValue("@bairro",        bairro);
                    query.Parameters.AddWithValue("@cidade",        cidade);
                    query.Parameters.AddWithValue("@uf",            uf);
                    query.Parameters.AddWithValue("@cep",           cep);
                    query.Parameters.AddWithValue("@telefone",      telefone);
                    query.Parameters.AddWithValue("@celular",       celular);
                    query.ExecuteNonQuery();

                }else{ res = false; }       
                                       
            }catch (Exception ex) { res = false; }
            

            if (con.State == ConnectionState.Open)
                con.Close(); //FECHA CONEXÃO
            return res; //RETORNA RESPOSTA DE CONFIRMAÇÃO
        }

        /********************************************************** BUSCA PESSOA **********************************************************/

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
                    bp.endereco     = leitor["endereco"].ToString();
                    bp.bairro       = leitor["bairro"].ToString();
                    bp.cidade       = leitor["cidade"].ToString();
                    bp.uf           = leitor["uf"].ToString();
                    bp.cep          = leitor["cep"].ToString();
                    bp.telefone     = leitor["telefone"].ToString();
                    bp.celular      = leitor["celular"].ToString();
                }
            }catch (Exception e) { bp = null; }
 
            if (con.State == ConnectionState.Open)
                con.Close();
            return bp;
        }

        /******************************************************** UPDATE PESSOA FISICA ********************************************************/

        internal bool UpdatePessoaFisica()
        {
            bool res = true;
            try
            {
                con.Open();
                SqlCommand query =
                    new SqlCommand("UPDATE comprador SET nome = @nome, sobrenome = @sobrenome, email = @email," +
                    "uf = @uf, telefone = @telefone, @celular = celular WHERE codigo = @codigo", con);
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

        /******************************************************** UPDATE PESSOA JURIDICA ********************************************************/

        internal bool UpdatePessoaJuridica()
        {
            bool res = true;
            try
            {
                con.Open(); //ABRE CONEXÃO
                //CRIAÇÃO DE COMANDO
                SqlCommand query =
                    new SqlCommand("UPDATE comprador SET nome_empresa = @nome_empresa, email = @email, endereco = @endereco," +
                    "bairro = @bairro, cidade = @cidade, uf = @uf, cep = @cep, telefone = @telefone," + 
                    "celular = @celular WHERE codigo = @codigo", con);
                if (nome_empresa.Length >= 1 && email.Length >= 8 && (telefone.Length == 14 || telefone.Length == 0) &&
                    (celular.Length == 15 || celular.Length == 0) && endereco.Length > 1 && bairro.Length > 1 &&
                    cidade.Length > 1 && uf.Length == 2 && cep.Length == 9)
                {
                    query.Parameters.AddWithValue("@codigo",        codigo);
                    query.Parameters.AddWithValue("@nome_empresa",  nome_empresa);
                    query.Parameters.AddWithValue("@email",         email);
                    query.Parameters.AddWithValue("@endereco",      endereco);
                    query.Parameters.AddWithValue("@bairro",        bairro);
                    query.Parameters.AddWithValue("@cidade",        cidade);
                    query.Parameters.AddWithValue("@uf",            uf);
                    query.Parameters.AddWithValue("@cep",           cep);
                    query.Parameters.AddWithValue("@telefone",      telefone);
                    query.Parameters.AddWithValue("@celular",       celular);
                    query.ExecuteNonQuery(); //EXECUTA

                }else{ res = false; }
    
            }catch (Exception e) { res = false; }

            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            return res;
        }

        /******************************************************** EXCLUIR CONTA ********************************************************/    
        
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

        /******************************************************** UPDATE SENHA ********************************************************/

        public bool UpdateSenha(string senhaAntiga, string senhaNova, string confirmaSenhaNova) //ALTERAR SENHA
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
                if (senhaAntiga == senhaUsada && senhaNova == confirmaSenhaNova && senhaNova != senhaAntiga) //CONDIÇÃO PARA EFETUAR O UPDATE SENHA
                {
                    SqlCommand querySenha =
                    new SqlCommand("UPDATE comprador SET senha = @senha WHERE codigo = @codigo", con);
                    querySenha.Parameters.AddWithValue("@senha", senhaNova);
                    querySenha.Parameters.AddWithValue("@codigo", codigo);
                    querySenha.ExecuteNonQuery(); //EXECUTE
                }

                //SE O COMPRADOR ESQUECEU A SENHA (RESTAURAR SENHA)
                if(senhaAntiga == null && senhaNova == confirmaSenhaNova && senhaNova != senhaUsada)
                {
                    SqlCommand querySenha =
                    new SqlCommand("UPDATE comprador SET senha = @senha WHERE codigo = @codigo", con);
                    querySenha.Parameters.AddWithValue("@senha", senhaNova);
                    querySenha.Parameters.AddWithValue("@codigo", codigoEmail);
                    querySenha.ExecuteNonQuery(); //EXECUTE
                }

            }catch (Exception ex) { return false; }

            if (con.State == ConnectionState.Open)
                con.Close();
            return true; 
        }

        /******************************************************* RESTAURAR SENHA *******************************************************/

        public bool RestaurarSenha(string cnpj, string cpf) //RESTAURAR (ESQUECEU SUA SENHA
        {
            string email = "";

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

                //CONFIGURANDO A MENSAGEM
                MailMessage mail = new MailMessage();
                //ORIGEM
                mail.From = new MailAddress("vaal_sk8@live.com");
                //DESTINATÁRIO
                mail.To.Add(email);
                //ASSUNTO
                mail.Subject = "REDEFINIÇÃO DE SENHA - Supplier Ranking";
                //CORPO DO E-MAIL
                mail.Body = "Clique aqui para redefinir sua senha:\n http://localhost:16962/Comprador/UpdateSenha";
                //CONFIGURAR O SMTP
                SmtpClient smtpServer = new SmtpClient("smtp.live.com");
                //CONFIGURAR PORTA
                smtpServer.Port = 25;
                //HABILITAR O TLS
                smtpServer.EnableSsl = true;
                //CONFIGURAR USUARIO E SENHA PARA LOGAR
                smtpServer.Credentials = new System.Net.NetworkCredential("vaal_sk8@live.com", "counter4");
                //ENVIAR
                smtpServer.Send(mail);

            }catch (Exception ex) { return false; }
 
            if (con.State == ConnectionState.Open)
                con.Close();
            return true;
        }

















    }//FINAL DA CLASSE
}//NAMESPACE