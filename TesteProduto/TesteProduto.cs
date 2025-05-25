using System;
using Xunit;
using TesteProduto;
using System.Linq;
using System.Collections.Generic;
using Produto;

namespace TesteProduto
{
    public class ProdutoTestes
    {
        private ProdutoBanco repo = new ProdutoBanco();

        [Fact]
        public void CriarProduto_DeveInserirProdutoNoBanco()
        {
            var produto = new Produtos
            {
                Nome = "Mouse USB",
                Preco = 49.90m,
                Quantidade = 10
            };

            repo.CriarProduto(produto);

            var produtos = repo.LerProdutos();
            Assert.Contains(produtos, p => p.Nome == "Mouse USB");
        }

        [Fact]
        public void AtualizarProduto_DeveAlterarPreco()
        {
            var produtos = repo.LerProdutos();
            var produto = produtos.FirstOrDefault(p => p.Nome == "Mouse USB");

            Assert.NotNull(produto);

            decimal novoPreco = 59.90m;
            repo.AtualizarProduto(produto.Id, novoPreco);

            var atualizado = repo.LerProdutos().First(p => p.Id == produto.Id);
            Assert.Equal(novoPreco, atualizado.Preco);
        }
    }
}
