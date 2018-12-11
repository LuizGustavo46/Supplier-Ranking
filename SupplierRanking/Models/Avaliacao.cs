using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SupplierRanking.Models;
using System.Configuration;

namespace SupplierRanking.Models
{
    public class Avaliacao
    {
        //CONEXÃO COM O BANCO DE DADOS - SE FOR USAR EM CASA É SÓ TROCAR "SENAI" PARA O SEU NOME
        private static SqlConnection con =
            new SqlConnection(ConfigurationManager.ConnectionStrings["MARCELO"].ConnectionString);

        //CAMPOS DO BANCO DE DADOS
        private int qualidade;
        private int atendimento;
        private int entrega;
        private int preco;
        private int satisfacao;
        private string comentario;
        private string data_avaliacao;
        private string cnpj_fornecedor;
        private int codigo_comprador;
        private Comprador c;

        public int Qualidade                { get { return qualidade; }             set { qualidade = value; }}
        public int Atendimento              { get { return atendimento; }           set { atendimento = value; }}
        public int Entrega                  { get { return entrega; }               set { entrega = value; }}
        public int Preco                    { get { return preco; }                 set { preco = value; }}
        public int Satisfacao               { get { return satisfacao; }            set { satisfacao = value; } }
        public string Comentario            { get { return comentario; }            set { comentario = value; }}
        public string Data_avaliacao        { get { return data_avaliacao; }        set { data_avaliacao = value; }}
        public string Cnpj_fornecedor       { get { return cnpj_fornecedor; }       set { cnpj_fornecedor = value; }}
        public int Codigo_comprador         { get { return codigo_comprador; }      set { codigo_comprador = value; }}
        public Comprador C                  { get { return c; }                     set { c = value; }}

        /***************************************************** VERIFICAR AVALIAÇÃO 7 DIAS ************************************************/

        public bool VerificarSeteDias()
        {
            Avaliacao a = new Avaliacao();
            string data_agora = DateTime.Now.ToShortDateString(); //PEGA A DATA COMPLETA SEM A HORA DO DIA
            try
            {
                con.Open();
                SqlCommand query = //UM COMPRADOR NÃO PODE AVALIAR MAIS DE UMA VEZ O MESMO FORNECEDOR
                    new SqlCommand(" SELECT * FROM avaliacao WHERE cnpj_fornecedor = @cnpj_fornecedor AND " +
                    "codigo_comprador = @codigo_comprador", con);
                query.Parameters.AddWithValue("@cnpj_fornecedor", cnpj_fornecedor);
                query.Parameters.AddWithValue("@codigo_comprador", codigo_comprador);
                SqlDataReader leitor = query.ExecuteReader();

                //SE NÃO HOUVER NADA REGISTRADO NA TABELA DE AVALIACAO NO BANCO, COM RELAÇÃO AO COMPRADOR E AO FORNECEDOR,
                //OU A ULTIMA AVALIAÇÃO JA PASSOU DE 7 DIAS, SOMENTE ASSIM PODE OCORRER O REGISTRO DA AVALIAÇÃO NOVAMENTE

                if (leitor.Read())  //SE CAIR NO ELSE É PORQUE O COMPRADOR ESTA FAZENDO A PRIMEIRA AVALIAÇÃO DA EMPRESA
                {
                    a.data_avaliacao = leitor["data_avaliacao"].ToString();
                    TimeSpan date = Convert.ToDateTime(data_agora) - Convert.ToDateTime(a.data_avaliacao);
                    int diferenca = date.Days;

                    if (diferenca >= 7)
                    {
                        con.Close(); //FECHA CONEXÃO
                        return true;
                    }
                    else
                    {
                        con.Close(); //FECHA CONEXÃO
                        return false;
                    }
                }
            } catch(Exception ex) { }

            if (con.State == ConnectionState.Open)
                con.Close(); //FECHA CONEXÃO
            return true;
        }

        /***************************************************** INSERIR CADASTRO DE AVALIAÇÃO ************************************************/

        public bool CadastrarAvaliacao()
        {
            string data_agora = DateTime.Now.ToShortDateString(); //PEGA A DATA COMPLETA SEM A HORA DO DIA
            try
            {
                con.Open();//ABRE CONEXÃO       
                
                //CRIAÇÃO DE COMANDO
                SqlCommand query =
                new SqlCommand("INSERT INTO avaliacao VALUES (@qualidade,@atendimento,@entrega,@preco," +
                "@satisfacao,@comentario,@data_avaliacao,@cnpj_fornecedor,@codigo_comprador)", con);

                query.Parameters.AddWithValue("@qualidade", qualidade);
                query.Parameters.AddWithValue("@atendimento", atendimento);
                query.Parameters.AddWithValue("@entrega", entrega);
                query.Parameters.AddWithValue("@preco", preco);
                query.Parameters.AddWithValue("@satisfacao", satisfacao);
                query.Parameters.AddWithValue("@comentario", comentario);
                query.Parameters.AddWithValue("@data_avaliacao", data_agora);
                query.Parameters.AddWithValue("@cnpj_fornecedor", cnpj_fornecedor);
                query.Parameters.AddWithValue("@codigo_comprador", codigo_comprador);
                query.ExecuteNonQuery(); //EXECUTA
                          
            }catch(Exception ex) { return false; }

            double mediaQualidade = 0, mediaAtendimento = 0, mediaEntrega = 0, mediaPreco = 0, mediaSatisfacao = 0, media = 0;
            int cont = 0;
            try //TRY PARA EXECUÇÃO DA LÓGICA DE ATUALIZAÇÃO DA MÉDIA DAS AVALIAÇÕES
            {
                //CRIAÇÃO DE COMANDO
                SqlCommand queryMedia = new SqlCommand("SELECT qualidade, atendimento, entrega, preco, satisfacao " + 
                    "FROM avaliacao WHERE cnpj_fornecedor = @cnpj_fornecedor", con);
                queryMedia.Parameters.AddWithValue("@cnpj_fornecedor", cnpj_fornecedor);
                SqlDataReader leitor = queryMedia.ExecuteReader();

                while (leitor.Read()) //ENQUANTO O LEITOR LER AS AVALIAÇÕES
                {
                    Avaliacao b = new Avaliacao();
                    b.qualidade         =   int.Parse(leitor["Qualidade"].ToString());
                    b.atendimento       =   int.Parse(leitor["Atendimento"].ToString());
                    b.entrega           =   int.Parse(leitor["Entrega"].ToString());
                    b.preco             =   int.Parse(leitor["Preco"].ToString());
                    b.satisfacao        =   int.Parse(leitor["Satisfacao"].ToString());

                    mediaQualidade      =   mediaQualidade    +   b.qualidade;
                    mediaAtendimento    =   mediaAtendimento  +   b.atendimento;
                    mediaEntrega        =   mediaEntrega      +   b.entrega;
                    mediaPreco          =   mediaPreco        +   b.preco;
                    mediaSatisfacao     =   mediaSatisfacao   +   b.satisfacao;
        
                    cont++;
                }

                //ATRIBUINDO A MÉDIA DE CADA CRITÉRIO DE AVALIAÇÃO
                mediaQualidade      =   mediaQualidade    /   cont;
                mediaAtendimento    =   mediaAtendimento  /   cont;
                mediaEntrega        =   mediaEntrega      /   cont;
                mediaPreco          =   mediaPreco        /   cont;
                mediaSatisfacao     =   mediaSatisfacao   /   cont;
                //MÉDIA TOTAL
                media = (mediaQualidade + mediaAtendimento + mediaEntrega + mediaPreco + mediaSatisfacao) / 5;
                media = Math.Round(media, 1); //LIMITA A CASA DECIMAL "0.0"
                //COMANDO PARA INSERIR A MÉDIA PARA O FORNECEDOR
                leitor.Close();
                SqlCommand queryMedia2 = new SqlCommand("UPDATE fornecedor SET media = @media, media_qualidade = @media_qualidade, "+
                    "media_atendimento = @media_atendimento, media_entrega = @media_entrega, media_preco = @media_preco, " +
                    "media_satisfacao = @media_satisfacao WHERE cnpj = @cnpj", con);
                queryMedia2.Parameters.AddWithValue("@media", media);
                queryMedia2.Parameters.AddWithValue("@media_qualidade", mediaQualidade);
                queryMedia2.Parameters.AddWithValue("@media_atendimento", mediaAtendimento);
                queryMedia2.Parameters.AddWithValue("@media_entrega", mediaEntrega);
                queryMedia2.Parameters.AddWithValue("@media_preco", mediaPreco);
                queryMedia2.Parameters.AddWithValue("@media_satisfacao", mediaSatisfacao);
                queryMedia2.Parameters.AddWithValue("@cnpj", cnpj_fornecedor);
                queryMedia2.ExecuteNonQuery();
            } catch (Exception ex) { return false; }

            if (con.State == ConnectionState.Open)
                con.Close(); //FECHA CONEXÃO
            return true;
        }

        /************************************************ SELECT DA AVALIAÇÃO PARA FAZER UPDATE *********************************************/

        //MÉTODO QUE PUXA O COMENTÁRIO DA AVALIAÇÃO PARA FAZER O UPDATE DO COMENTÁRIO (COMENTÁRIO)
        public static Avaliacao ReturnUpdateComentario(string cnpj_fornecedor, int codigo_comprador)
        {
            Avaliacao a = new Avaliacao();
            try
            {
                con.Open(); //ABRE CONEXÃO
                SqlCommand query = new SqlCommand("SELECT * FROM avaliacao WHERE cnpj_fornecedor = @cnpj_fornecedor " +
                    "AND codigo_comprador = @codigo_comprador", con);
                query.Parameters.AddWithValue("@cnpj_fornecedor", cnpj_fornecedor);
                query.Parameters.AddWithValue("@codigo_comprador", codigo_comprador);
                SqlDataReader leitor = query.ExecuteReader();

                if (leitor.Read())
                {
                    a.qualidade         = int.Parse(leitor["Qualidade"].ToString());
                    a.atendimento       = int.Parse(leitor["Atendimento"].ToString());
                    a.entrega           = int.Parse(leitor["Entrega"].ToString());
                    a.preco             = int.Parse(leitor["Preco"].ToString());
                    a.satisfacao        = int.Parse(leitor["Satisfacao"].ToString());
                    a.comentario        = leitor["Comentario"].ToString();
                    a.data_avaliacao    = leitor["Data_avaliacao"].ToString();
                    a.cnpj_fornecedor   = leitor["Cnpj_fornecedor"].ToString();
                    a.codigo_comprador  = int.Parse(leitor["Codigo_comprador"].ToString());
                }

            } catch (Exception e) { a = null; }
                
            if (con.State == ConnectionState.Open)
                con.Close(); //FECHA CONEXÃO
            return a;
        }

        /***************************************************** UPDATE DE AVALIAÇÃO (COMENTÁRIO) **********************************************/

        public bool UpdateComentario()
        {
            try
            {
                con.Open(); //ABRE CONEXÃO
                SqlCommand query = new SqlCommand("UPDATE avaliacao SET comentario = @comentario WHERE " +
                    "cnpj_fornecedor = @cnpj_fornecedor AND codigo_comprador = @codigo_comprador", con);

                if (comentario.Length > 2)
                {
                    query.Parameters.AddWithValue("@comentario", comentario);
                    query.Parameters.AddWithValue("@cnpj_fornecedor", cnpj_fornecedor);
                    query.Parameters.AddWithValue("@codigo_comprador", codigo_comprador);
                    query.ExecuteNonQuery(); //EXECUTA
                }
                else
                {
                    return false; //TALVEZ PRECISE COLOCAR UMA VARIAVEL BOOL PARA SALVER TRUE OU FALSE
                }
            } catch (Exception ex) { return false; }

            if (con.State == ConnectionState.Open)
                con.Close();
            return true;
        }





    }//FIM DA CLASSE
}//FIM DO NAMESPACE