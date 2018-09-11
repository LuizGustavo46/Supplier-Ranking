using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SupplierRanking.Models
{
    public class Comprador
    {
        private static SqlConnection con =
                new SqlConnection("Server=ESN509VMSSQL;Database=TCC_Laressa_Luiz_Marcelo_Valmir;User id=Aluno;Password=Senai1234");

        private int codigo;
        private string cpf;
        private string nome;
        private string sobrenome;
        private string email;
        private string tipo_usuario;
        private string senha;
        private string cnpj;
        private string nome_empresa;
        private string endereco;
        private string bairro;
        private string cidade;
        private string uf;
        private string cep;
        private string fone;
        private string celular;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public string Cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public string Sobrenome
        {
            get { return sobrenome; }
            set { sobrenome = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Tipo_usuario
        {
            get { return tipo_usuario; }
            set { tipo_usuario = value; }
        }

        public string Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        public string Cnpj
        {
            get { return cnpj; }
            set { cnpj = value; }
        }

        public string Nome_empresa
        {
            get { return nome_empresa; }
            set { nome_empresa = value; }
        }

        public string Endereço
        {
            get { return endereco; }
            set { endereco = value; }
        }

        public string Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }

        public string Cidade
        {
            get { return cidade; }
            set { cidade = value; }
        }

        public string Uf
        {
            get { return uf; }
            set { uf = value; }
        }

        public string Cep
        {
            get { return cep; }
            set { cep = value; }
        }

        public string Fone
        {
            get { return fone; }
            set { fone = value; }
        }

        public string Celular
        {
            get { return celular; }
            set { celular = value; }
        }
        //-----INICIO DOS MÉTODOS -------------------------------------------------------------------------------------------------

        //Método de login de pessoa fisica
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
            }
            catch (Exception e)
            {
                res = false;
            }

            if (con.State == ConnectionState.Open)
                con.Close();
            return res; //Retorna true or false
        }


        //Método de login de pessoa juridica 
        public bool LoginPessoaJuridica()
        {
            bool res = false;
            try
            {
                con.Open();
                SqlCommand query =
                    new SqlCommand("SELECT * FROM comprador WHERE cnpj = @cnpj AND senha = @senha", con);
                query.Parameters.AddWithValue("@cnpj", cpf);
                query.Parameters.AddWithValue("@senha", senha);
                SqlDataReader leitor = query.ExecuteReader();

                res = leitor.HasRows;
            }
            catch (Exception e)
            {
                res = false;
            }

            if (con.State == ConnectionState.Open)
                con.Close();
            return res; //Retorna true or false
        }

        //Contador de código para cadastro de pessoas fisicas ou juridicas
        public static Comprador ContadorCadastroUsuario()
        {
            Comprador u = new Comprador();
            try
            {
                con.Open();
                SqlCommand query =
                    new SqlCommand("select max(codigo) AS valor from comprador", con);

                int codigo = 0;

                try
                {
                    codigo = Convert.ToInt32(query.ExecuteScalar());
                }
                catch (Exception ex) { }

                u.codigo = codigo + 1;
                u.cpf = "";
                u.nome = "";
                u.sobrenome = "";
                u.email = "";
                u.senha = "";
                u.cnpj = "";
                u.nome_empresa = "";
                u.endereco = "";
                u.bairro = "";
                u.cidade = "";
                u.uf = "";
                u.cep = "";
                u.fone = "";
                u.celular = "";
            }
            catch (Exception e)
            {
                u = null;
            }
            if (con.State == ConnectionState.Open)
                con.Close();
            return u;
        }

        //Cadastro de usuario VERIFICAR SE ESTÁ CORRETO
        public bool CadastroPessoaFisica()
        {
            bool res = true;
            try
            {
                con.Open(); // abre conexão
                // Criação de comando
                SqlCommand query =
                    new SqlCommand("INSERT INTO comprador VALUES (@codigo,@cpf,@nome,@sobrenome,@email,@tipo_usuario,@senha," +
                    "@cargo,@cnpj,@nome_empresa,@endereco,@bairro,@cidade,@uf,@cep,@fone,@celular)",
                        con);
                if (cpf.Length == 14 && senha.Length >= 5 && nome.Length >= 3 && email.Length >= 8 && cep.Length == 9 &&
                    (fone.Length == 14 || fone.Length == 0) && (celular.Length == 15 || celular.Length == 0)) //Condição para efetuar o cadastro
                {
                    // Adiciona os parâmetros
                    query.Parameters.AddWithValue("codigo", codigo);
                    query.Parameters.AddWithValue("@cpf", cpf);
                    query.Parameters.AddWithValue("@nome", nome);
                    query.Parameters.AddWithValue("@sobrenome", sobrenome);
                    query.Parameters.AddWithValue("@email", email);
                    query.Parameters.AddWithValue("@tipo_usuario", tipo_usuario);
                    query.Parameters.AddWithValue("@senha", senha);
                    query.Parameters.AddWithValue("@cnpj", cnpj);
                    query.Parameters.AddWithValue("@nome_empresa", nome_empresa);
                    query.Parameters.AddWithValue("endereco", endereco);
                    query.Parameters.AddWithValue("@bairro", bairro);
                    query.Parameters.AddWithValue("@cidade", cidade);
                    query.Parameters.AddWithValue("@uf", uf);
                    query.Parameters.AddWithValue("@cep", cep);
                    query.Parameters.AddWithValue("@fone", fone);
                    query.Parameters.AddWithValue("@celular", celular);
                    query.ExecuteNonQuery();
                }
                else
                {
                    res = false;
                }
            }
            catch (Exception ex)
            {
                string exception = ex.Message; // Caso der erro na inserção
            }

            if (con.State == ConnectionState.Open)
                con.Close(); // fecha conexão

            return res; // retorna resposta de confirmação
        }

        //Cadastro de usuario VERIFICAR SE ESTÁ CORRETO
        public bool CadastroPessoaJuridica()
        {
            bool res = true;
            try
            {
                con.Open(); // abre conexão
                // Criação de comando
                SqlCommand query =
                    new SqlCommand("INSERT INTO comprador VALUES (@codigo,@cpf,@nome,@sobrenome,@email,@tipo_usuario,@senha," +
                    "@cargo,@cnpj,@nome_empresa,@endereco,@bairro,@cidade,@uf,@cep,@fone,@celular)",
                        con);
                if (cnpj.Length == 19 && senha.Length >= 5 && nome_empresa.Length >= 3 && email.Length >= 8 && cep.Length == 9 &&
                    (fone.Length == 14 || fone.Length == 0) && (celular.Length == 15 || celular.Length == 0) && uf.Length == 2 &&
                    endereco.Length > 1 && bairro.Length > 1 && cidade.Length > 1) //Condição para efetuar o cadastro
                {
                    // Adiciona os parâmetros
                    query.Parameters.AddWithValue("codigo", codigo);
                    query.Parameters.AddWithValue("@cpf", cpf);
                    query.Parameters.AddWithValue("@nome", nome);
                    query.Parameters.AddWithValue("@sobrenome", sobrenome);
                    query.Parameters.AddWithValue("@email", email);
                    query.Parameters.AddWithValue("@tipo_usuario", tipo_usuario);
                    query.Parameters.AddWithValue("@senha", senha);
                    query.Parameters.AddWithValue("@cnpj", cnpj);
                    query.Parameters.AddWithValue("@nome_empresa", nome_empresa);
                    query.Parameters.AddWithValue("endereco", endereco);
                    query.Parameters.AddWithValue("@bairro", bairro);
                    query.Parameters.AddWithValue("@cidade", cidade);
                    query.Parameters.AddWithValue("@uf", uf);
                    query.Parameters.AddWithValue("@cep", cep);
                    query.Parameters.AddWithValue("@fone", fone);
                    query.Parameters.AddWithValue("@celular", celular);
                    query.ExecuteNonQuery();
                }
                else
                {
                    res = false;
                }
            }
            catch (Exception ex)
            {
                string exception = ex.Message; // Caso der erro na inserção
            }

            if (con.State == ConnectionState.Open)
                con.Close(); // fecha conexão

            return res; // retorna resposta de confirmação
        }

        //BUSCAR USUARIO PARA MOSTRAR NA PAGINA DE UPDATE
        public static Comprador BuscaPessoa(int codigo)
        {
            Comprador bp = new Comprador();
            try
            {
                con.Open();
                SqlCommand query =
                    new SqlCommand("SELECT * FROM comprador WHERE codigo = @codigo", con);
                query.Parameters.AddWithValue("@codigo", codigo);
                SqlDataReader leitor = query.ExecuteReader();

                while (leitor.Read())
                {
                    bp.codigo = int.Parse(leitor["codigo"].ToString());
                    bp.cpf = leitor["cpf"].ToString();
                    bp.nome = leitor["nome"].ToString();
                    bp.sobrenome = leitor["sobrenome"].ToString();
                    bp.email = leitor["email"].ToString();
                    bp.tipo_usuario = leitor["tipo_usuario"].ToString();
                    bp.senha = leitor["senha"].ToString();
                    bp.cnpj = leitor["cnpj"].ToString();
                    bp.nome_empresa = leitor["nome_empresa"].ToString();
                    bp.endereco = leitor["endereco"].ToString();
                    bp.bairro = leitor["bairro"].ToString();
                    bp.cidade = leitor["cidade"].ToString();
                    bp.uf = leitor["uf"].ToString();
                    bp.cep = leitor["cep"].ToString();
                    bp.fone = leitor["fone"].ToString();
                    bp.celular = leitor["celular"].ToString();
                }
            }
            catch (Exception e)
            {
                bp = null;
            }

            if (con.State == ConnectionState.Open)
                con.Close();
            return bp;
        }

        //MÉTODO DE UPDATE PESSOA FISICA
        internal bool UpdatePessoaFisica()
        {
            bool res = true;
            try
            {
                con.Open();
                SqlCommand query =
                    new SqlCommand("UPDATE comprador SET " +
                    "nome = @nome, sobrenome = @sobrenome, email = @email, fone = @fone, @celular = celular WHERE codigo = @codigo", con);
                if (nome.Length >= 1 && sobrenome.Length >= 1 && email.Length >= 8 && (fone.Length == 14 || fone.Length == 0) && (celular.Length == 15 || celular.Length == 0))
                {
                    query.Parameters.AddWithValue("@codigo", codigo);
                    query.Parameters.AddWithValue("@nome", nome);
                    query.Parameters.AddWithValue("@sobrenome", sobrenome);
                    query.Parameters.AddWithValue("@email", email);
                    query.Parameters.AddWithValue("@fone", fone);
                    query.Parameters.AddWithValue("@celular", celular);
                    query.ExecuteNonQuery(); // executa
                }
                else
                {
                    res = false;
                }
            }
            catch (Exception e)
            {
                string exception = e.Message;
            }

            if (con.State == System.Data.ConnectionState.Open)
                con.Close();

            return res;
        }

        //MÉTODO DE UPDATE PESSOA JURIDICA
        internal bool UpdatePessoaJuridica()
        {
            bool res = true;
            try
            {
                con.Open();
                SqlCommand query =
                    new SqlCommand("UPDATE comprador SET " +
                    "nome_empresa = @nome_empresa, email = @email, endereco = @endereco, bairro = @bairro, cidade = @cidade," + 
                    "@uf = uf, @cep = cep, @fone = fone, @celular = celular WHERE codigo = @codigo", con);
                if (nome_empresa.Length >= 1 && email.Length >= 8 && (fone.Length == 14 || fone.Length == 0) && (celular.Length == 15 || celular.Length == 0)
                    && endereco.Length > 1 && bairro.Length > 1 && cidade.Length > 1 && uf.Length == 2 && cep.Length == 9)
                {
                    query.Parameters.AddWithValue("@codigo", codigo);
                    query.Parameters.AddWithValue("@nome_empresa", nome_empresa);
                    query.Parameters.AddWithValue("@email", email);
                    query.Parameters.AddWithValue("@endereco", endereco);
                    query.Parameters.AddWithValue("@bairro", bairro);
                    query.Parameters.AddWithValue("@cidade", cidade);
                    query.Parameters.AddWithValue("@uf", uf);
                    query.Parameters.AddWithValue("@cep", cep);
                    query.Parameters.AddWithValue("@fone", fone);
                    query.Parameters.AddWithValue("@celular", celular);
                    query.ExecuteNonQuery(); // executa
                }
                else
                {
                    res = false;
                }
            }
            catch (Exception e)
            {
                string exception = e.Message;
            }

            if (con.State == System.Data.ConnectionState.Open)
                con.Close();

            return res;
        }

    }//Final da Classe
}//Namespace