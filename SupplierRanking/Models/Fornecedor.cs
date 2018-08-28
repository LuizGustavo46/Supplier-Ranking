//Teste
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
        private string nome;
        private string email;
        private string telefone;
        private string cidade;
        private string bairro;
        private string rua;
        private string uf;
        private string img;
        private string senha;
        private string numero_celular;
        private string numero_endereco;
        private string posicao;
        private string descricao;
        private string cep;
        private string media;

        private bool fornecedor;
        private bool consumidor;

        //---* PEGANDO E RETORNANDO VALORES INSERIDOS NAS VARIAVEIS *---
        public String Cnpj
        {
            get { return cnpj; }
            set { cnpj = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
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

        public String Img
        {
            get { return img; }
            set { img = value; }
        }
        public String Senha
        {
            get { return senha; }
            set { senha = value; }
        }
        public String Numero_celular
        {
            get { return numero_celular; }
            set { numero_celular = value; }
        }
        public String Numero_endereco
        {
            get { return numero_endereco; }
            set { numero_endereco = value; }
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





    }
}