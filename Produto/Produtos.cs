using System;
using System.Collections.Generic;
using Npgsql;

namespace Produto
{
    public class Produtos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }

    public class ProdutoBanco
    {
        private string connectionString = "Host=localhost;Username=seu_usuario;Password=sua_senha;Database=Produtos";

        public void CriarProduto(Produtos produto)
        {
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            string query = "INSERT INTO produtos (nome, preco, quantidade) VALUES (@nome, @preco, @quantidade)";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("nome", produto.Nome);
            cmd.Parameters.AddWithValue("preco", produto.Preco);
            cmd.Parameters.AddWithValue("quantidade", produto.Quantidade);

            cmd.ExecuteNonQuery();
        }

        public List<Produtos> LerProdutos()
        {
            var lista = new List<Produtos>();

            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            string query = "SELECT id, nome, preco, quantidade FROM produtos";
            using var cmd = new NpgsqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Produtos
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Preco = reader.GetDecimal(2),
                    Quantidade = reader.GetInt32(3)
                });
            }

            return lista;
        }

        public void AtualizarProduto(int id, decimal novoPreco)
        {
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            string query = "UPDATE produtos SET preco = @preco WHERE id = @id";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("preco", novoPreco);
            cmd.Parameters.AddWithValue("id", id);

            cmd.ExecuteNonQuery();
        }
    }
}

