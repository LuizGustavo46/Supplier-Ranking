/*RESPONSÁVEL PELA CLASSE: MARCELO LEMOS 4INF- A TURMA - B*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
        private string media;
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
        public String Numero_celular
        {
            get { return celular; }
            set { celular = value; }
        }
        public String Numero_endereco
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
        public String Media
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

        //MÉTODO PARA LOGAR COM O USUÁRIO
        public bool Logar()
        {
            bool res = false;
           
            try
            {
                con.Open();
                SqlCommand query =
                    new SqlCommand("SELECT * FROM fornecedor WHERE cnpj = @cnpj AND senha = @senha", con);
                query.Parameters.AddWithValue("@login", cnpj);
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
      
                return res;
        }

        /*RESPONSÁVEL PELA CLASSE: MARCELO LEMOS 4INF- A TURMA - B*/

        // CADASTRAR NOVO FORNECEDOR
        public string Cadastrar()
        {
            string res = "Inserido com sucesso!";
            try
            {
                con.Open(); // abre conexão
                // Criação de comando
                SqlCommand query =
                    new SqlCommand("INSERT INTO fornecedor VALUES (@cnpj,@nome_empresa,@email,@telefone@celular,@endereco,@bairro,@cidade,@uf,@cep,@senha,@slogan,@descricao,@media,@plano,@imagem,@nome_categoria)",
                        con);
                // Adiciona os parâmetros
                if (telefone.Length == 14 || telefone.Length == 15)
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


        //MÉTODO PARA LOGAR COM O FORNECEDOR
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

        public Boolean UpdateSenha (string senha, string novaSenha, string senhaDigitada)
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

                if (novaSenha!=senha && senhaDigitada == senha)
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

        // Método para EDITAR um Funcionário - Bibliotecário
        internal string EditarInfo(string confirmaSenha)
        {
            string res = "Salvo com sucesso!";

            /*PEDE-SE UMA CONFIRMAÇÃO DE SENHA PARA EDITAR AS INFORMAÇÕES DO FORNCEDOR
            PARA QUE TENHA UMA SEGURANÇA MAIOR*/

            if (confirmaSenha == senha)
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


        // MÉTODO PARA EXCLUIR UM FUNCIONÁRIO DO BANCO
        public bool DeletarFuncionario()
        {
            try
            {
                con.Open();

                SqlCommand query =
                    new SqlCommand("DELETE FROM Funcionario WHERE codigo = @codigo AND nome = @nome",
                        con);
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

        // Método para EDITAR um Funcionário - FORNECEDOR
        internal string EditarFuncionario(string novoNome, string novaSenha)
        {
            string res = "Salvo com sucesso!";
            try
            {
                con.Open();
                SqlCommand query =
                    new SqlCommand("UPDATE funcionario SET " +
                    "senha = @senha, nome = @nome", con);

                /*SE AS INFORMAÇÕES DIGITADAS PELO USUÁRIO FOREM IGUAIS AS QUE JA ESTÃO NO BANCO
                O SISTEMA AVISA O USUARIO*/
                /*RESPONSÁVEL PELA CLASSE: MARCELO LEMOS 4INF- A TURMA - B*/

                if (nome!=novoNome && senha!=novaSenha)
                {                 
                    query.Parameters.AddWithValue("@senha", senha);
                    query.Parameters.AddWithValue("@nome", nome);
               
                    query.ExecuteNonQuery();
                }
                else
                {
                    res = "Informações ainda são iguais as antigas. Atualize as informações";
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







    }
    }


    
