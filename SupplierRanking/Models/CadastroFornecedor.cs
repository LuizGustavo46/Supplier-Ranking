using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SupplierRanking.Models
{
    public class CadastroFornecedor
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
    }
}