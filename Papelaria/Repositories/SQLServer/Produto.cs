using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
namespace Papelaria.Repositories.SQLServer
{
    public class Produto
    {
        //atributo da classe que irá receber a linha de conexão que está no web.config
        private readonly string connectionString;

        //metodo construtor que exige um parametro com a linha de conexão
        public Produto(string connectionString){
            this.connectionString = connectionString;
        }
        
        public List<Models.Produto> ObterProduto() {
            List<Models.Produto> produtos = new List<Models.Produto>();

            using (SqlConnection conn = new SqlConnection(this.connectionString)) {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand()) 
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select id, nome, quantidade, preco from produto";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Models.Produto produto = new Models.Produto();

                            produto.Id = (int)reader["id"];
                            produto.Nome = (string)reader["nome"];
                            produto.Quantidade = (int)reader["quantidade"];
                            produto.Preco = (decimal)reader["preco"];

                            produtos.Add(produto);
                        }
                    }
                }
            }
            return produtos;
        }

        public Models.Produto ObterProduto(int id)
        {
            Models.Produto produto = null;

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select id, nome, quantidade, preco from produto where id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            produto = new Models.Produto();

                            produto.Id = (int)reader["id"];
                            produto.Nome = reader["nome"].ToString();
                            produto.Quantidade = (int)reader["quantidade"];
                            produto.Preco = (decimal)reader["preco"];
                        }
                    }
                }
            }
            return produto;
        }

        public bool Add(Models.Produto produto)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $"INSERT INTO Produto (nome, quantidade, preco) VALUES (@nome, @quantidade, @preco);select convert(int,SCOPE_IDENTITY()) as id";
                    cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar)).Value = produto.Nome;
                    cmd.Parameters.Add(new SqlParameter("@quantidade", SqlDbType.VarChar)).Value = produto.Quantidade;
                    cmd.Parameters.Add(new SqlParameter("@preco", SqlDbType.VarChar)).Value = produto.Preco;

                    produto.Id = (int)cmd.ExecuteScalar();
                }
            }
            return produto.Id != 0 ? true : false;
        }

        public bool Update(Models.Produto produto)
        {
            int linhasAfetadas = 0;

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $"update produto set nome = @nome, quantidade = @quantidade, preco = @preco where id = @id";
                    cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar)).Value = produto.Nome;
                    cmd.Parameters.Add(new SqlParameter("@quantidade", SqlDbType.Int)).Value = produto.Quantidade;
                    cmd.Parameters.Add(new SqlParameter("@preco", SqlDbType.Decimal)).Value = produto.Preco;
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = produto.Id;

                    linhasAfetadas = cmd.ExecuteNonQuery();
                }
            }
            return linhasAfetadas != 0;
        }

        public bool Delete(int id)
        {
            int linhasAfetadas = 0;

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $"delete from produto where id = @id;";
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

                    linhasAfetadas = cmd.ExecuteNonQuery();
                }
            }
            return linhasAfetadas == 1;

        }
    }
}