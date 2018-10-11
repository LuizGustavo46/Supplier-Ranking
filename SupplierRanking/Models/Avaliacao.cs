using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SupplierRanking.Models;

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
        private string data_avaliacao;
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

        public string Data_avaliacao
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
        public bool CadastrarAvaliacao()
        {
            Avaliacao a = new Avaliacao();
            string data_agora = DateTime.Now.ToShortDateString(); //PEGA A DATA COMPLETA SEM A HORA DO DIA
            //string dia = DateTime.Now.Day.ToString(); //PEGA O DIA - USAR SOMENTE SE PRECISAR
            //string mes = DateTime.Now.Month.ToString(); //PEGA O MÊS - USAR SOMENTE SE PRECISAR
            //string ano = DateTime.Now.Year.ToString(); //PEGA O ANO - USAR SOMENTE SE PRECISAR
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

                //SE NÃO HOUVER NADA REGISTRADO NA TABELA DE AVALIACAO NO BANCO, COM RELAÇÃO AO COMPRADOR E AO FORNECEDOR,
                //OU A ULTIMA AVALIAÇÃO JA PASSOU DE 7 DIAS, SOMENTE ASSIM PODE OCORRER O REGISTRO DA AVALIAÇÃO NOVAMENTE

                if (leitor.Read())  //SE CAIR NO ELSE É PORQUE O COMPRADOR ESTA FAZENDO A PRIMEIRA AVALIAÇÃO DA EMPRESA
                {
                    a.data_avaliacao = leitor["data_avaliacao"].ToString();

                    TimeSpan date = Convert.ToDateTime(data_agora) - Convert.ToDateTime(a.data_avaliacao);
                    int diferenca = date.Days;

                    if (diferenca >= 7)
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
                    } //FINAL  DA CONDIÇÃO DOS DIAS
                }
                else
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
         
            int mediaQualidade = 0, mediaAtendimento = 0, mediaEntrega = 0, mediaPreco = 0, mediaSatisfacao = 0, media = 0, cont = 0;
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
                    b.qualidade = int.Parse(leitor["Qualidade"].ToString());
                    b.atendimento = int.Parse(leitor["Atendimento"].ToString());
                    b.entrega = int.Parse(leitor["Entrega"].ToString());
                    b.preco = int.Parse(leitor["Preco"].ToString());
                    b.satisfacao = int.Parse(leitor["Satisfacao"].ToString());

                    mediaQualidade = mediaQualidade + b.qualidade;
                    mediaAtendimento = mediaAtendimento + b.atendimento;
                    mediaEntrega = mediaEntrega + b.entrega;
                    mediaPreco = mediaPreco + b.preco;
                    mediaSatisfacao = mediaSatisfacao + b.satisfacao;
        
                    cont = cont++;
                }

                //ATRIBUINDO A MÉDIA DE CADA CRITÉRIO DE AVALIAÇÃO
                mediaQualidade = mediaQualidade / cont;
                mediaAtendimento = mediaAtendimento / cont;
                mediaEntrega = mediaEntrega / cont;
                mediaPreco = mediaPreco / cont;
                mediaSatisfacao = mediaSatisfacao / cont;
                //MÉDIA TOTAL
                media = mediaQualidade + mediaAtendimento + mediaEntrega + mediaPreco + mediaSatisfacao / 5;
                //COMANDO PARA INSERIR A MÉDIA PARA O FORNECEDOR
                SqlCommand queryMedia2 = new SqlCommand("UPDATE fornecedor SET media = @media WHERE cnpj_fornecedor = @cnpj_fornecedor", con);
                queryMedia2.Parameters.AddWithValue("@media", media);
                queryMedia2.Parameters.AddWithValue("@cnpj_fornecedor", cnpj_fornecedor);
                queryMedia2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string exception = ex.Message; //CASO DER ERRO NA INSERÇÃO
                return false;
            }

            if (con.State == ConnectionState.Open)
                con.Close(); //FECHA CONEXÃO

            return true;
        }

        //MÉTODO QUE PUXA O COMENTÁRIO DA AVALIAÇÃO PARA FAZER O UPDATE DE AVALIAÇÃO (COMENTÁRIO)
        public static Avaliacao ReturnUpdateAvaliacao(string cnpj_fornecedor, string codigo_comprador)
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
                    a.qualidade = int.Parse(leitor["Qualidade"].ToString());
                    a.atendimento = int.Parse(leitor["Atendimento"].ToString());
                    a.entrega = int.Parse(leitor["Entrega"].ToString());
                    a.preco = int.Parse(leitor["Preco"].ToString());
                    a.satisfacao = int.Parse(leitor["Satisfacao"].ToString());
                    a.comentario = leitor["Comentario"].ToString();
                    a.data_avaliacao = leitor["Data_avaliacao"].ToString();
                }

            }
            catch (Exception e)
            {
                a = null;
            }
            if (con.State == ConnectionState.Open)
                con.Close(); //FECHA CONEXÃO

            return a;
        }

        //MÉTODO UPDATE DE AVALIAÇÃO (COMENTÁRIO)
        public bool UpdateAvaliacao()
        {
            try
            {
                con.Open(); //ABRE CONEXÃO
                SqlCommand query = new SqlCommand("UPDATE avaliacao SET comentario = @comentario WHERE " +
                    "cnpj_fornecedor = @cnpj_fornecedor AND codigo_comprador = @codigo_comprador", con);

                if (comentario.Length > 10)
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
            }
            catch (Exception ex)
            {
                return false;
            }
            if (con.State == ConnectionState.Open)
                con.Close();
            return true;
        }

        // MÉTODO PARA LISTAR RANKING DE CERTA CATEGORIA
        public static List<Fornecedor> RankingLista(string categoria)
        {
            List<Fornecedor> ranking = new List<Fornecedor>();
            Fornecedor f = new Fornecedor();
            try
            {
                con.Open(); //ABRE CONEXÃO
                //CRIAÇÃO DE COMANDO PARA FAZER O SELECT DAS EMPRESAS JÁ FORMANDO O RANKING, DA MAIOR PARA A MENOR MÉDIA
                SqlCommand query = new SqlCommand("SELECT * FROM fornecedor WHERE categoria = @categoria ORDER BY media DESC", con);
                query.Parameters.AddWithValue("@categoria", categoria);

                SqlDataReader leitor = query.ExecuteReader();

                while (leitor.Read()) //ENQUANTO O LEITOR LER AS MEDIAS
                {
                    f.Cnpj = leitor["Cnpj"].ToString();
                    f.Nome_empresa = leitor["Nome_empresa"].ToString();
                    f.Email = leitor["Email"].ToString();
                    f.Telefone = leitor["Telefone"].ToString();
                    f.Celular = leitor["Celular"].ToString();
                    f.Endereco = leitor["Endereco"].ToString();
                    f.Bairro = leitor["Bairro"].ToString();
                    f.Cidade = leitor["Cidade"].ToString();
                    f.Uf = leitor["Uf"].ToString();
                    f.Cep = leitor["Cep"].ToString();
                    //f.Senha = leitor["Senha"].ToString(); //NÃO VAMOS MOSTRAR A SENHA, OBVIO...
                    //f.Posicao = leitor["Posicao"].ToString(); //TALVEZ PODEMOS TIRAR ESSE CAMPO DO BANCO...
                    f.Slogan = leitor["Slogan"].ToString();
                    f.Descricao = leitor["Descricao"].ToString();
                    f.Media = float.Parse(leitor["Media"].ToString());
                    f.Plano = leitor["Plano"].ToString();
                    f.Imagem = (byte[])leitor["Imagem"];
                    f.Nome_categoria = leitor["Nome_categorias"].ToString();
                    
                    ranking.Add(f);
                }
            }
            catch (Exception ex)
            {
                ranking = null;
            }
            if (con.State == ConnectionState.Open)
                con.Close();

            return ranking;
        }

        //MÉTODO PARA RETORNAR A LISTA DO RANKING GERAL
        public static List<Fornecedor> RankingGeral()
        {
            List<Fornecedor> ranking = new List<Fornecedor>();
            Fornecedor f = new Fornecedor();

            try
            {
                con.Open(); //ABRE CONEXÃO
                            //CRIAÇÃO DE COMANDO PARA FAZER O SELECT DAS EMPRESAS JÁ FORMANDO O RANKING DAS TOP 10 EMPRESAS
                SqlCommand query = new SqlCommand("SELECT TOP 10 * FROM fornecedor ORDER BY media DESC", con);

                SqlDataReader leitor = query.ExecuteReader();
                while (leitor.Read()) //ENQUANTO O LEITOR LER AS MEDIAS
                {
                    f.Cnpj = leitor["Cnpj"].ToString();
                    f.Nome_empresa = leitor["Nome_empresa"].ToString();
                    f.Email = leitor["Email"].ToString();
                    f.Telefone = leitor["Telefone"].ToString();
                    f.Celular = leitor["Celular"].ToString();
                    f.Endereco = leitor["Endereco"].ToString();
                    f.Bairro = leitor["Bairro"].ToString();
                    f.Cidade = leitor["Cidade"].ToString();
                    f.Uf = leitor["Uf"].ToString();
                    f.Cep = leitor["Cep"].ToString();
                    //f.Senha = leitor["Senha"].ToString(); //NÃO VAMOS MOSTRAR A SENHA, OBVIO...
                    //f.Posicao = leitor["Posicao"].ToString(); //TALVEZ PODEMOS TIRAR ESSE CAMPO DO BANCO...
                    f.Slogan = leitor["Slogan"].ToString();
                    f.Descricao = leitor["Descricao"].ToString();
                    f.Media = float.Parse(leitor["Media"].ToString());
                    f.Plano = leitor["Plano"].ToString();
                    f.Imagem = (byte[])leitor["Imagem"];
                    f.Nome_categoria = leitor["Nome_categorias"].ToString();

                    ranking.Add(f);
                }
            }
            catch (Exception ex)
            {
                ranking = null;
            }
            if (con.State == ConnectionState.Open)
                con.Close();

            return ranking;
        }



       




    }//FIM DA CLASSE
}//FIM DO NAMESPACE