using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeiraWebAPI.Domain.DTO;
using PrimeiraWebAPI.Domain.Entity;
using PrimeiraWebAPI.Services;

namespace PrimeiraWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbunsController : ControllerBase
    {

        //injeção de dependência da classe AlbunsService, que vai validar regras de negócio de um album
        private readonly AlbunsService albumService;

        public AlbunsController(AlbunsService albumService)
        {
            this.albumService = albumService;
        }

        [HttpGet]
        public IEnumerable<Album> Get()
        {
            //List<string> listaDeDiscos = new List<string>();
            //listaDeDiscos.Add("Da Lama ao Caos");
            //listaDeDiscos.Add("Fragile");
            //listaDeDiscos.Add("This Is Acting");
            //listaDeDiscos.Add("Clube da Esquina");
            //return listaDeDiscos;
            return albumService.ListarTodos();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //return "Album XYZ " + id;
            var retorno = albumService.PesquisarPorId(id);
            if (retorno.Sucesso)
            {
                return Ok(retorno.ObjetoRetorno);
            }
            else
            {
                return NotFound(retorno.Mensagem);
            }
        }

        [HttpGet("nome/{nomeParam}")]
        public IActionResult GetByNome(string nomeParam) // nome do parametro deve ser o mesmo do {}
        {
            //return "Album XYZ " + nomeParam;
            var retorno = albumService.PesquisarPorNome(nomeParam);
            if (retorno.Sucesso)
            {
                return Ok(retorno.ObjetoRetorno);
            }
            else
            {
                return NotFound(retorno.Mensagem);
            }
        }

        [HttpPost]
        // FromBody para indicar de o corpo da requisição deve ser mapeado para o modelo
        public IActionResult Post([FromBody] AlbumCreateRequest postModel)
        {
            //Validação modelo de entrada
            if (ModelState.IsValid)
            {
                var retorno = albumService.CadastrarNovo(postModel);
                if (!retorno.Sucesso)
                    return BadRequest(retorno.Mensagem);
                else
                    return Ok(retorno);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPut("{id}")]
        // FromBody para indicar de o corpo da requisição deve ser mapeado para o modelo
        public IActionResult Put(int id, [FromBody] AlbumUpdateRequest putModel)
        {
            if (ModelState.IsValid)
            {
                var retorno = albumService.Editar(id, putModel);
                if (!retorno.Sucesso)
                {
                    return BadRequest(retorno.Mensagem);
                }
                return Ok(retorno.ObjetoRetorno);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        // FromBody para indicar de o corpo da requisição deve ser mapeado para o modelo
        public IActionResult Delete(int id)
        {
            var retorno = albumService.Deletar(id);
            if (!retorno.Sucesso)
            {
                return BadRequest(retorno.Mensagem);
            }
            return Ok();
        }
    }

}
