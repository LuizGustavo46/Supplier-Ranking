using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SupplierRanking.Models
{
    public class Categorias
    {
        //CONEXÃO COM O BANCO DE DADOS - SE FOR USAR EM CASA É SÓ TROCAR "SENAI" PARA O SEU NOME
        private static SqlConnection con =
            new SqlConnection(ConfigurationManager.ConnectionStrings["VALMIR"].ConnectionString);

        //CAMPOS DO BANCO DE DADOS
        private string categoria;

        public string Categoria         { get { return categoria; }         set { categoria = value; }}

        /************************************************************ LISTAR CATEGORIAS ********************************************************/

        public List<Categorias> ListaCategorias() //MÉTODO PARA RETORNR A LISTA DE TODAS AS CATEGORIAS CADASTRADAS NO BANCO
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

            }catch (Exception ex) { lista = null; }
    
            if (con.State == ConnectionState.Open)
                con.Close();
            return lista;
        }

    }
}