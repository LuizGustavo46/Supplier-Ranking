using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SupplierRanking.Models;
using System.Linq;
using System.Web;

namespace SupplierRanking.Models
{
    public class HomeLogada
    {
        //CONEXÃO COM O BANCO DE DADOS - SE FOR USAR EM CASA É SÓ TROCAR "SENAI" PARA O SEU NOME
        private static SqlConnection con =
            new SqlConnection(ConfigurationManager.ConnectionStrings["SENAI"].ConnectionString);

        /**************************************************** LISTAR PESQUISA CATEGORIA ********************************************************/

        public static List<Fornecedor> RankingCategoria(string categoria)
        {
            List<Fornecedor> ranking = new List<Fornecedor>();
            
            try
            {
                con.Open(); //ABRE CONEXÃO
                //CRIAÇÃO DE COMANDO PARA FAZER O SELECT DAS EMPRESAS JÁ FORMANDO O RANKING, DA MAIOR PARA A MENOR MÉDIA
                SqlCommand query = new SqlCommand("SELECT * FROM fornecedor WHERE nome_categorias = @nome_categorias ORDER BY plano DESC, media DESC", con);
                query.Parameters.AddWithValue("@nome_categorias", categoria);
                SqlDataReader leitor = query.ExecuteReader();
                while (leitor.Read()) //ENQUANTO O LEITOR LER AS MEDIAS
                {
                    Fornecedor f = new Fornecedor();

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
                    f.Slogan = leitor["Slogan"].ToString();
                    f.Descricao = leitor["Descricao"].ToString();
                    f.Media = float.Parse(leitor["Media"].ToString());
                    f.Plano = leitor["Plano"].ToString();
                    f.Imagem = (byte[])leitor["Imagem"];
                    f.Imagem64 = Convert.ToBase64String(f.Imagem);
                    f.Nome_categoria = leitor["Nome_categorias"].ToString();
                    if (f.Plano.Equals("P")) //SE O FORNECEDOR FOR PREMIUM MOSTRA AS MÉDIAS DOS CRITÉRIOS DE AVALIAÇÃO
                    {
                        f.Media_qualidade = float.Parse(leitor["Media_qualidade"].ToString());
                        f.Media_atendimento = float.Parse(leitor["Media_atendimento"].ToString());
                        f.Media_entrega = float.Parse(leitor["Media_entrega"].ToString());
                        f.Media_preco = float.Parse(leitor["Media_preco"].ToString());
                        f.Media_satisfacao = float.Parse(leitor["Media_satisfacao"].ToString());
                    }
                    ranking.Add(f);
                }
            }
            catch (Exception ex) { ranking = null; }

            if (con.State == ConnectionState.Open)
                con.Close();
            return ranking;
        }

        /***************************************************** LISTAR RANKING GERAL **********************************************************/

        public static List<Fornecedor> RankingGeral()
        {
            List<Fornecedor> ranking = new List<Fornecedor>();
  

            try
            {
                con.Open(); //ABRE CONEXÃO
                            //CRIAÇÃO DE COMANDO PARA FAZER O SELECT DAS EMPRESAS JÁ FORMANDO O RANKING DAS TOP 10 EMPRESAS
                SqlCommand query = new SqlCommand("SELECT TOP 10 * FROM fornecedor ORDER BY plano DESC, media DESC", con);
                SqlDataReader leitor = query.ExecuteReader();
                while (leitor.Read()) //ENQUANTO O LEITOR LER AS MEDIAS
                {
                    Fornecedor f = new Fornecedor();

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
                    f.Slogan = leitor["Slogan"].ToString();
                    f.Descricao = leitor["Descricao"].ToString();
                    f.Media = float.Parse(leitor["Media"].ToString());
                    f.Plano = leitor["Plano"].ToString();
                    f.Imagem = (byte[])leitor["Imagem"];
                    f.Imagem64 = Convert.ToBase64String(f.Imagem);
                    f.Nome_categoria = leitor["Nome_categorias"].ToString();
                    if (f.Plano.Equals("P")) //SE O FORNECEDOR FOR PREMIUM MOSTRA AS MÉDIAS DOS CRITÉRIOS DE AVALIAÇÃO
                    {
                        f.Media_qualidade = float.Parse(leitor["Media_qualidade"].ToString());
                        f.Media_atendimento = float.Parse(leitor["Media_atendimento"].ToString());
                        f.Media_entrega = float.Parse(leitor["Media_entrega"].ToString());
                        f.Media_preco = float.Parse(leitor["Media_preco"].ToString());
                        f.Media_satisfacao = float.Parse(leitor["Media_satisfacao"].ToString());
                    }
                    ranking.Add(f);
                }
            }
            catch (Exception ex) { ranking = null; }

            if (con.State == ConnectionState.Open)
                con.Close();
            return ranking;
        }

        /***************************************************** LISTAR RANKING PREMUIUM **********************************************************/

        public static List<Fornecedor> RankingPremium()
        {
            List<Fornecedor> ranking = new List<Fornecedor>();
            

            try
            {
                con.Open(); //ABRE CONEXÃO
                            //CRIAÇÃO DE COMANDO PARA FAZER O SELECT DAS EMPRESAS PREMIUM EM ORDEM DESCRESCENTE
                SqlCommand query = new SqlCommand("SELECT * FROM fornecedor WHERE plano = 'P' ORDER BY media DESC", con);
                SqlDataReader leitor = query.ExecuteReader();
                while (leitor.Read()) //ENQUANTO O LEITOR LER AS MEDIAS
                {
                    Fornecedor f = new Fornecedor();

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
                    f.Slogan = leitor["Slogan"].ToString();
                    f.Descricao = leitor["Descricao"].ToString();
                    f.Media = float.Parse(leitor["Media"].ToString());
                    f.Plano = leitor["Plano"].ToString();
                    f.Imagem = (byte[])leitor["Imagem"];
                    f.Imagem64 = Convert.ToBase64String(f.Imagem);
                    f.Nome_categoria = leitor["Nome_categorias"].ToString();
                    f.Media_qualidade = float.Parse(leitor["Media_qualidade"].ToString());
                    f.Media_atendimento = float.Parse(leitor["Media_atendimento"].ToString());
                    f.Media_entrega = float.Parse(leitor["Media_entrega"].ToString());
                    f.Media_preco = float.Parse(leitor["Media_preco"].ToString());
                    f.Media_satisfacao = float.Parse(leitor["Media_satisfacao"].ToString());


                    ranking.Add(f);
                }
            }
            catch (Exception ex) { ranking = null; }

            if (con.State == ConnectionState.Open)
                con.Close();
            return ranking;
        }

        /**************************************************** LISTAR RANKING COM FILTROS ********************************************************/
        public static List<Fornecedor> RankingFiltro(string filtro)
        {
            List<Fornecedor> ranking = new List<Fornecedor>();
            

            try
            {
                con.Open(); //ABRE CONEXÃO
                SqlCommand query;
                SqlDataReader leitor = null;
                //CRIAÇÃO DE COMANDO PARA FAZER O SELECT DOS FORNECEDORES EM ORDEM DE FILTRO
                if (filtro.Equals("media_qualidade"))
                {
                    query = new SqlCommand("SELECT * FROM fornecedor ORDER BY plano DESC, media_qualidade DESC", con);
                    leitor = query.ExecuteReader();
                }
                else if (filtro.Equals("media_atendimento"))
                {
                    query = new SqlCommand("SELECT * FROM fornecedor ORDER BY plano DESC, media_atendimento DESC", con);
                    leitor = query.ExecuteReader();
                }
                else if (filtro.Equals("media_entrega"))
                {
                    query = new SqlCommand("SELECT * FROM fornecedor ORDER BY plano DESC, media_entrega DESC", con);
                    leitor = query.ExecuteReader();
                }
                else if (filtro.Equals("media_preco"))
                {
                    query = new SqlCommand("SELECT * FROM fornecedor ORDER BY plano DESC, media_preco DESC", con);
                    leitor = query.ExecuteReader();
                }
                else if (filtro.Equals("media_satisfacao"))
                {
                    query = new SqlCommand("SELECT * FROM fornecedor ORDER BY plano DESC, media_satisfacao DESC", con);
                    leitor = query.ExecuteReader();
                }

                while (leitor.Read())
                {
                    Fornecedor f = new Fornecedor();

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
                    f.Slogan = leitor["Slogan"].ToString();
                    f.Descricao = leitor["Descricao"].ToString();
                    f.Media = float.Parse(leitor["Media"].ToString());
                    f.Plano = leitor["Plano"].ToString();
                    f.Imagem = (byte[])leitor["Imagem"];
                    f.Imagem64 = Convert.ToBase64String(f.Imagem);
                    f.Nome_categoria = leitor["Nome_categorias"].ToString();
                    if (f.Plano.Equals("P")) //SE O FORNECEDOR FOR PREMIUM MOSTRA AS MÉDIAS DOS CRITÉRIOS DE AVALIAÇÃO
                    {
                        f.Media_qualidade = float.Parse(leitor["Media_qualidade"].ToString());
                        f.Media_atendimento = float.Parse(leitor["Media_atendimento"].ToString());
                        f.Media_entrega = float.Parse(leitor["Media_entrega"].ToString());
                        f.Media_preco = float.Parse(leitor["Media_preco"].ToString());
                        f.Media_satisfacao = float.Parse(leitor["Media_satisfacao"].ToString());
                    }
                    ranking.Add(f);
                }
            }
            catch (Exception ex) { ranking = null; }

            if (con.State == ConnectionState.Open)
                con.Close();
            return ranking;
        }

        /**************************************************** LISTAR PESQUISA FORNECEDOR ********************************************************/

        public static List<Fornecedor> PesquisaFornecedor(string pesquisa) //TRAVADO PELA HOME LOGADA
        {
            List<Fornecedor> lista = new List<Fornecedor>();
            
            try
            {
                // abre conexão
                con.Open();

                // Criação de comando para selecionar a tabela FORNECEDOR
                SqlCommand query =
                    new SqlCommand("SELECT * FROM fornecedor WHERE nome_empresa like @texto", con);
                query.Parameters.AddWithValue("@texto", "%" + pesquisa + "%");
                SqlDataReader leitor = query.ExecuteReader();



                //prepara o leitor
                while (leitor.Read())
                {
                    Fornecedor f = new Fornecedor();

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
                    f.Slogan = leitor["Slogan"].ToString();
                    f.Descricao = leitor["Descricao"].ToString();
                    f.Media = float.Parse(leitor["Media"].ToString());
                    f.Plano = leitor["Plano"].ToString();
                    f.Imagem = (byte[])leitor["Imagem"];
                    f.Imagem64 = Convert.ToBase64String(f.Imagem);
                    f.Nome_categoria = leitor["Nome_categorias"].ToString();
                    if (f.Plano.Equals("P")) //SE O FORNECEDOR FOR PREMIUM MOSTRA AS MÉDIAS DOS CRITÉRIOS DE AVALIAÇÃO
                    {
                        f.Media_qualidade = float.Parse(leitor["Media_qualidade"].ToString());
                        f.Media_atendimento = float.Parse(leitor["Media_atendimento"].ToString());
                        f.Media_entrega = float.Parse(leitor["Media_entrega"].ToString());
                        f.Media_preco = float.Parse(leitor["Media_preco"].ToString());
                        f.Media_satisfacao = float.Parse(leitor["Media_satisfacao"].ToString());
                    }
                    //colocar campo de posiçõ de ranking
                    lista.Add(f); // adiciona os valores cadastrados no banco à lista
                }
            }

            //tratamento de erro
            catch (Exception ex)
            {
                lista = null;
            }

            if (con.State == ConnectionState.Open)
                con.Close();//fecha a conexao

            return lista;
        }

        /***************************************************** LISTAR COMENTÁRIOS ********************************************************/

        public List<Avaliacao> Comentarios(string cnpj_fornecedor)
        {
            List<Avaliacao> listaComentarios = new List<Avaliacao>();

            con.Open();
            try
            {
                SqlCommand query =
                    new SqlCommand("SELECT comentario, data_avaliacao, cnpj_fornecedor, codigo_comprador, nome, nome_empresa FROM avaliacao A,"+ 
                    " comprador C where codigo = codigo_comprador AND cnpj_fornecedor = @cnpj_fornecedor", con);
                query.Parameters.AddWithValue("@cnpj_fornecedor", cnpj_fornecedor);
                SqlDataReader leitor = query.ExecuteReader();

                while (leitor.Read())
                {
                    Avaliacao a = new Avaliacao();
                    Comprador c = new Comprador();

                    a.Comentario = leitor["comentario"].ToString();
                    a.Data_avaliacao = leitor["data_avaliacao"].ToString();
                    a.Codigo_comprador = int.Parse(leitor["codigo_comprador"].ToString());
                    c.Nome = leitor["nome"].ToString();
                    c.Nome_empresa = leitor["nome_empresa"].ToString();

                 
                        
                    a.C = c;

                    listaComentarios.Add(a);
                }
            } catch(Exception ex) { listaComentarios = null; }

            if (con.State == ConnectionState.Open)
                con.Close();//fecha a conexao

            return listaComentarios;
        }

        /***************************************************** PERFIL DO FORNECEDOR ********************************************************/

        public static Fornecedor Perfil(string cnpj) //FEITO
        {
            Fornecedor f = new Fornecedor();
            try
            {
                con.Open(); //ABRE CONEXÃO

                //comando para selecionar o fornecedor apartir do cnpj
                SqlCommand query = new SqlCommand("SELECT * FROM fornecedor WHERE cnpj = @cnpj", con);
                query.Parameters.AddWithValue("@cnpj", cnpj);
                SqlDataReader leitor = query.ExecuteReader();

                //prepara o leitor para pegar as informações a serem exibidas
                if (leitor.Read())
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
                    f.Slogan = leitor["Slogan"].ToString();
                    f.Descricao = leitor["Descricao"].ToString();
                    f.Media = float.Parse(leitor["Media"].ToString());
                    f.Plano = leitor["Plano"].ToString();
                    f.Imagem = (byte[])leitor["Imagem"];
                    f.Imagem64 = Convert.ToBase64String(f.Imagem);
                    f.Nome_categoria = leitor["Nome_categorias"].ToString();
                    if (f.Plano.Equals("P")) //SE O FORNECEDOR FOR PREMIUM MOSTRA AS MÉDIAS DOS CRITÉRIOS DE AVALIAÇÃO
                    {
                        f.Media_qualidade = float.Parse(leitor["Media_qualidade"].ToString());
                        f.Media_atendimento = float.Parse(leitor["Media_atendimento"].ToString());
                        f.Media_entrega = float.Parse(leitor["Media_entrega"].ToString());
                        f.Media_preco = float.Parse(leitor["Media_preco"].ToString());
                        f.Media_satisfacao = float.Parse(leitor["Media_satisfacao"].ToString());
                    }
                    f.Pdf = (byte[])leitor["Pdf"];
                    f.Pdf64 = Convert.ToBase64String(f.Pdf);
                }
            }
            //tratamento de erro
            catch (Exception e) { f = null; }
            
            if (con.State == ConnectionState.Open)
                con.Close(); //FECHA CONEXÃO

            return f;
        }

        /***************************************************** PERFIL DO FORNECEDOR ********************************************************/

        public List<String> GaleriaFotos(string cnpj) //FEITO
        {
            List<String> galeriaFotos = new List<string>();
            byte[] foto;
            string foto64;
            try
            {
                con.Open(); //ABRE CONEXÃO

                //comando para selecionar o fornecedor apartir do cnpj
                SqlCommand query = new SqlCommand("SELECT * FROM arquivos WHERE cnpj = @cnpj", con);
                query.Parameters.AddWithValue("@cnpj", cnpj);
                SqlDataReader leitor = query.ExecuteReader();

                //prepara o leitor para pegar as informações a serem exibidas
                while(leitor.Read())
                {
                    foto = (byte[])leitor["Imagem"];
                    foto64 = Convert.ToBase64String(foto);
                    galeriaFotos.Add(foto64);
                }
            }
            //tratamento de erro
            catch (Exception e) { galeriaFotos = null; }

            if (con.State == ConnectionState.Open)
                con.Close(); //FECHA CONEXÃO

            return galeriaFotos;
        }

    }//FINAL DA CLASSE
}//FINAL NAMESPACE