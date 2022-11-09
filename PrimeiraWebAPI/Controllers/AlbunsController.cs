using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeiraWebAPI.Domain.DTO;
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
        public IEnumerable<String> Get()
        {
            List<string> listaDeDiscos = new List<string>();
            listaDeDiscos.Add("Da Lama ao Caos");
            listaDeDiscos.Add("Fragile");
            listaDeDiscos.Add("This Is Acting");
            listaDeDiscos.Add("Clube da Esquina");
            return listaDeDiscos;
        }

        [HttpGet("{id}")]
        public String GetById(int id)
        {
            return "Album XYZ " + id;
        }

        [HttpGet("nome/{nomeParam}")]
        public String GetByNome(string nomeParam) // nome do parametro deve ser o mesmo do {}
        {
            return "Album XYZ " + nomeParam;
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
                return Ok(retorno);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
    
}
