using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SupplierRanking.Models
{
    public class Categorias
    {
        private static SqlConnection con =
                new SqlConnection("Server=ESN509VMSSQL;Database=TCC_Laressa_Luiz_Marcelo_Valmir;User id=Aluno;Password=Senai1234");

        //CAMPOS DO BANCO DE DADOS
        private string categoria;

        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
        //--------------------------------------------------INICIO DOS MÉTODOS-----------------------------------------------------------

        //MÉTODO PARA RETORNR A LISTA DE TODAS AS CATEGORIAS CADASTRADAS NO BANCO
        public List<Categorias> ListaCategorias()
        {
            List<Categorias> lista = new List<Categorias>();
            try
            {
                con.Open(); //ABRE CONEXÃO
                //CRIAÇÃO DE COMANDO
                SqlCommand query =
                    new SqlCommand("SELECT * FROM categorias", con);
                SqlDataReader leitor = query.ExecuteReader();

                while (leitor.Read())
                {
                    Categorias c = new Categorias();
                    c.categoria= leitor["nome"].ToString();
                    lista.Add(c); //ADICIONA O QUE VEIO DO BANCO NA LISTA              
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

    }
}