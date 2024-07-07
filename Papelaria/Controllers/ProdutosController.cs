using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Papelaria.Controllers
{
    public class ProdutosController : ApiController
    {
        //cria o atributo que vai acessar todos os metodos do repositorio produto
        public readonly Repositories.SQLServer.Produto RepositorioProduto;

        public ProdutosController() 
        { 
            //sempre que o controller é chamdo, o atributo de acesso aos metodos no repositorio é instanciado e envia como parametro a connection string
            this.RepositorioProduto = new Repositories.SQLServer.Produto(Configurations.Database.getConnectionString());
        }
        // GET: api/Produtos
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(this.RepositorioProduto.ObterProduto());
        }

        // GET: api/Produtos/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(this.RepositorioProduto.ObterProduto(id));
        }

        // POST: api/Produtos
        [HttpPost]
        public IHttpActionResult Post(Models.Produto produto)
        {
            if (!this.RepositorioProduto.Add(produto))
                return InternalServerError();

            return Ok("Nice! produto cadastrado com sucesso.");
        }

        // PUT: api/Produtos/5
        [HttpPut]
        public IHttpActionResult Put(int id, Models.Produto produto)
        {
            if (id != produto.Id)
                return BadRequest("O id da requisição não coincide com o id do produto.");

            if (!this.RepositorioProduto.Update(produto))
                return InternalServerError();

            return Ok(produto);
        }

        // DELETE: api/Produtos/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!this.RepositorioProduto.Delete(id))
                return NotFound();

            return Ok("Nice! produto deletado com sucesso.");
        }
    }
}
