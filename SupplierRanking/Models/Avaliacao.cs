using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SupplierRanking.Models
{
    public class Avaliacao
    {
        private static SqlConnection con =
                new SqlConnection("Server=ESN509VMSSQL;Database=TCC_Laressa_Luiz_Marcelo_Valmir;User id=Aluno;Password=Senai1234");
        //CAMPOS DO BANCO DE DADOS
        private int qualidade;
        private int atendimento;
        private int entrega;
        private int preco;
        private int satisfacao;
        private string comentario;
        private DateTime data_avaliacao;
        private string cnpj_fornecedor;
        private int codigo_comprador;

        public int Qualidade
        {
            get { return qualidade; }
            set { qualidade = value; }
        }

        public int Atendimento
        {
            get { return atendimento; }
            set { atendimento = value; }
        }

        public int Entrega
        {
            get { return entrega; }
            set { entrega = value; }
        }

        public int Preco
        {
            get { return preco; }
            set { preco = value; }
        }

        public int Satisfacao
        {
            get { return satisfacao; }
            set { satisfacao = value; }
        }

        public string Comentario
        {
            get { return comentario; }
            set { comentario = value; }
        }

        public DateTime Data_avaliacao
        {
            get { return data_avaliacao; }
            set { data_avaliacao = value; }
        }

        public string Cnpj_fornecedor
        {
            get { return cnpj_fornecedor; }
            set { cnpj_fornecedor = value; }
        }

        public int Codigo_comprador
        {
            get { return codigo_comprador; }
            set { codigo_comprador = value; }
        }
        //--------------------------------------------------INICIO DOS MÉTODOS-----------------------------------------------------------

        //MÉTODO PARA INSERIR CADASTRO DE AVALIAÇÃO
        public bool CadastrarAvaliacao() //FALTA COLOCAR A RESTRIÇÃO DE TEMPO - SÓ PODERA AVALIAR A MESMA EMPRESA DEPOIS DE 7 DIAS
        {
            try
            {
                con.Open();//ABRE CONEXÃO
                //CONDIÇÃO PARA SABER SE AQUELE COMPRADOR JÁ AVALIOU AQUELE FORNECEDOR
                SqlCommand query1 = //UM COMPRADOR NÃO PODE AVALIAR MAIS DE UMA VEZ O MESMO FORNECEDOR
                    new SqlCommand(" SELECT * FROM avaliacao WHERE cnpj_fonecedor = @cnpj_fonecedor AND"+
                    "codigo_comprador = @codigo_comprador", con);
                query1.Parameters.AddWithValue("@cnpj_fornecedor", cnpj_fornecedor);
                query1.Parameters.AddWithValue("@codigo_comprador", codigo_comprador);
                SqlDataReader leitor = query1.ExecuteReader();

                //SE NÃO HOUVER NADA REGISTRADO NA TABELA DE AVALIACAO NO BANCO COM RELAÇÃO AO COMPRADOR E AO FORNECEDOR,
                //SOMENTE ASSIM PODE OCORRER O REGISTRO DA AVALIAÇÃO
                if (!leitor.Read())
                {
                    //CRIAÇÃO DE COMANDO
                    SqlCommand query =
                    new SqlCommand("INSERT INTO avaliacao VALUES (@qualidade,@atendimento,@entrega,@preco," +
                    "@satisfacao,@comentario,@data_avaliacao,@cnpj_fornecedor,@codigo_comprador)", con);

                    query.Parameters.AddWithValue("@qualidade", qualidade);
                    query.Parameters.AddWithValue("@atendimento", atendimento);
                    query.Parameters.AddWithValue("@entrega", entrega);
                    query.Parameters.AddWithValue("@preco", preco);
                    query.Parameters.AddWithValue("@satisfcao", satisfacao);
                    query.Parameters.AddWithValue("@comentario", comentario);
                    query.Parameters.AddWithValue("@data_avaliacao", data_avaliacao);
                    query.Parameters.AddWithValue("@cnpj_fornecedor", cnpj_fornecedor);
                    query.Parameters.AddWithValue("@codigo_comprador", codigo_comprador);
                    query.ExecuteNonQuery(); //EXECUTA
                }           

            }
            catch(Exception ex)
            {
                string exception = ex.Message; //CASO DER ERRO NA INSERÇÃO
                return false;
            }
            if (con.State == ConnectionState.Open)
                con.Close(); //FECHA CONEXÃO

            return true;
        }






    }//FIM DA CLASSE
}//FIM DO NAMESPACE